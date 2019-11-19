using System;

namespace DataStructures
{
    public class ResultsPageData
    {
        public int Total { get; set; }
        public int HighCount { get; set; }
        public double HighConfidence { get; set; }
        public int LowCount { get; set; }
        public double LowConfidence { get; set; }
    }
}