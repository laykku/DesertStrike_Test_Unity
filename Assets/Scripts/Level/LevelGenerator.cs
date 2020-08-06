using System.Collections.Generic;
using DesertStrike.Level.Navigation;
using UnityEngine;

namespace DesertStrike.Level
{
    public class LevelGenerator
    {
        private readonly GroundTile.Factory _groundTileFactory;
        private readonly LevelNavigation _navigation;
        private readonly LevelSettings _settings;
        private readonly ObstacleFactory _obstacleFactory;
        
        private Bounds _bounds = new Bounds();

        public LevelGenerator(GroundTile.Factory groundTileFactory, LevelNavigation navigation,
            LevelSettings settings, ObstacleFactory obstacleFactory)
        {
            _groundTileFactory = groundTileFactory;
            _navigation = navigation;
            _settings = settings;
            _obstacleFactory = obstacleFactory;
        }

        public void BuildGround()
        {
            List<GroundTile> tiles = new List<GroundTile>();
            for (int x = 0; x < _settings.Width; x++)
            {
                for (int y = 0; y < _settings.Height; y++)
                {
                    GroundTile tile = _groundTileFactory.Create();
                    tile.name = $"tile_{x}_{y}";
                    Vector3 pos = new Vector3(x * tile.Collider.size.x, 0f, y * tile.Collider.size.z);
                    tile.transform.position = pos;
                    tiles.Add(tile);
                }
            }

            foreach (var groundObj in tiles)
            {
                Vector3 center = groundObj.Transform.position;
                Vector3 size = groundObj.Collider.bounds.size;
                _bounds.Encapsulate(new Bounds(center, size));
            }
            
            List<IObstacle> obstacles = BuildObjects(_settings.ObstacleCount);

            _navigation.BuildNavigationGraph(_bounds.min, _bounds.max, _settings.AllowDiagonal, obstacles);
        }

        private List<IObstacle> BuildObjects(int count)
        {
            List<IObstacle> obstacles = new List<IObstacle>();
            
            for (int i = 0; i < count; i++)
            {
                IObstacle obstacle = _obstacleFactory.Create();
                obstacle.Transform.position = GetRandomPoint();
                obstacles.Add(obstacle);
            }

            return obstacles;
        }

        public Vector3 GetRandomPoint()
        {
            return new Vector3(Random.Range(_bounds.min.x, _bounds.max.x), 0f,
                Random.Range(_bounds.min.z, _bounds.max.z));
        }
        
        public List<Vector3> FindPath(Vector3 start, Vector3 target)
        {
            return _navigation.FindPath(start, target);
        }
        
        [System.Serializable]
        public class LevelSettings
        {
            public int Width;
            public int Height;
            public int ObstacleCount;
            
            [Header("Navigation settings")]
            public bool AllowDiagonal;
        }
    }
}