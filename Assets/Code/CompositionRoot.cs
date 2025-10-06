using System;
using System.Collections.Generic;
using Project.Scripts.ArenaSystem;
using UnityEngine;

namespace Code
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private MainEnemySpawner _mainEnemySpawner;
        [SerializeField] private Player _player;
        [SerializeField] private List<Transform> _spawnPoints;

        [SerializeField] private WaveConfig _waveConfig;
        private Wave _wave;

        private void Awake()
        {
            _mainEnemySpawner.Initialize(_player, _spawnPoints);
            
            _wave = new Wave(_waveConfig, _mainEnemySpawner);
            _wave.Begin();
        }
    }
}