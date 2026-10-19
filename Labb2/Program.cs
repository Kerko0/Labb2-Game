using System.Reflection.Metadata.Ecma335;
using Labb2.Enums;
using Labb2.Classes;

string filePath;
do
{
    Console.WriteLine("Please enter the path to the level.");
    filePath = Console.ReadLine();
    
} while (string.IsNullOrEmpty(filePath));

var levelData = new LevelData();
levelData.Load(filePath);

HUD hud = new HUD(File.ReadLines(filePath).Count());

Player player = levelData.Player;

player.BumpedInto += OnCollision;
foreach (var enemy in levelData.Enemies)
{
    enemy.BumpedInto += OnCollision;
}

bool gameOver = false;
player.IsVisible = true;
Console.CursorVisible = false;

do
{
    Update();
    Render();
    var key = Console.ReadKey(intercept: true).Key;
    OnKeyPress(key);
    
} while (!gameOver);

void OnKeyPress(ConsoleKey key)
{
    switch (key)
    {
        case ConsoleKey.W:
            player.Move(Direction.Up, levelData.LevelElements.ToArray());
            break;
        case ConsoleKey.A:
            player.Move(Direction.Left, levelData.LevelElements.ToArray());
            break;
        case ConsoleKey.S:
            player.Move(Direction.Down, levelData.LevelElements.ToArray());
            break;
        case ConsoleKey.D:
            player.Move(Direction.Right, levelData.LevelElements.ToArray());
            break;
    }
}

void Update()
{
    if (player.Health <= 0)
    {
        gameOver = true;
    }

    hud.PlayerStats = "Health: " + player.Health;
    foreach (var enemy in levelData.Enemies)
    {
        enemy.Move(levelData.LevelElements.ToArray());
    }

    levelData.LevelElements.RemoveAll(e => e is Enemy { IsDead: true });
}

void Render()
{
    Console.Clear();
    
    foreach (var wall in levelData.Walls)
    {
        if (wall.Position.DistanceTo(player.Position) <= 5 && !wall.IsVisible)
        {
            wall.IsVisible = true;
        }
        
        wall.Render();
    }

    foreach (var enemy in levelData.Enemies)
    {
        enemy.IsVisible = enemy.Position.DistanceTo(player.Position) <= 5;
        enemy.Render();
    }
    
    player.Render();
    hud.Render();
}

void OnCollision(object? sender, CollisionEventArgs e)
{
    if (sender is not LevelElement attacker) return;
    
    var player = attacker as Player ?? e.Other as Player;
    var enemy  = attacker as Enemy  ?? e.Other as Enemy;
    if (player is null || enemy is null) return;

    switch (sender, e.Other)
    {
        case (Player p, Enemy en):
            hud.AddToCombatLog(new CombatLog(p.Attack(en), ConsoleColor.Yellow));
            if(en.Health > 0)
                hud.AddToCombatLog(new CombatLog(en.Attack(p), ConsoleColor.Red));
            break;
        case(Enemy en, Player p):
            hud.AddToCombatLog(new CombatLog(en.Attack(p), ConsoleColor.Red));
            if(p.Health > 0)
                hud.AddToCombatLog(new CombatLog(p.Attack(en), ConsoleColor.Yellow));
            break;
    }

    if (enemy.Health <= 0)
    {
        enemy.BumpedInto -= OnCollision;
        enemy.IsDead = true;
    }
}

