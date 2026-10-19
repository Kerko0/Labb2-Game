using Labb2.Structs;
using Labb2.Enums;

namespace Labb2.Classes;

public class Snake : Enemy
{    
    public override int MaxHealth { get; } = 5;
    public override Dice AttackDice { get; } = new Dice(3, 4, 2);
    public override Dice DefenseDice { get; } = new Dice(1, 8, 5);
    
    public Snake(Position position, char texture, ConsoleColor color) :  base(position, texture, color) { }

    public override void Move(LevelElement[] levelElements)
    {
        Position newPos = new Position();
        Position playerPos = levelElements.OfType<Player>().FirstOrDefault().Position;

        if (Position.DistanceTo(playerPos) <= 2)
        {        
            Direction playerDirection = Position.DirectionTo(playerPos);
            
            switch (playerDirection)
            {
                case Direction.Up:
                    newPos = Position.DownBy(1);
                    break;
                case Direction.Left:
                    newPos = Position.RightBy(1);
                    break;
                case Direction.Down:
                    newPos = Position.DownBy(-1);
                    break;
                case Direction.Right:
                    newPos = Position.RightBy(-1);
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
}