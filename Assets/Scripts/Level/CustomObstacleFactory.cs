using UnityEngine;
using Zenject;

namespace DesertStrike.Level
{
    public class CustomObstacleFactory : IFactory<IObstacle>
    {
        private DiContainer _container;
        private Settings _settings;
        
        public CustomObstacleFactory(DiContainer container, Settings settings)
        {
            _container = container;
            _settings = settings;
        }

        public IObstacle Create()
        {
            GameObject prefab = _settings.ObstaclePrefabs[Random.Range(0, _settings.ObstaclePrefabs.Length)];
            return _container.InstantiatePrefab(prefab).GetComponent<IObstacle>();
        }
        
        [System.Serializable]
        public class Settings
        {
            public GameObject[] ObstaclePrefabs;
        }
    }
}