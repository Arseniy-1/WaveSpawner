using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Code.Enemy;
using Code.Services;
using Code.Spawners.Enemy;
using Cysharp.Threading.Tasks;
using Sirenix.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Waves
{
    public class Wave
    {
        private readonly WaveConfig _config;

        private readonly MainEnemySpawner _mainEnemySpawner;

        private CancellationTokenSource _cancellationToken;

        private int _aliveEnemyCount;
        private bool _isWaveEnd;

        public Wave(WaveConfig config, MainEnemySpawner mainEnemySpawner)
        {
            _config = config;
            _mainEnemySpawner = mainEnemySpawner;
        }

        public event Action<Wave> WaveFinished;

        public void Begin()
        {
            _isWaveEnd = false;
            
            if (_config.EnemyStatModifiers.Value > 0)
                _mainEnemySpawner.ApplyModifier(_config.EnemyStatModifiers);

            _cancellationToken = new CancellationTokenSource();
            
            SpawningEnemies(_cancellationToken.Token).Forget();

            WaitWaveTimesOut(_cancellationToken.Token).Forget();
        }

        public void Disable()
        {
            _cancellationToken?.Cancel();
        }

        private async UniTaskVoid SpawningEnemies(CancellationToken token)
        {
            while (token.IsCancellationRequested == false || _isWaveEnd)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_config.SpawnDuration), cancellationToken: token);

                if(token.IsCancellationRequested || _isWaveEnd)
                    break;
                
                int enemyCount = Random.Range(_config.SpawnClusterSize.x, _config.SpawnClusterSize.y + 1);
                var enemyTypes = _config.EnemyTypes[Random.Range(0, _config.EnemyTypes.Count)];
                
                _aliveEnemyCount += enemyCount;

                for (int i = 0; i < enemyCount; i++)
                {
                    Enemy.Enemy enemy = _mainEnemySpawner.Spawn(enemyTypes);
                    enemy.Destroyed += HandleEnemyDeath;
                }
            }
        }

        private void HandleEnemyDeath(Enemy.Enemy enemy)
        {
            enemy.Destroyed -= HandleEnemyDeath;
            _aliveEnemyCount -= 1;
        }

        private async UniTaskVoid HandleEndWave(CancellationToken token)
        {
            await UniTask.WaitUntil(() => _aliveEnemyCount == 0);
            await UniTask.Delay(TimeSpan.FromSeconds(_config.WavePauseTime), cancellationToken: token);

            WaveFinished?.Invoke(this);
        }

        private async UniTaskVoid WaitWaveTimesOut(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_config.WaveDuration), cancellationToken: token);
            
            HandleEndWave(_cancellationToken.Token).Forget();
            _isWaveEnd = true;
        }
    }
}