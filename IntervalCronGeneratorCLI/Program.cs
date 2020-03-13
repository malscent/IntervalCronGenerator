using System;
using CommandDotNet;

namespace IntervalCronGeneratorCLI
{
    class Program
    {
        static int Main(string[] args)
        {
            return new AppRunner<CronCLI>().Run(args);
        }
    }
}