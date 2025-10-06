using Code.Services;
using Code.Stats;
using UnityEngine;

namespace Code.Enemy
{
    public class EnemyMover : IUpdateable
    {
        private IMoverStats _moverStats;
        private ITarget _target;
        private Transform _holder;

        public void Update()
        {
            var moveDirection = (_target.TargetTransform.position - _holder.position).normalized;

            _holder.position += moveDirection * (_moverStats.Speed.CurrentValue * Time.deltaTime);
        }

        public EnemyMover(IMoverStats moverStats, ITarget target, Transform holder)
        {
            _moverStats = moverStats;
            _target = target;
            _holder = holder;
        }
    }
}