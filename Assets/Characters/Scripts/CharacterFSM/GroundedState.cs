using DesertStrike.Characters.CharacterFSM.Base;

namespace DesertStrike.Characters.CharacterFSM
{
    public class GroundedState : State
    {
        private float _horizontalInput = 0f;
        private float _verticalInput = 0f;
        
        public GroundedState(Character character, IStateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _horizontalInput = 0f;
            _verticalInput = 0f;
        }

        public override void Exit()
        {
            base.Exit();
            _character.ResetMove();
        }

        public override void HandleInput()
        {
            base.HandleInput();
            _horizontalInput = _character.Controls.Horizontal;
            _verticalInput = _character.Controls.Vertical;
        }

        public override void HandlePhysics()
        {
            base.HandlePhysics();
            _character.Move(_verticalInput, _horizontalInput);
        }

        public override void HandleLogic()
        {
            base.HandleLogic();
            
            if (_character.Health <= 0f)
            {
                _character.StateMachine.ChangeState(_character.Dead);
            }
        }
    }
}