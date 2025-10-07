using UnityEngine;

namespace Code.Player
{
    public class Player : MonoBehaviour, ITarget, IDieable
    {
        [SerializeField] private PlayerInputHandler _inputHandler;
        [SerializeField] private PlayerWeapon _playerGun;

        public Transform TargetTransform { get; private set; }

        private void Awake()
        {
            TargetTransform = transform;
        }

        private void OnEnable()
        {
            _inputHandler.ShootButtonPressed += OnShootButtonPressed;
        }

        private void OnDisable()
        {
            _inputHandler.ShootButtonPressed -= OnShootButtonPressed;
        }

        private void OnShootButtonPressed()
        {
            _playerGun.Shoot();
        }

        public void Die()
        {
            Debug.Log("PlayerDie");
        }
    }
}