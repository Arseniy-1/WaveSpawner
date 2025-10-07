using System.Collections.Generic;
using System.Linq;
using Code.Enemy;
using Code.Services.StaticData;
using Code.Waves;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Spawners.Enemy
{
    public class MainEnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<Code.Enemy.Enemy> _enemyPrefabs;
        [SerializeField] private int _startPoolCount;
        [SerializeField] private float _spawnOffset;

        private IReadOnlyList<Transform> _spawnPoints;

        private readonly Dictionary<EnemyTypes, EnemySpawner> _spawners = new();

        public void Initialize(IReadOnlyList<Transform> spawnPoints)
        {
            _spawnPoints = spawnPoints; 
        }

        [Inject]
        public void Construct(IStaticDataService staticData, IEnemyFabric enemyFabric)
        {
            staticData.LoadAll();
            _enemyPrefabs = staticData.EnemyPrefabs;
            
            foreach (var enemySpawner in _enemyPrefabs
                         .Select(enemyPrefab => new EnemySpawner(enemyPrefab, _startPoolCount, enemyFabric)))
            {
                _spawners.Add(enemySpawner.EnemyType, enemySpawner);
            }
        }

        public Code.Enemy.Enemy Spawn(EnemyTypes type)
        {
            var spawner = _spawners[type];

            Code.Enemy.Enemy enemy = spawner.Spawn();
        
            MoveEnemy(enemy);

            return enemy;
        }

        public void ApplyModifier(StatModifier modifier)
        {
            foreach (EnemySpawner enemySpawner in _spawners.Values)
            {
                enemySpawner.ApplyModifier(modifier);
            }
        }

        private void MoveEnemy(Code.Enemy.Enemy enemy)
        {
            Vector2 spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;
        
            enemy.transform.position = Random.insideUnitCircle * _spawnOffset + spawnPoint;
        }
    }
}