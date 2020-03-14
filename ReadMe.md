# Interval Cron Generator
## Simplify the generation of multiple cron expressions to represent an interval

[![Nuget Package](https://badgen.net/nuget/v/IntervalCronGenerator.core)](https://www.nuget.org/packages/IntervalCronGenerator.Core/)
[![CLI](https://badgen.net/nuget/v/IntervalCronGeneratorCLI)](https://www.nuget.org/packages/IntervalCronGeneratorCLI/)
![Test](https://github.com/malscent/IntervalCronGenerator/workflows/Test/badge.svg)
![Publish Nuget Packages](https://github.com/malscent/IntervalCronGenerator/workflows/Publish%20Nuget%20Packages/badge.svg)

## Introduction

Have you ever needed to create a cron expression to represent some interval, that cannot be simply represented with a single cron expression?  What does a 45 second cron expression look like?

```
0/45 0/3 * * * *
30 1/3 * * * *
15 2/3 * * * *
```

Why not `0/45 * * * *`, You might ask?  Because cron will execute at the 0th second of each minute, causing your second interval to only be 15 seconds which isn't what you wanted!

Generating these cron expressions can be tedious and error prone.  So I wrote IntervalCronGenerator.

## Usage

IntervalCronGenerator has a CLI and a Nuget Package.   You can either install the nuget package in your application and use the CronGenerator class or install the tool CLI.

```
dotnet add package IntervalCronGenerator.Core
dotnet tool install IntervalCronGeneratorCLI --global
```

CronGenerator implements the following methods for converting your interval to a set of cron expressions:

```
        IEnumerable<string> ConvertInterval(int interval, Units unit);
        IEnumerable<string> ConvertIntervalSeconds(int seconds);
        IEnumerable<string> ConvertIntervalMinutes(int minutes);
        IEnumerable<string> ConvertIntervalHours(int hours);
        IEnumerable<string> ConvertInterval(int hours, int minutes, int seconds);
        IEnumerable<string> ConvertInterval(TimeSpan interval);
```

Simply choose the one that matches the time frame you have.

To uses the CLI.

![cli_usage](https://imgur.com/qikPM4R.jpg)

* Install the tool
`dotnet tool install IntervalCronGeneratorCLI --global`
* Run the tool with `icg <interval> <Unit> <command> (OPTIONAL) --seconds (OPTIONAL)`
* Running `icg --help` will provide help information regarding the available command.

Interval: the amount of time between executions.

Units: Hour, Minute, or Second (Case Insensitive'ish).

Command (OPTIONAL): command you want to execute on the interval (For copy/paste to Crontab)

--seconds (-s)(OPTIONAL): specify whether to return the seconds column or not.  Not all cron implementations support seconds.

_Cannot use "seconds" unit without --seconds or -s_ 

## Caveats

This will only guarantee executions up to 24 hours.  If your interval does not evenly divide into 86,400 seconds, then the duration of the last execution of the day and the first of the following day will be shorter than the specified interval.  This may change in the future, but for now, keep this in mind when choosing your interval.

## Contributions
This application was written using .Net Core 3.1.  You will need that installed.

Submitions, issues, and PRs are welcome.  Please run the unit tests before creating a PR.

Enjoy


