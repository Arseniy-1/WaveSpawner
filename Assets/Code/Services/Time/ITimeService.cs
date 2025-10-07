using System;

namespace Code.Services.Time
{
    public interface ITimeService
    {
        float DeltaTime { get; }
        DateTime UtcNow { get; }
        void StopTime();
        void StartTime();
    }
}