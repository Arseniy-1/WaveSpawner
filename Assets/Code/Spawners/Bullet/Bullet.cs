using System;
using System.Collections;
using UnityEngine;

namespace Code.Spawners.Bullet
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour, IDestoyable<Bullet>
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;
        [SerializeField] private int _damage;
        [SerializeField] private TrailRenderer _trail;

        private Coroutine _coroutine;
        private WaitForSeconds _waitLife;

        public event Action<Bullet> Destroyed;

        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        
            _waitLife = new WaitForSeconds(_lifeTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ReturnToPool();

            if (collision.TryGetComponent(out IDamageable damagable) == false) 
                return;
        
            damagable.TakeDamage(_damage);
        }
    
        private void OnDisable()
        {
            ReturnToPool();
        }
    
        public void Activate()
        {
            _rigidbody2D.velocity = transform.right * _speed;
        }

        public void Init(Vector3 startPosition, Quaternion rotation, int damage)
        {
            _trail.Clear();
            _trail.enabled = false;
            StartCoroutine(ReenableTrailNextFrame());

            _damage = damage;
            transform.position = startPosition;
            transform.rotation = rotation;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(WaitDestroy());
        }

        private IEnumerator WaitDestroy()
        {
            yield return _waitLife;

            ReturnToPool();
        }

        private void ReturnToPool()
        {
            Destroyed?.Invoke(this);
        }
    
        private IEnumerator ReenableTrailNextFrame()
        {
            yield return null;
        
            _trail.enabled = true;
        }
    }
}