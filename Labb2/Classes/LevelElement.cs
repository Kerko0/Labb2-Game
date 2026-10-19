using Labb2.Structs;

namespace Labb2.Classes;

public abstract class LevelElement
{
    public Position Position;
    public char Texture;
    public ConsoleColor Color;
    public bool IsVisible;

    public void Render()
    {
        if (IsVisible)
        {
            Console.SetCursorPosition(Position.Left, Position.Top);
            Console.ForegroundColor = Color;
            Console.Write(Texture);
        }

    }
}