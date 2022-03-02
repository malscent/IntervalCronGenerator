using System;
using System.Collections.Generic;
using System.Linq;
using IntervalCronGenerator.Core;
using Xunit;

namespace IntervalCronGeneratorTests
{
    public class IntervalCronGeneratorTests
    {
        private readonly IIntervalCronGenerator _generator = new CronGenerator();
        [Fact]
        public void GeneratesCorrect20SecondInterval()
        {
            var results = _generator.ConvertIntervalSeconds(20);
            Assert.Contains(results, s => s == "0/20 * * * * *");
        }

        [Fact]
        public void GeneratesCorrect25SecondInterval()
        {
            var results = _generator.ConvertIntervalSeconds(25);
            var expected = new List<string>
            {
                "0/25 0/5 * * * *",
                "15/25 1/5 * * * *",
                "5/25 2/5 * * * *",
                "20/25 3/5 * * * *",
                "10/25 4/5 * * * *"
            };
            var diffCount = expected.Intersect(results).Count();
            Assert.Equal(5, diffCount);
        }
        
        [Fact]
        public void GeneratesCorrect30MinuteInterval()
        {
            var results = _generator.ConvertIntervalMinutes(30);
            Assert.Contains(results, s => s == "0 0/30 * * * *");
        }
        [Fact]
        public void GeneratesCorrect2HourInterval()
        {
            var results = _generator.ConvertIntervalHours(2);
            Assert.Contains(results, s => s == "0 0 0/2 * * *");
        }
        
        [Fact]
        public void GeneratesCorrectFromTimespan()
        {
            var ts = new TimeSpan(0, 0, 30);
            var results = _generator.ConvertInterval(ts);
            Assert.Contains(results, s => s == "0/30 * * * * *");
        }

        [Fact]
        public void GeneratesCorrectFromHoursMinutes()
        {
            var results = _generator.ConvertInterval(1, 30, 0).ToArray();
            
            Assert.Contains(results, s => s == "0 0 0/3 * * *");
            Assert.Contains(results, s => s == "0 30 1/3 * * *");
        }        
    }
}