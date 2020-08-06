namespace DesertStrike.Characters.CharacterFSM.Base
{
    public interface IStateMachine
    {
        IState CurrentState { get; }
        void ChangeState(IState state);
    }
}