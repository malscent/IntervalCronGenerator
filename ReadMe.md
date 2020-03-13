# Interval Cron Generator
## Simplify the generation of multiple cron expressions to represent an interval

[![Nuget Package](https://badgen.net/nuget/v/IntervalCronGenerator.core)](https://www.nuget.org/packages/IntervalCronGenerator.Core/)
[![CLI](https://badgen.net/nuget/v/IntervalCronGeneratorCLI)](https://www.nuget.org/packages/IntervalCronGeneratorCLI/)
## Introduction

Have you ever needed to create a cron expression to represent some interval, that cannot be simply represented with a single cron expression?  What does a 45 second cron expression look like?

```
0,45 0,3,6,9,12,15,18,21,24,27,30,33,36,39,42,45,48,51,54,57 * * * *
30 1,4,7,10,13,16,19,22,25,28,31,34,37,40,43,46,49,52,55,58 * * * *
15 2,5,8,11,14,17,20,23,26,29,32,35,38,41,44,47,50,53,56,59 * * * *
```

Generating these cron expressions can be tedious and error prone.  So I wrote IntervalCronGenerator.

## Usage

IntervalCronGenerator has a CLI and a Nuget Package.   You can either install the nuget package in your application and use the CronGenerator class or install the tool CLI.

```
dotnet add package IntervalCronGenerator.Core --version 1.0.1
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

![cli_usage](https://imgur.com/SoQ70oN.jpg)

* Install the tool
`dotnet tool install IntervalCronGeneratorCLI --global`
* Run the tool with `icg <interval> <Unit>`
* Running `icg --help` will provide help information regarding the available command.

Interval is the amount of time between executions.
Units can be Hour, Minute, or Second (Case Insensitive'ish).

## Caveats

This will only guarantee executions up to 24 hours.  If your interval does not evenly divide into 86,400 seconds, then the duration of the last execution of the day and the first of the following day will be shorter than the specified interval.  This may change in the future, but for now, keep this in mind when choosing your interval.

## Contributions
This application was written using .Net Core 3.1.  You will need that installed.

Submitions, issues, and PRs are welcome.  Please run the unit tests before creating a PR.

Enjoy


