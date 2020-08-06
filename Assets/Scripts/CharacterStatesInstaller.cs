using DesertStrike.Characters;
using DesertStrike.Characters.CharacterFSM;
using DesertStrike.Characters.CharacterFSM.Base;
using Zenject;

namespace DesertStrike
{
    public class CharacterStatesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IStateMachine>().To<StateMachine>().AsTransient();
            Container.BindFactory<Character, IStateMachine, ShootingState, ShootingState.Factory>().AsSingle();
            Container.BindFactory<Character, IStateMachine, StandingState, StandingState.Factory>().AsSingle();
            Container.BindFactory<Character, IStateMachine, DeadState, DeadState.Factory>().AsSingle();
        }
    }
}