using System;
using System.Collections.Generic;

namespace IntervalCronGenerator.Core
{
    public interface IIntervalCronGenerator
    {
        IEnumerable<string> ConvertInterval(int interval, Units unit);
        IEnumerable<string> ConvertIntervalSeconds(int seconds);
        IEnumerable<string> ConvertIntervalMinutes(int minutes);
        IEnumerable<string> ConvertIntervalHours(int hours);
        IEnumerable<string> ConvertInterval(int hours, int minutes, int seconds);
        IEnumerable<string> ConvertInterval(TimeSpan interval);
    }
}