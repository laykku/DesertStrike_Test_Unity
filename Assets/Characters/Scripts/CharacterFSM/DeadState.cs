using DesertStrike.Characters.CharacterFSM.Base;
using UnityEngine;
using Zenject;

namespace DesertStrike.Characters.CharacterFSM
{
    public class DeadState : State
    {
        private int deathParam = Animator.StringToHash("Death");
        
        public override void Enter()
        {
            base.Enter();
            _character.TriggerAnimation(deathParam);
            _character.Kill();
        }

        public DeadState(Character character, IStateMachine stateMachine) : base(character, stateMachine)
        {
        }
        
        public class Factory : Factory<Character, IStateMachine, DeadState>
        {
            
        }
    }
}