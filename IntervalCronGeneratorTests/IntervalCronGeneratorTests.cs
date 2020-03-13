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
            Assert.Contains(results, s => s == "0,20,40 * * * * *");
        }

        [Fact]
        public void GeneratesCorrect25SecondInterval()
        {
            var results = _generator.ConvertIntervalSeconds(25);
            var expected = new List<string>
            {
                "0,25,50 0,5,10,15,20,25,30,35,40,45,50,55 * * * *",
                "15,40 1,6,11,16,21,26,31,36,41,46,51,56 * * * *",
                "5,30,55 2,7,12,17,22,27,32,37,42,47,52,57 * * * *",
                "20,45 3,8,13,18,23,28,33,38,43,48,53,58 * * * *",
                "10,35 4,9,14,19,24,29,34,39,44,49,54,59 * * * *"
            };
            var diffCount = expected.Intersect(results).Count();
            Assert.Equal(5, diffCount);
        }
        
        [Fact]
        public void GeneratesCorrect30MinuteInterval()
        {
            var results = _generator.ConvertIntervalMinutes(30);
            Assert.Contains(results, s => s == "0 0,30 * * * *");
        }
        [Fact]
        public void GeneratesCorrect2HourInterval()
        {
            var results = _generator.ConvertIntervalHours(2);
            Assert.Contains(results, s => s == "0 0 0,2,4,6,8,10,12,14,16,18,20,22 * * *");
        }
        
        [Fact]
        public void GeneratesCorrectFromTimespan()
        {
            var ts = new TimeSpan(0, 0, 30);
            var results = _generator.ConvertInterval(ts);
            Assert.Contains(results, s => s == "0,30 * * * * *");
        }

        [Fact]
        public void GeneratesCorrectFromHoursMinutes()
        {
            var results = _generator.ConvertInterval(1, 30, 0);
            
            Assert.Contains(results, s => s == "0 0 0,3,6,9,12,15,18,21 * * *");
            Assert.Contains(results, s => s == "0 30 1,4,7,10,13,16,19,22 * * *");
        }        
    }
}