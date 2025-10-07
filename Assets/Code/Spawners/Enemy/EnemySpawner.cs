using System.Collections.Generic;
using Code.Enemy;
using Code.Waves;
using UnityEngine;

namespace Code.Spawners.Enemy
{
    public class EnemySpawner : Spawner<Code.Enemy.Enemy>
    {
        private List<Transform> _spawnPoints;

        public EnemyTypes EnemyType => Prefab.EnemyType;

        public EnemySpawner(Code.Enemy.Enemy enemyPrefab, int startCount, IEnemyFabric enemyFabric)
        {
            StartAmount = startCount;
            var fabric = enemyFabric;

            Prefab = enemyPrefab;

            Pool = new EnemyPool(Prefab, fabric, StartAmount);
        }

        public void ApplyModifier(StatModifier statModifier)
        {
            (Pool as EnemyPool)?.AddModifier(statModifier);
        }
    }
}