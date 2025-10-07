using System.Collections.Generic;
using Code.Services.StaticData;
using Code.Spawners.Enemy;
using Code.Waves;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private MainEnemySpawner _mainEnemySpawner;
        [SerializeField] private Player.Player _player;
        [SerializeField] private List<Transform> _spawnPoints;

        private List<WaveConfig> _waveConfigs;
        private Queue<Wave> _waves = new Queue<Wave>();
        
        [Inject]
        public void Construct(IStaticDataService staticDataService)
        {
            staticDataService.LoadAll();
            _waveConfigs = staticDataService.WaveConfigs;
            
            _mainEnemySpawner.Initialize(_spawnPoints);

            SetupWaves(_mainEnemySpawner);
            StartFistWave();
        }

        private void OnDisable()
        {
            foreach (Wave wave in _waves) 
                wave.Disable();
        }

        private void StartFistWave()
        {
            Wave wave = _waves.Dequeue();
            wave.Begin();
            wave.WaveFinished += HandleEndWave;
        }

        private void HandleEndWave(Wave wave)
        {
            wave.WaveFinished -= HandleEndWave;
            wave.Disable();
            
            if (_waves.TryDequeue(out var newWave) == false)
                return;
            
            newWave.Begin();
            newWave.WaveFinished += HandleEndWave;
        }

        private void SetupWaves(MainEnemySpawner enemySpawner)
        {
            foreach (WaveConfig waveConfig in _waveConfigs)
            {
                _waves.Enqueue(new Wave(waveConfig, enemySpawner));
            }
        }
    }
}