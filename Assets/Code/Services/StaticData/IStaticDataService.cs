using System.Collections.Generic;
using Code.Waves;

namespace Code.Services.StaticData
{
    public interface IStaticDataService
    {
        void LoadAll();
        List<WaveConfig> WaveConfigs { get; }
    }
}