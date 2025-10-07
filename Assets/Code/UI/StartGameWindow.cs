using Code.Services.SceneLoader;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI
{
    public class StartGameWindow : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        private ISceneLoader _sceneLoader;

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(HandleStartButtonClick);
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(HandleStartButtonClick);
        }

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        } 

        private void HandleStartButtonClick()
        {
            _sceneLoader.LoadScene(ScenesId.Main.ToString());
        }
    }
}