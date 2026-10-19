using Labb2.Structs;
using Labb2.Enums;

namespace Labb2.Classes;

public abstract class Enemy : LevelElement
{
    public abstract int MaxHealth { get; }
    public abstract Dice AttackDice { get; }
    public abstract Dice DefenseDice { get; }
    public int Health;
    public bool IsDead = false;

    protected Enemy(Position position, char texture, ConsoleColor color)
    {
        Position = position;
        Texture = texture;
        Color = color;
        Health = MaxHealth;
    }

    public abstract void Move(LevelElement[] levelElements);
    public event EventHandler<CollisionEventArgs>? BumpedInto;
    
    protected void OnBumpedInto(LevelElement other)
        => BumpedInto?.Invoke(this, new CollisionEventArgs(other));
    
    public string Attack(Player player)
    {
        var enemyAttack = AttackDice.Throw();
        var playerDefense = player.DefenseDice.Throw();

        var attackResult = enemyAttack - playerDefense;

        string resultMessage = "";
        switch (attackResult)
        {
            case >0:
                player.Health -= attackResult;
                resultMessage = "wounding you.";
                break;
            case <= 0:
                resultMessage = "but it missed.";
                break;
        }
        
        if (player.Health < 0)
        {
            resultMessage = "killing you.";
        }

        return $"The {this} (ATK: {AttackDice} => {enemyAttack}) attacked you (DEF: {player.DefenseDice} => {playerDefense}), {resultMessage}";
    }

    public override string ToString()
    {
        return GetType().Name;
    }
}