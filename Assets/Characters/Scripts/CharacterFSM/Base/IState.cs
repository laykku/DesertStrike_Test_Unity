namespace DesertStrike.Characters.CharacterFSM.Base
{
    public interface IState
    {
        void Enter();
        void Exit();
        void HandleInput();
        void HandleLogic();
        void HandlePhysics();
    }
}