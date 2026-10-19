using Labb2.Enums;

namespace Labb2.Structs;

public struct Position
{
    public Position(int top, int left)
    {
        Top = top;
        Left = left;
    }
    
    public int Top { get; }
    public int Left { get; }   
    
    public Position RightBy(int n) => new Position(Top, Left + n);
    public Position DownBy(int n) => new Position(Top + n, Left);

    public double DistanceTo(Position other)
    {
        double dx = other.Left - this.Left;
        double dy = other.Top - this.Top;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    public Direction DirectionTo(Position other)
    {
        int dx = other.Left - Left;
        int dy = other.Top - Top;

        if (dx == 0 && dy == 0)
            throw new InvalidOperationException("Positions are the same; there is no direction.");

        if (Math.Abs(dx) > Math.Abs(dy))
            return dx > 0 ? Direction.Right : Direction.Left;

        return dy > 0 ? Direction.Down : Direction.Up;
    }
}