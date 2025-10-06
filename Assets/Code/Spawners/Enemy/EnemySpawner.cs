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

        public EnemySpawner(Code.Enemy.Enemy enemyPrefab, Player.Player player, int startCount)
        {
            StartAmount = startCount;
            var fabric = new EnemyFabric(player);

            Prefab = enemyPrefab;

            Pool = new EnemyPool(Prefab, fabric, StartAmount);
        }

        public void ApplyModifier(StatModifier statModifier)
        {
            (Pool as EnemyPool)?.AddModifier(statModifier);
        }
    }
}