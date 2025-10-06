using System;
using Code.Services;
using Code.Spawners.Bullet;
using UnityEngine;

namespace Code.Enemy
{
    public class Enemy : MonoBehaviour, IDestoyable<Enemy>, IDamageable, IDieable
    {
        [field: SerializeField] public EnemyTypes EnemyType { get; private set; }
        [field: SerializeField] public EnemyStats EnemyStats { get; private set; }
        [field: SerializeField] public Destroyer Destroyer { get; private set; }

        private EnemyMover _enemyMover;

        public event Action<Enemy> OnDestroyed;


        public void Initialize(ITarget target)
        {
            _enemyMover = new EnemyMover(EnemyStats, target, transform);
            Destroyer.Initialize(EnemyStats.Health, this);
        }

        private void Update()
        {
            _enemyMover.Update();
        }

        public void ResetState()
        {
        }

        public void TakeDamage(float amount)
        {
            if (amount <= 0)
                return;

            EnemyStats.Health.TakeDamage(amount);
        }

        public void Die()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}