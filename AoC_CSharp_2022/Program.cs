// C#

using AoC_CSharp_2022;
using AoC_CSharp_2022.Day09;
using AoC_CSharp_2022.Day10;
using AoC_CSharp_2022.Day11;

const int day = 11;
const bool useExample = false;
const bool useRevised = true;

List<string> lines = File
    .ReadAllLines($"./Day{day}/{(useExample ? "example" : "input")}")
    .ToList();

IDayHandler handler = day switch
{
    9 => new Day9Impl(),
    10 => new Day10Impl(),
    11 => useRevised
        ? new AoC_CSharp_2022.Day11.Revised.Day11Impl()
        : new Day11Impl(),
};

// Console.WriteLine("Example: ");
handler.Go(lines);

// Console.WriteLine("Real:");
// handler.Go(inputLines);
