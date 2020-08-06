using DesertStrike;
using DesertStrike.Level;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ConfigInstaller", menuName = "Installers/ConfigInstaller")]
public class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
{
    public LevelGenerator.LevelSettings LevelSettings;
    public CustomObstacleFactory.Settings ObstacleFactorySettings;
    public CustomEnemyFactory.Settings EnemyFactorySettings;
    
    public override void InstallBindings()
    {
        Container.BindInstance(LevelSettings);
        Container.BindInstance(ObstacleFactorySettings);
        Container.BindInstance(EnemyFactorySettings);
    }
}