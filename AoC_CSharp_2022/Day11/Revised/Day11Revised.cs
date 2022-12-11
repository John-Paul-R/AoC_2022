using System.Linq.Expressions;

namespace AoC_CSharp_2022.Day11.Revised;

// Note, for this one, I used the reddit to identify 2 things. I likely would
// not have completed this one in any reasonable period of time otherwise.
// Those things are:
//   - My program worked correctly for the `example`, but not for the `input`
//     because of int overflow. (Using `long` over `int` allowed part1 to pass)
//     - Idk how I missed this. Spent too much time thinking my program's logic
//       was wrong. Was looking in all the wrong places ig. (I never saw a
//       negative value.) Takeaway: when in doubt, JSON serialize entire objects.
//       I was looking solely at debugger values, and didn't think to look in
//       the relevant places at the appropriate times. A sky-high view would've
//       helped.
//   - (a % kn) % n = a % n
//     Using this fact, you can keep the "worry" levels manageable.
//     What this effectively means: "you can safely `mod` the `worry` value by
//     a common multiple of the monkeys' "testNum" values. (the numbers they
//     check for divisibility with)
//     - The other piece of information necessary here, which I sort of
//       intuitively guessed, but was not confident on (idk the actual math
//       here), is:
//         (ka % n = 0) = (k (a % n) = 0)
//         ((a + k) % n = 0) = (k + (a % n) = 0)

class Monkey
{
    private readonly Func<long, long> _operation;
    private readonly Func<long, int> _test;
    public long TestNum { get; }

    public long InspectionCount { get; private set; } = 0;
    public int Id { get; }
    public List<long> Inventory { get; }

    public Monkey(
        int id,
        List<long> startingItems,
        Func<long, long> operation,
        Func<long, int> test, // in: worry, out: monkey id
        long testNum
    ) {
        _operation = operation;
        _test = test;
        TestNum = testNum;
        Id = id;
        Inventory = startingItems;
    }

    public void ApplyOperation()
    {
        for (int i = 0; i < Inventory.Count; i++) {
            Inventory[i] = _operation(Inventory[i]);
        }

        InspectionCount += Inventory.Count;
    }

    public void Calm()
    {
        for (int i = 0; i < Inventory.Count; i++) {
            Inventory[i] /= 3;
        }
    }

    public void Toss(List<Monkey> monkeys)
    {
        for (int i = 0; i < Inventory.Count; i++) {
            var item = Inventory[i];
            var targetMonkey = _test(item);
            monkeys[targetMonkey].Inventory.Add(item);
        }

        Inventory.Clear();
    }

    public void CompressWorry(long commonDivisorMultiple)
    {
        for (int i = 0; i < Inventory.Count; i++) {
            if (Inventory[i] <= TestNum) {
                continue;
            }
            // https://www.reddit.com/r/adventofcode/comments/zifqmh/comment/izrfgit/?utm_source=share&utm_medium=web2x&context=3
            Inventory[i] = Inventory[i] % commonDivisorMultiple;
        }
    }

    private static Func<long, long> BuildOperationFn(string left, string op, string right)
    {
        var oldParam = Expression.Parameter(typeof(long), "old");
        Expression leftExpr = left == "old"
            ? oldParam
            : Expression.Constant(long.Parse(left));

        Expression rightExpr = right == "old"
            ? oldParam
            : Expression.Constant(long.Parse(right));

        var expr = op switch
        {
            "+" => Expression.Add(leftExpr, rightExpr),
            "*" => Expression.Multiply(leftExpr, rightExpr),
        };

        return Expression.Lambda<Func<long, long>>(expr, oldParam).Compile();
    }

    public static Monkey Parse(List<string> lines)
    {
        var id = int.Parse(lines[0]["Monkey ".Length..^1]);
        var items = lines[1]["  Starting items: ".Length..].Split(", ").Select(long.Parse).ToList();
        var operationTextTokens = lines[2]["  Operation: new = ".Length..].Split(' ');

        var testDivisibleNum = int.Parse(lines[3]["  Test: divisible by ".Length..]);

        var trueMonkeyId = int.Parse(lines[4]["    If true: throw to monkey ".Length..]);
        var falseMonkeyId = int.Parse(lines[5]["    If false: throw to monkey ".Length..]);
        return new Monkey(
            id,
            items,
            BuildOperationFn(operationTextTokens[0], operationTextTokens[1], operationTextTokens[2]),
            (worry) => worry % testDivisibleNum == 0 ? trueMonkeyId : falseMonkeyId,
            testDivisibleNum
        );
    }
}


public class Day11Impl : PartitionedDayHandler
{
    protected override void Part1(List<string> lines)
    {
        var lineChunks = new List<List<string>>();
        int i = 0;
        var curLine = new List<string>();
        foreach (var line in lines) {
            if (line.Length == 0) {
                i++;
                lineChunks.Add(curLine);
                curLine = new List<string>();
                continue;
            }

            curLine.Add(line);
        }

        lineChunks.Add(curLine);

        var monkeys = lineChunks.Select(Monkey.Parse).ToList();

        for (int j = 0; j < 20; j++) {
            foreach (var monkey in monkeys) {
                monkey.ApplyOperation();
                monkey.Calm();
                monkey.Toss(monkeys);
            }

        }

        var inspectionCounts = monkeys.Select(m => m.InspectionCount).ToList();
        var top2 = inspectionCounts.OrderByDescending(el => el).Take(2).ToList();

        // Final Answer
        Console.WriteLine(top2[0] * top2[1]);
    }

    protected override void Part2(List<string> lines)
    {
        var lineChunks = new List<List<string>>();
        int i = 0;
        var curLine = new List<string>();
        foreach (var line in lines) {
            if (line.Length == 0) {
                i++;
                lineChunks.Add(curLine);
                curLine = new List<string>();
                continue;
            }

            curLine.Add(line);
        }

        lineChunks.Add(curLine);

        var monkeys = lineChunks.Select(Monkey.Parse).ToList();
        long productOfTestNums = monkeys
            .Select(m => m.TestNum)
            .Distinct()
            .Aggregate((long)1, (accum, cur) => cur * accum);

        for (int j = 0; j < 10000; j++) {
            foreach (var monkey in monkeys) {
                monkey.ApplyOperation();
                // monkey.Calm();
                monkey.CompressWorry(productOfTestNums);
                monkey.Toss(monkeys);
            }

        }

        var inspectionCounts = monkeys.Select(m => m.InspectionCount).ToList();
        var top2 = inspectionCounts.OrderByDescending(el => el).Take(2).ToList();

        // Final Answer
        Console.WriteLine(top2[0] * top2[1]);
    }
}
