using DesertStrike.Characters;
using UnityEngine;
using Zenject;

namespace DesertStrike
{
    public class CustomEnemyFactory : IFactory<int, Enemy>
    {
        private DiContainer _container;
        private Settings _settings;
        
        public CustomEnemyFactory(DiContainer container, Settings settings)
        {
            _container = container;
            _settings = settings;
        }
        
        public Enemy Create(int enemy)
        {
            if (enemy == -1) enemy = Random.Range(0, _settings.EnemyPrefabs.Length);
            return _container.InstantiatePrefab(_settings.EnemyPrefabs[enemy]).GetComponent<Enemy>();
        }
        
        [System.Serializable]
        public class Settings
        {
            public Character[] EnemyPrefabs;
        }
    }
}