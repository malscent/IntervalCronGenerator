using System.Collections.Generic;
using System.Linq;

namespace IntervalCronGenerator.Core
{
    internal class SecondIntervalToCronConverter
    {
        public IEnumerable<string> ConvertToCronTabExpression(int seconds)
        {
            List<string> expressions = new List<string>();
            Dictionary<int, Dictionary<int, List<int>>> executions = new Dictionary<int, Dictionary<int, List<int>>>();
            int elapsed = 0;
            // iterate elapsed throughout a day and generate the list of executions
            while (elapsed  < Constants.SECONDS_IN_A_DAY)
            {
                var hour = elapsed / 3600;
                var minute = elapsed / 60 - (hour * 60);
                var second = elapsed % 60;
                AddOrUpdate(ref executions, hour, minute, second);
                elapsed += seconds;
            }
            Dictionary<string, string> combos = new Dictionary<string, string>();
            
            // here we're doing some dedupe work to find execution patterns that are the same on an hour basis
            foreach (var hour in executions)
            {
                // need to dedupe the minutes as some minutes will have the same executions
                List<KeyValuePair<string, List<int>>> combinations = new List<KeyValuePair<string, List<int>>>();
                foreach (var minute in hour.Value)
                {
                    var added = false;
                    var kvp = new KeyValuePair<string,List<int>>();
                    var key = string.Empty;
                    foreach (var combo in combinations)
                    {
                        if (combo.Value.Intersect(minute.Value).Count() == minute.Value.Count)
                        {
                            kvp = new KeyValuePair<string,List<int>>
                            (
                                $"{combo.Key},{minute.Key}",
                                combo.Value
                            );

                            key = combo.Key;
                            added = true;
                            break;
                        }
                    }

                    if (!added)
                    {
                        kvp = new KeyValuePair<string, List<int>>(minute.Key.ToString(), minute.Value);
                    }
                    else
                    {
                        combinations.RemoveAll(x => x.Key == key);
                    }
                    combinations.Add(kvp);
                }
                //setting our combinations in a dictionary we can modify.
                foreach (var combo in combinations)
                {
                    var value = Constants.HOURS_PLACEHOLDER.Replace(Constants.HOURS_PLACEHOLDER, hour.Key.ToString());
                    var key = $"{Constants.SECONDS_PLACEHOLDER} {Constants.MINUTES_PLACEHOLDER}"
                                        .Replace(Constants.MINUTES_PLACEHOLDER, combo.Key)
                                        .Replace(Constants.SECONDS_PLACEHOLDER, string.Join(",", combo.Value));;

                    if (combos.ContainsKey(key))
                    {
                        combos[key] = $"{combos[key]},{value}";
                    }
                    else
                    {
                        combos.Add(key, value);
                    }
                }
            }
            foreach (var combo in combos)
            {
                var expression = $"{combo.Key} {combo.Value} * * *";
                expressions.Add(expression);
            }

            return expressions;
        }

        private void AddOrUpdate(ref Dictionary<int, Dictionary<int, List<int>>> executions, in int hour, in int minute, in int second)
        {
            if (!executions.ContainsKey(hour))
            {
                executions.Add(hour, new Dictionary<int, List<int>>());
            }

            if (!executions[hour].ContainsKey(minute))
            {
                executions[hour].Add(minute, new List<int>());
            }

            if (!executions[hour][minute].Contains(second))
            {
                executions[hour][minute].Add(second);
            }
        }
    }
}