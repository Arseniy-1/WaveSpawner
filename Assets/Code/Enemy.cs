using System;
using Code;
using UnityEngine;

public class Enemy : MonoBehaviour, IDestoyable<Enemy>
{
    [field: SerializeField] public EnemyTypes EnemyType { get; private set; }
    [field: SerializeField] public EnemyStats EnemyStats { get; private set; }
    
    [SerializeField] private float _speed;
    
    private ITarget _target;

    public event Action<Enemy> OnDestroyed;

    
    private void Update()
    {
        var moveDirection = (_target.TargetTransform.position - transform.position).normalized;
        
        transform.position += moveDirection * (_speed * Time.deltaTime);
    }

    public void Initialize(ITarget target)
    {
        _target = target;
    }

    public void ResetState()
    {
    }
}