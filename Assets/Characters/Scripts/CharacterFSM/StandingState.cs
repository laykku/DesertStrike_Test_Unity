using DesertStrike.Characters.CharacterFSM.Base;
using Zenject;

namespace DesertStrike.Characters.CharacterFSM
{
    public class StandingState : GroundedState
    {
        private bool _shoot;
        
        public StandingState(Character character, IStateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public class Factory : Factory<Character, IStateMachine, StandingState>
        {
            
        }
        
        public override void Enter()
        {
            base.Enter();
            _shoot = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
            _shoot = _character.Controls.FireStart;
        }

        public override void HandleLogic()
        {
            base.HandleLogic();
            if (_shoot)
            {
                _character.StateMachine.ChangeState(_character.Shooting);
            }
        }
    }
}