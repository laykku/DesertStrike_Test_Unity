using DesertStrike.Characters;
using DesertStrike.Level;
using Zenject;

namespace DesertStrike
{
    public class GameController : IInitializable
    {
        private readonly Enemy.Factory _enemyFactory;
        private readonly LevelGenerator _levelGenerator;
        private readonly Coin.Factory _coinFactory;
        
        public GameController(Player.Factory playerFactory, Enemy.Factory enemyFactory,
            LevelGenerator levelGenerator, CameraController cameraController, Coin.Factory coinFactory)
        {
            _enemyFactory = enemyFactory;
            _levelGenerator = levelGenerator;
            _coinFactory = coinFactory;
            levelGenerator.BuildGround();

            Character player = playerFactory.Create();
            player.transform.position = _levelGenerator.GetRandomPoint();
            cameraController.SetTarget(player.transform);
            
            CreateEnemies(10);
        }

        private void CreateEnemies(int count)
        {
            for (int i = 0; i < count; i++)
            {
                CreateEnemy();
            }
        }

        private void OnEnemyKilled(Character enemy)
        {
            _coinFactory.Create().transform.position = enemy.transform.position;
            CreateEnemy();
        }

        private void CreateEnemy()
        {
            Enemy enemy = _enemyFactory.Create(-1);
            enemy.Killed += OnEnemyKilled;
            enemy.transform.position = _levelGenerator.GetRandomPoint();
        }

        public void Initialize()
        {
        }
    }
}