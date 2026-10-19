using Labb2.Structs;

namespace Labb2.Classes;

public class HUD
{
    public int Top;

    public string PlayerStats;
    public List<CombatLog> CombatLogs = new();

    public HUD(int top)
    {
        Top = top;
    }

    public void Render()
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.SetCursorPosition(0,Top);
        Console.Write(PlayerStats);

        foreach (var log in CombatLogs)
        {
            Console.SetCursorPosition(0,1+Top+CombatLogs.IndexOf(log));
            Console.ForegroundColor = log.Color;
            Console.Write(log.Log);
        }
    }

    public void AddToCombatLog(CombatLog log)
    {
        if (CombatLogs.Count == 2)
        {
            CombatLogs.RemoveAt(CombatLogs.Count - 1);
        }
        
        CombatLogs.Insert(0,log);
    }
}

public class CombatLog 
{
    public string Log;
    public ConsoleColor Color;
    
    public CombatLog (string log, ConsoleColor color)
    {
        Log = log;
        Color = color;
    }
}

