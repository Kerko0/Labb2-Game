using Labb2.Structs;
using Labb2.Enums;
namespace Labb2.Classes;

public class Rat : Enemy
{
    public override int MaxHealth { get; } = 10;
    public override Dice AttackDice { get; } = new Dice(1, 6, 3);
    public override Dice DefenseDice { get; } = new Dice(1, 6, 1);
    
    public Rat(Position position, char texture, ConsoleColor color) :  base(position, texture, color) { }
    
    public override void Move(LevelElement[] levelElements)
    {
        Position newPos = new Position();
        
        Array values =  Enum.GetValues(typeof(Direction));
        Random random = new Random();
        Direction direction = (Direction)values.GetValue(random.Next(values.Length));
        
        switch (direction)
        {
            case Direction.Up:
                newPos = Position.DownBy(-1);
                break;
            case Direction.Left:
                newPos = Position.RightBy(-1);
                break;
            case Direction.Down:
                newPos = Position.DownBy(1);
                break;
            case Direction.Right:
                newPos = Position.RightBy(1);
                break;
        }
        
        foreach (var element in levelElements)
        {
            if (element != this && element.Position.Equals(newPos))
            {
                OnBumpedInto(element);
                return;
            } 
        }
        
        Position = newPos;
    }
}