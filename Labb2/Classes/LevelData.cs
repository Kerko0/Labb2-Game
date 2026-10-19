using Labb2.Structs;

namespace Labb2.Classes;

public class LevelData
{
    private static List<LevelElement> _levelElements = new();

    public readonly List<LevelElement> LevelElements = _levelElements;

    public Player Player => LevelElements.OfType<Player>().FirstOrDefault();
    public IEnumerable<Enemy> Enemies => LevelElements.OfType<Enemy>();
    public IEnumerable<Wall> Walls => LevelElements.OfType<Wall>();

    public void Load(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                var position = new Position(i, j);
                var texture = lines[i][j];
                
                switch (lines[i][j])
                {
                    case '#':
                        _levelElements.Add(new Wall
                        {
                            Position = position,
                            Texture = texture,
                            Color = ConsoleColor.DarkRed
                        });
                        break;
                    case 'r':
                        _levelElements.Add(new Rat(position, texture, ConsoleColor.Gray));
                        break;
                    case 's':
                        _levelElements.Add(new Snake(position, texture, ConsoleColor.DarkGreen));
                        break;
                    case '@':
                        _levelElements.Add(new Player
                        {
                            Position = position,
                            Texture = texture,
                            Color = ConsoleColor.Yellow
                        });
                        break;
                    
                }
            }
        }
    }
}