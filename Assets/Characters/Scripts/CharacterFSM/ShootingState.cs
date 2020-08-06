using DesertStrike.Characters.CharacterFSM.Base;
using UnityEngine;
using Zenject;

namespace DesertStrike.Characters.CharacterFSM
{
    public class ShootingState : GroundedState
    {
        private int shootParam = Animator.StringToHash("Shooting");

        private bool _shooting;
        
        public override void Enter()
        {
            base.Enter();
            _character.SetAnimatorBool(shootParam, true);
            _shooting = false;
        }

        public override void Exit()
        {
            base.Exit();
            _character.SetAnimatorBool(shootParam, false);
        }

        public override void HandleInput()
        {
            base.HandleInput();
            _shooting = _character.Controls.FireStop;
        }

        public override void HandleLogic()
        {
            base.HandleLogic();
            if (_shooting)
            {
                _character.StateMachine.ChangeState(_character.Standing);
            }
            
            _character.Shoot();
        }

        public ShootingState(Character character, IStateMachine stateMachine) : base(character, stateMachine)
        {
        }
        
        public class Factory : Factory<Character, IStateMachine, ShootingState>
        {
            
        }
    }
}