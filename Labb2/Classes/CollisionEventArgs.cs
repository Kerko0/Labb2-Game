namespace Labb2.Classes;

public class CollisionEventArgs : EventArgs
{
    public LevelElement Other { get; }
    public CollisionEventArgs(LevelElement other) => Other = other;
}