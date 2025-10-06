using System.Collections.Generic;
using Code.Waves;

namespace Code.Spawners.Enemy
{
    public class EnemyPool : Pool<Code.Enemy.Enemy>
    {
        private readonly EnemyFabric _enemyFabric;
        private readonly List<Code.Enemy.Enemy> _enemies = new();
        private readonly List<StatModifier> _modifiers = new();

        public EnemyPool(Code.Enemy.Enemy prefab, EnemyFabric enemyFabric, int startAmount) : base(prefab, startAmount)
        {
            _enemyFabric = enemyFabric;
        
            CreateStartCount();
        }

        public void AddModifier(StatModifier statModifier)
        {
            _modifiers.Add(statModifier);
        
            foreach (Code.Enemy.Enemy enemy in _enemies)
            {
                ApplyModifier(enemy, statModifier);
            }
        }
        
        protected override Code.Enemy.Enemy Create()
        {
            var enemy = _enemyFabric.Create(Prefab);

            foreach (StatModifier modifier in _modifiers)
            {
                ApplyModifier(enemy, modifier);
            }

            enemy.gameObject.SetActive(false);
            _enemies.Add(enemy);

            return enemy;
        }

        private void ApplyModifier(Code.Enemy.Enemy enemy, StatModifier statModifier)
        {
            enemy.EnemyStats.Speed.AddModifier(statModifier.Copy());
            enemy.EnemyStats.Health.AddModifier(statModifier.Copy());
        }
    }
}