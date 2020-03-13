using System;

namespace IntervalCronGenerator.Core
{
    public enum Units
    {
        Hour, Minute, Second
    }

    public class Constants
    {
        public const string MINUTES_PLACEHOLDER = "<MINUTES_PLACEHOLDER>";
        public const string HOURS_PLACEHOLDER = "<HOURS_PLACEHOLDER>";
        public const string SECONDS_PLACEHOLDER = "<SECONDS_PLACEHOLDER>";
        public const string ALL_HOURS = "0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23";

        public const string ALL_MINUTES =
            "0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59";
        public const int SECONDS_IN_A_DAY = 86400;
        public const int SECONDS_IN_A_HOUR = 3600;
        public const int SECONDS_IN_A_MINUTE = 60;

    }
}