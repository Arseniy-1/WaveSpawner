using Code.Services.SceneLoader;
using Code.UI;
using UnityEngine;
using Zenject;

namespace Code.Player
{
    public class Player : MonoBehaviour, ITarget, IDieable
    {
        [SerializeField] private PlayerInputHandler _inputHandler;
        [SerializeField] private PlayerWeapon _playerGun;
        
        private ISceneLoader _sceneLoader;

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

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Die()
        {
            _sceneLoader.LoadScene(ScenesId.Main.ToString());
        }

        private void OnShootButtonPressed()
        {
            _playerGun.Shoot();
        }
    }
}