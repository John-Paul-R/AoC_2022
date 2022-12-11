namespace AoC_CSharp_2022.Day10;

enum Op
{
    NoOp,
    AddX,
}

public class Day10Impl : IDayHandler
{
    public void Go(List<string> lines)
    {
        var instructions = lines
            .Select(line =>
            {
                var parts = line.Split(' ');
                Enum.TryParse<Op>(parts[0], ignoreCase: true, out var op);
                int? amt = parts.Length > 1 ? int.Parse(parts[1]) : null;
                return (op, amt);
            })
            .ToList();



        int cycle = 1;
        int x = 1;
        var interestingSignals = new List<int>();
        var chars = new List<char>();
        void PerCycleOp()
        {
            if ((cycle - 20) % 40 == 0) {
                interestingSignals.Add(cycle * x);
            }

            var curPixel = cycle % 40;
            if (curPixel <= x + 2 && curPixel >= x) {
                chars.Add('#');
            }
            else {
                chars.Add('.');
            }

            if (cycle % 40 == 0) {
                chars.Add('\n');
            }
        }
        foreach (var (op, amt) in instructions) {
            PerCycleOp();

            switch (op) {
                case Op.NoOp:
                    break;
                case Op.AddX:
                    cycle++;
                    PerCycleOp();
                    x += amt.Value;
                    break;
            }

            cycle++;
        }

        Console.WriteLine(interestingSignals.Sum());
        Console.WriteLine(string.Concat(chars));

    }
}
