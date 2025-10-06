using System;
using UnityEngine;

namespace Code.Enemy
{
    public class Enemy : MonoBehaviour, IDestoyable<Enemy>
    {
        [field: SerializeField] public EnemyTypes EnemyType { get; private set; }
        [field: SerializeField] public EnemyStats EnemyStats { get; private set; }

        private EnemyMover _enemyMover;

        public event Action<Enemy> OnDestroyed;


        public void Initialize(ITarget target)
        {
            _enemyMover = new EnemyMover(EnemyStats, target, transform);
        }

        private void Update()
        {
            _enemyMover.Update();
        }

        public void ResetState()
        {
        }
    }
}