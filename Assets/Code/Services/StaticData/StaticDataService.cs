using System.Collections.Generic;
using System.Linq;
using Code.Waves;
using UnityEngine;

namespace Code.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {    
        private List<WaveConfig> _waveConfigs = new List<WaveConfig>();
        
        public List<WaveConfig> WaveConfigs => _waveConfigs;

        public void LoadAll()
        {
            LoadConfigs();
        }
        
        private void LoadConfigs()
        {
            _waveConfigs = Resources
                .LoadAll<WaveConfig>("Resources/Configs")
                .ToList();
        }
    }
}