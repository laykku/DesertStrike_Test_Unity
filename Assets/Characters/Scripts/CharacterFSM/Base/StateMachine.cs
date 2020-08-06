namespace DesertStrike.Characters.CharacterFSM.Base
{
    public class StateMachine : IStateMachine
    {
        public IState CurrentState { get; private set; }

        public void ChangeState(IState state)
        {
            CurrentState?.Exit();
            CurrentState = state;
            CurrentState.Enter();
        }
    }
}