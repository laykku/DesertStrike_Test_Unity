using DesertStrike.Level;
using DesertStrike.Level.Navigation;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private GroundTile _groundTilePrefab;

    public override void InstallBindings()
    {
        Container.BindFactory<IObstacle, ObstacleFactory>()
            .FromFactory<CustomObstacleFactory>();
        
        Container.Bind<IPathfindingAlgorithm>().To<AStar>().AsSingle();
        Container.Bind<LevelNavigation>().AsSingle();
        Container.BindFactory<GroundTile, GroundTile.Factory>()
            .FromComponentInNewPrefab(_groundTilePrefab);
            
        Container.Bind<LevelGenerator>().AsSingle();
    }
}