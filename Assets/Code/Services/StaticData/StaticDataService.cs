using System.Collections.Generic;
using System.Linq;
using Code.Waves;
using UnityEngine;

namespace Code.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {    
        private List<WaveConfig> _waveConfigs = new List<WaveConfig>();
        private List<Enemy.Enemy> _enemyPrefabs = new List<Enemy.Enemy>();
        
        public List<WaveConfig> WaveConfigs => _waveConfigs;
        public List<Enemy.Enemy> EnemyPrefabs => _enemyPrefabs;

        public void LoadAll()
        {
            LoadConfigs();
            LoadEnemies();
        }
        
        private void LoadConfigs()
        {
            _waveConfigs = Resources
                .LoadAll<WaveConfig>("Configs/Waves")
                .ToList();
        }
        
        private void LoadEnemies()
        {
            _enemyPrefabs = Resources
                .LoadAll<Enemy.Enemy>("EnemyPrefabs")
                .ToList();
        }
    }
}