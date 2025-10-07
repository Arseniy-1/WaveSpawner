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
        [SerializeField] private CollisionDetector _collisionDetector;
        
        private EnemyMover _enemyMover;

        public event Action<Enemy> Destroyed;

        private void OnEnable()
        {
            _collisionDetector.CollisionDetected += OnCollisionDetected;
        }

        private void OnDisable()
        {
            _collisionDetector.CollisionDetected -= OnCollisionDetected;
        }

        public void Initialize(ITarget target)
        {
            _enemyMover = new EnemyMover(EnemyStats, target, transform);
            Destroyer.Initialize(EnemyStats.Health, this);
            EnemyStats.Initialize();
        }

        private void Update()
        {
            _enemyMover.Update();
            EnemyStats.Update();
        }

        public void TakeDamage(float amount)
        {
            if (amount <= 0)
                return;

            EnemyStats.Health.TakeDamage(amount);
        }

        public void Die()
        {
            Destroyed?.Invoke(this);
        }

        private void OnCollisionDetected(Collider2D collider2D)
        {
            if (collider2D.TryGetComponent(out Player.Player player)) 
                player.Die();
        }
    }
}