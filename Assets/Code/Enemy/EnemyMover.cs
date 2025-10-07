using Code.Services;
using Code.Services.Time;
using Code.Stats;
using UnityEngine;
using Zenject;

namespace Code.Enemy
{
    public class EnemyMover : IUpdateable
    {
        private IMoverStats _moverStats;
        private ITarget _target;
        private Transform _holder;
        private ITimeService _time;

        public EnemyMover(IMoverStats moverStats, ITarget target, Transform holder)
        {
            _moverStats = moverStats;
            _target = target;
            _holder = holder;
        }

        [Inject]
        public void Construct(ITimeService time)
        {
            _time = time;
        }
        
        public void Update()
        {
            var moveDirection = (_target.TargetTransform.position - _holder.position).normalized;

            _holder.position += moveDirection * (_moverStats.Speed.CurrentValue * _time.DeltaTime);
        }
    }
}