namespace AoC_CSharp_2022.Day09;

record struct Coordinate(int X, int Y)
{
    public Coordinate Move(Direction direction)
    {
        return direction switch
        {
            Direction.Up    => new (X, Y + 1),
            Direction.Down  => new (X, Y - 1),
            Direction.Left  => new (X - 1, Y),
            Direction.Right => new (X + 1, Y),
        };
    }

    public int DistanceSq(Coordinate other)
        => (int)Math.Pow(other.X - X, 2) + (int)Math.Pow(other.Y - Y, 2);

    public Direction? PrimaryDirectionTo(Coordinate other)
    {
        int relativeX = other.X - X;
        int relativeY = other.Y - Y;
        if (relativeX * relativeX == relativeY * relativeY) {
            return null;
        }

        bool isXLarger = relativeX * relativeX > relativeY * relativeY;
        if (isXLarger) {
            return relativeX > 0 ? Direction.Right : Direction.Left;
        }

        return relativeY > 0 ? Direction.Up : Direction.Down;
    }

    public Coordinate GetCornerInDirection(Coordinate other)
    {
        int relativeX = Math.Sign(other.X - X);
        int relativeY = Math.Sign(other.Y - Y);
        return new(X + relativeX, Y + relativeY);

    }
}

public class Day9Impl : IDayHandler
{
    public void Go(List<string> lines)
    {
        var moves = lines
            .Select(line =>
            {
                var parts = line.Split(' ');
                return (Direction: ParseDirection(parts[0]), Distance: int.Parse(parts[1]));
            })
            .ToList();

        var tailPositionLog = new List<Coordinate>();

        Coordinate headPosition = new(0, 0);
        Coordinate[] tailPosition = new Coordinate[9];//new(0, 0);
        tailPositionLog.Add(tailPosition[^1]);

        foreach (var (direction, distance) in moves) {
            for (int i = 0; i < distance; i++) {
                headPosition = headPosition.Move(direction);

                var kindaHead = headPosition;
                for (int j = 0; j < tailPosition.Length; j++) {
                    var newTailPos = GetTailPosition(kindaHead, tailPosition[j]);
                    tailPosition[j] = newTailPos;
                    kindaHead = newTailPos;
                }

                tailPositionLog.Add(tailPosition[^1]);
            }
        }


        Console.WriteLine(tailPositionLog.Distinct().Count());
    }
    // 5877
    private Coordinate GetTailPosition(
        Coordinate headPosition,
        Coordinate tailPosition)
    {
        if (tailPosition == headPosition) {
            return tailPosition;
        }

        int distSq = headPosition.DistanceSq(tailPosition);
        return distSq switch
        {
            < 2 * 2 => tailPosition,
            2 * 2 => tailPosition.Move(tailPosition.PrimaryDirectionTo(headPosition)!.Value),
            >= 2 * 2 and < 8 => headPosition.Move(headPosition.PrimaryDirectionTo(tailPosition)!.Value),
            >= 8 => headPosition.GetCornerInDirection(tailPosition),
        };
    }

    private static Direction ParseDirection(string str)
    => str switch
    {
        "U" => Direction.Up,
        "D" => Direction.Down,
        "L" => Direction.Left,
        "R" => Direction.Right,
    };
}

enum Direction
{
    Left = -1,
    Right = 1,
    Up = 2,
    Down = -2,
}
