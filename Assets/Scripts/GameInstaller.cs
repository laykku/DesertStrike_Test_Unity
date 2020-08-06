using DesertStrike.Characters;
using UnityEngine;
using Zenject;

namespace DesertStrike
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private CameraController _cameraPrefab;
        [SerializeField] private ABullet _bulletPrefab;
        [SerializeField] private Coin _coinPrefab;
        
        public override void InstallBindings()
        {
            Container.Bind<UserControls>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyAIControls>().AsSingle(); // TODO check

            Container.BindFactory<Coin, Coin.Factory>().FromComponentInNewPrefab(_coinPrefab);
            Container.BindFactory<ABullet, ABullet.Factory>().FromComponentInNewPrefab(_bulletPrefab);
            Container.Bind<IWeapon>().To<Weapon>().AsTransient();
            
            Container.BindFactory<Player, Player.Factory>()
                .FromComponentInNewPrefab(_playerPrefab);
            Container.BindFactory<int, Enemy, Enemy.Factory>()
                .FromFactory<CustomEnemyFactory>();

            Container.Bind<CameraController>().FromComponentInNewPrefab(_cameraPrefab).AsSingle();

            Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
            
        }
    }
}