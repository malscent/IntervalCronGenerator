using System;
using System.Collections.Generic;

namespace IntervalCronGenerator.Core
{
    public class CronGenerator : IIntervalCronGenerator
    {
        private readonly SecondIntervalToCronConverter _converter = new SecondIntervalToCronConverter();
        
        /// <summary>
        /// Converts an interval into a set of cron expressions representing that interval
        /// </summary>
        /// <param name="interval">The amount of time between executions</param>
        /// <param name="unit">Hours, Minutes or Seconds</param>
        /// <returns>Returns a collection of cron expressions</returns>
        public IEnumerable<string> ConvertInterval(int interval, Units unit)
        {
            IEnumerable<string> values;
            switch (unit)
            {
                case Units.Hour:
                case Units.Hours:
                case Units.HOUR:
                case Units.HOURS:
                case Units.hour:
                case Units.hours:
                    values = _converter.ConvertToCronTabExpression(interval * Constants.SECONDS_IN_A_HOUR);
                    break;
                case Units.Minute:
                case Units.Minutes:
                case Units.MINUTE:
                case Units.MINUTES:
                case Units.minute:
                case Units.minutes:
                    values = _converter.ConvertToCronTabExpression(interval * Constants.SECONDS_IN_A_MINUTE);
                    break;
                default:
                    values = _converter.ConvertToCronTabExpression(interval);
                    break;
            }

            return values;
        }
        /// <summary>
        /// Converts an interval of seconds into a set of cron expressions.
        /// </summary>
        /// <param name="seconds">The number of seconds between executions</param>
        /// <returns>Returns a collection of cron expressions</returns>
        public IEnumerable<string> ConvertIntervalSeconds(int seconds)
        {
            return _converter.ConvertToCronTabExpression(seconds);
        }
        /// <summary>
        /// Converts an interval of seconds into a set of cron expressions
        /// </summary>
        /// <param name="minutes">The number of minutes between executions</param>
        /// <returns>Returns a collection of cron expressions</returns>
        public IEnumerable<string> ConvertIntervalMinutes(int minutes)
        {
            return _converter.ConvertToCronTabExpression(minutes * Constants.SECONDS_IN_A_MINUTE);
        }
        /// <summary>
        /// Converts an interval of hours into a set of cron expressions.
        /// </summary>
        /// <param name="hours">The number of hours between executions</param>
        /// <returns>Returns a collection of cron expressions</returns>
        public IEnumerable<string> ConvertIntervalHours(int hours)
        {
            return _converter.ConvertToCronTabExpression(hours * Constants.SECONDS_IN_A_HOUR);
        }
        /// <summary>
        /// Converts an interval of hours, minutes and seconds to a set of cron expressions
        /// </summary>
        /// <param name="hours">The number of hours between executions</param>
        /// <param name="minutes">The number of minutes between executions</param>
        /// <param name="seconds">The number of seconds between executions</param>
        /// <returns>Returns a collection of cron expressions</returns>
        public IEnumerable<string> ConvertInterval(int hours, int minutes, int seconds)
        {
            return _converter.ConvertToCronTabExpression((hours * Constants.SECONDS_IN_A_HOUR) +
                                                         (minutes * Constants.SECONDS_IN_A_MINUTE) + seconds);
        }
        /// <summary>
        /// Converts a timespan interval into a set of cron expressions
        /// </summary>
        /// <param name="interval">The timespan representation of the interval</param>
        /// <returns>Returns a collection of cron expressions</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if timespan seconds are longer than a day.</exception>
        public IEnumerable<string> ConvertInterval(TimeSpan interval)
        {
            if (interval.TotalSeconds > Constants.SECONDS_IN_A_DAY)
            {
                throw new ArgumentOutOfRangeException(nameof(interval));
            }
            return _converter.ConvertToCronTabExpression(Convert.ToInt32(interval.TotalSeconds));
        }
    }
}