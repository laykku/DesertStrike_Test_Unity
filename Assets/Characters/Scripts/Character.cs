using System;
using System.Collections;
using Characters.Scripts;
using DesertStrike.Characters.CharacterFSM;
using DesertStrike.Characters.CharacterFSM.Base;
using UnityEngine;
using Zenject;

namespace DesertStrike.Characters
{
    public class Character : MonoBehaviour, IDamageReceiver
    {
        public event Action<Character> Killed;
        
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private CapsuleCollider _collider;
        [SerializeField] private Transform _shootOrigin;
        
        [SerializeField] private CharacterData _data;

        [Inject] private readonly ShootingState.Factory ShootingFactory;
        [Inject] private readonly StandingState.Factory StandingFactory;
        [Inject] private readonly DeadState.Factory DeadFactory;
        [Inject] private readonly IWeapon Weapon;

        [Inject] public readonly IStateMachine StateMachine;
        public StandingState Standing { get; private set; }
        public ShootingState Shooting { get; private set; }
        public DeadState Dead { get; private set; }
        
        public ICharacterControls Controls { get; private set; }

        private int horizontalMoveParam = Animator.StringToHash("H_Speed");
        private int verticalMoveParam = Animator.StringToHash("V_Speed");

        public float Health { get; private set; } = 1f;
        public void Hit(float damage)
        {
            Health -= damage;
        }

        protected void Init(ICharacterControls controls)
        {
            Controls = controls;

            Shooting = ShootingFactory.Create(this, StateMachine);
            Standing = StandingFactory.Create(this, StateMachine);
            Dead = DeadFactory.Create(this, StateMachine);
            
            StateMachine.ChangeState(Standing);
        }

        public void Shoot()
        {
            Weapon.Shoot(_shootOrigin.position, _shootOrigin.forward);
        }

        public void Kill()
        {
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
            
            Killed?.Invoke(this);
            StartCoroutine(DelayedDestroy(5f));
        }

        private IEnumerator DelayedDestroy(float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }

        private void Update()
        {
            StateMachine.CurrentState.HandleInput();
            StateMachine.CurrentState.HandleLogic();

            Debug.Log(StateMachine.CurrentState);
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.HandlePhysics();
        }

        public void Move(float speed, float rotationSpeed)
        {
            Vector3 targetVelocity = transform.forward * speed * _data.MovementSpeed * Time.deltaTime;
            targetVelocity.y = _rigidbody.velocity.y;
            _rigidbody.velocity = targetVelocity;
            _rigidbody.angularVelocity = Vector3.up * rotationSpeed * _data.TurnSpeed * Time.deltaTime;

            _animator.SetFloat(verticalMoveParam, speed * _data.MovementSpeed * Time.deltaTime);
            _animator.SetFloat(horizontalMoveParam, _rigidbody.angularVelocity.y);
        }

        public void SetAnimatorBool(int param, bool state)
        {
            _animator.SetBool(param, state);
        }

        public void TriggerAnimation(int param)
        {
            _animator.SetTrigger(param);
        }

        public void ResetMove()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _animator.SetFloat(verticalMoveParam, 0f);
            _animator.SetFloat(horizontalMoveParam, 0f);
        }
    }
}