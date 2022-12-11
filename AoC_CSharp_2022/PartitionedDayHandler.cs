using System.Diagnostics;

namespace AoC_CSharp_2022;

public abstract class PartitionedDayHandler : IDayHandler
{
    public void Go(List<string> lines)
    {
        Console.WriteLine("--- Part 1 ---");
        var stopwatch = Stopwatch.StartNew();
        Part1(lines);
        var pt1Time = stopwatch.Elapsed;
        Console.WriteLine("--- End Part 1 ({0} ms) ---", pt1Time.TotalMilliseconds);
        Console.WriteLine("--- Part 2 ---");
        stopwatch.Restart();
        Part2(lines);
        var pt2Time = stopwatch.Elapsed;
        Console.WriteLine("--- End Part 2 ({0} ms) ---", pt2Time.TotalMilliseconds);
    }

    protected abstract void Part1(List<string> lines);

    protected abstract void Part2(List<string> lines);
}
