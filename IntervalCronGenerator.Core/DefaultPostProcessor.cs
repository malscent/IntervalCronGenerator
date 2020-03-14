using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace IntervalCronGenerator.Core
{
    public class DefaultPostProcessor : ICronPostProcessor
    {
        public string PostProcess(string cronExpression)
        {
            var sections = cronExpression.Split(" ");
            var cron = string.Empty;
            foreach (var section in sections)
            {
                cron += $"{PostProcessSection(section)} ";
            }
            return cron.TrimEnd();
        }

        private string PostProcessSection(string section)
        {
            if (section.Equals("*", StringComparison.InvariantCultureIgnoreCase))
            {
                return section;
            }

            if (section.Equals(Constants.ALL_HOURS) || section.Equals(Constants.ALL_MINUTES))
            {
                return "*";
            }

            var selections = section.Split(',');
            var selectionsAsInts = selections.Select(x => int.Parse(x)).ToList();
            var first = selectionsAsInts[0];
            var next = first;
            int diff = 0;
            foreach (var selection in selectionsAsInts)
            {
                if (selection == first)
                {
                    continue;
                }

                var newDiff = selection - next;
                if (diff == 0)
                {
                    diff = newDiff;
                    next = selection;
                    continue;
                }

                if (newDiff != diff)
                {
                    return section;
                }

                next = selection;
            }

            if (first == diff || diff == 0)
            {
                return section;
            }
            return $"{first}/{diff}";
        }
    }
}