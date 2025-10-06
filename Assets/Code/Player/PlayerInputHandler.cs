using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerInputAction _playerInput;
        
        public event Action ShootButtonPressed;

        private void Awake()
        {
            _playerInput = new PlayerInputAction();
            _playerInput.Enable();
        }

        private void OnEnable()
        {
            _playerInput.Player.Shoot.performed += OnShootPerformed;
        }

        private void OnDisable()
        {
            _playerInput.Player.Shoot.performed -= OnShootPerformed;
        }

        private void OnShootPerformed(InputAction.CallbackContext obj)
        {
            ShootButtonPressed?.Invoke();
        }
    }
}