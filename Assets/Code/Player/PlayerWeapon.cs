using Code.Services.Random;
using Code.Spawners.Bullet;
using UnityEngine;
using Zenject;

namespace Code.Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] protected Bullet BulletPrefab;
        [SerializeField] protected Transform ShootPoint;
        [SerializeField] protected AmmoSpawner AmmoSpawner;
        [SerializeField] private float _bulletReloadTime;
        [SerializeField] private int _damage;
        [SerializeField] private float _spread;

        private float _currentTime;
        private bool _isReloaded;
        
        private IRandomService _random;

        protected virtual void Awake()
        {
            AmmoSpawner = new AmmoSpawner(BulletPrefab);
        }

        private void Update()
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mouseWorldPosition - transform.position;
    
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        
        private void FixedUpdate()
        {
            if (_currentTime < _bulletReloadTime && !_isReloaded)
                _currentTime += Time.deltaTime;

            if (_currentTime >= _bulletReloadTime)
                Reload();
        }

        [Inject]
        public void Construct(IRandomService randomService)
        {
            _random = randomService;
        }

        public void Shoot()
        {
            if(_isReloaded == false)
                return;

            _isReloaded = false;
            
            Bullet bullet = AmmoSpawner.Spawn();
            bullet.Init(ShootPoint.position, GetBulletDirection(), _damage);

            bullet.Activate();
        }

        private void Reload()
        {
            _currentTime = 0;
            _isReloaded = true;
        }

        private Quaternion GetBulletDirection()
        {
            Quaternion rotation = transform.rotation;
            rotation.z += _random.Range(-_spread, _spread);

            return rotation;
        }
    }
}