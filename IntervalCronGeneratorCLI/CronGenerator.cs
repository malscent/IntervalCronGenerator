using System;
using IntervalCronGenerator.Core;

namespace IntervalCronGeneratorCLI
{
    public class CronCLI
    {
        public void ConvertInterval(int interval, Units unit)
        {
            var icg = new CronGenerator();
            var results = icg.ConvertInterval(interval, unit);
            int index = 0;
            Console.WriteLine("Cron Expressions");
            Console.WriteLine("----------------");
            foreach (var result in results)
            {
                WriteLine(result, index);
                index++;
            }
        }

        private void WriteLine(string s, int index)
        {
            if (index % 2 == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine(s);
            Console.ResetColor();
        }
    }
}