using System;

namespace CraftTable.Contracts
{
    public static class Synth
    {
        public static SynthDelegate FromRawValue(int value)
        {
            return (a, b, c, d) => value;
        }

        public static SynthDelegate FrompPercent(int percent)
        {
            return (a, b, c, d) => (int)((a.Difficulty - c) * (double)percent / 100); ;
        }

        public static SynthDelegate FromEfficiency(int value)
        {
            return (a, b, c, d) => Math.Min(a.Difficulty - c, d.CalculateProgress(value, b.Craftmanship, a.Level - a.Level));
        }
    }
}