using Labb2.Enums;
using Labb2.Structs;

namespace Labb2.Classes;

public class Player : LevelElement
{
    
    public int Health = 100;
    public Dice AttackDice = new Dice(2, 6, 2);
    public Dice DefenseDice = new Dice(2, 6, 0);
    
    public event EventHandler<CollisionEventArgs>? BumpedInto;
    private void OnBumpedInto(LevelElement other)
        => BumpedInto?.Invoke(this, new CollisionEventArgs(other));
    public void Move(Direction direction, LevelElement[] levelElements)
    {
        Position newPos = new Position();
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

    public string Attack(Enemy enemy)
    {
        var playerAttack = AttackDice.Throw();
        var enemyDefense = enemy.DefenseDice.Throw();

        var attackResult = playerAttack - enemyDefense;

        string resultMessage = "";
        switch (attackResult)
        {
            case >0:
                enemy.Health -= attackResult;
                resultMessage = "wounding it.";
                break;
            case <= 0:
                resultMessage = "but you missed.";
                break;
        }

        if (enemy.Health < 0)
        {
            resultMessage = "killing it.";
        }

        return $"You (ATK: {AttackDice} => {playerAttack}) attacked the {enemy} (DEF: {enemy.DefenseDice} => {enemyDefense}), {resultMessage}";
    }
}