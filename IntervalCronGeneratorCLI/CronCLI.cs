using System;
using IntervalCronGenerator.Core;
using CommandDotNet;

namespace IntervalCronGeneratorCLI
{
    public class CronCLI
    {
        [DefaultMethod]
        [Command(Name = "convert", Usage = "convert <int> <unit> <command> (OPTIONAL)", Description="Converts the interval into a set of cron expressions")]    
        public void ConvertInterval(
            [Operand(Name = "interval", 
                Description = "The value of the interval")]
            int interval, 
            [Operand(Name ="unit", 
                Description = "The unit in seconds, minutes or hours")]
            Units unit, 
            [Operand(Name ="command", Description ="The optional command to add to the end of the cron expression")]
            string command = "",
            [Option(LongName = "seconds", ShortName = "s", Description = "Optional to use seconds. Defaults without.")]
            bool seconds = false)
        {
            if (!seconds && isSeconds(unit))
            {
                Console.WriteLine("You must use the -s or --seconds flag to enable second precision intervals.");
                return;
            }
            var icg = new CronGenerator();
            var results = icg.ConvertInterval(interval, unit);
            int index = 0;
            var headerLine = (string.IsNullOrWhiteSpace(command))
                ? "# Cron Expressions"
                : $"# Cron Expressions for {command}";
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"# ----------------------------------------------------------------");
            Console.WriteLine(headerLine);
            Console.WriteLine($"# To be executed on {interval} {unit.ToString()} intervals");
            Console.WriteLine($"# ----------------------------------------------------------------");
            Console.ResetColor();
            foreach (var result in results)
            {
                WriteLine(result, index, command, seconds);
                index++;
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"# End {interval} {unit.ToString()}");
            Console.WriteLine($"# ----------------------------------------------------------------");
            Console.ResetColor();
        }

        private bool isSeconds(Units unit)
        {
            return unit == Units.second ||
                   unit == Units.seconds ||
                   unit == Units.Second ||
                   unit == Units.Seconds ||
                   unit == Units.SECOND ||
                   unit == Units.SECONDS;
        }

        private void WriteLine(string s, int index, string command = "", bool seconds = false)
        {
            if (index % 2 == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            var value = (seconds) ? s : s.Substring(s.IndexOf(" ", StringComparison.Ordinal) + 1);
            if (!string.IsNullOrWhiteSpace(command))
            {
                Console.WriteLine($"{value} {command}");
            }
            else
            {
                Console.WriteLine($"{value}");
            }
            Console.ResetColor();
        }
    }
}