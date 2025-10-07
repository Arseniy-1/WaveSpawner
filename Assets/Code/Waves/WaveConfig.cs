using System.Collections.Generic;
using Code.Enemy;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Waves
{
    [CreateAssetMenu(fileName = "New Wave Config", menuName = "Wave/Create new wave config", order = 51)]
    public class WaveConfig : SerializedScriptableObject
    {
        [SerializeField] private List<EnemyTypes> _enemyTypes;
        [SerializeField] [Range(1, 1000)] private int _waveDuration;
        [SerializeField] [Range(0.01f, 10)] private float _spawnDuration;
        [SerializeField] [Range(3, 30)] private int _wavePauseTime;
        [SerializeField] [MinMaxSlider(1, 15)] private Vector2Int _spawnClusterSize;
        
        [SerializeField] private StatModifier _enemyStatModifiers;
        
        public List<EnemyTypes> EnemyTypes => _enemyTypes;
        public int WaveDuration => _waveDuration;
        public Vector2Int SpawnClusterSize => _spawnClusterSize;
        public int WavePauseTime => _wavePauseTime;
        public float SpawnDuration => _spawnDuration;
        public StatModifier EnemyStatModifiers => _enemyStatModifiers;
    }
}