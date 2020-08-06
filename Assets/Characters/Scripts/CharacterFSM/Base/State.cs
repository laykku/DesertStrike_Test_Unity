namespace DesertStrike.Characters.CharacterFSM.Base
{
    public class State : IState
    {
        protected Character _character;
        protected IStateMachine _stateMachine;

        protected State(Character character, IStateMachine stateMachine)
        {
            _character = character;
            _stateMachine = stateMachine;
        }
        
        public virtual void Enter()
        {
            
        }

        public virtual void Exit()
        {
            
        }

        public virtual void HandleInput()
        {
            
        }

        public virtual void HandleLogic()
        {
            
        }

        public virtual void HandlePhysics()
        {
            
        }
    }
}