namespace Labb2.Classes;

public class Dice
{
    private readonly int _numberOfDice;
    private readonly int _sidesPerDice;
    private readonly int _modifier;
    
    public Dice(int numberOfDice, int sidesPerDice, int modifier)
    {
        _numberOfDice = numberOfDice;
        _sidesPerDice = sidesPerDice;
        _modifier = modifier;
    }

    public int Throw()
    {
        int[] diceThrows = new int[_numberOfDice];
        Random r = new Random();

        for (int i = 0; i < _numberOfDice; i++)
        {
            diceThrows[i] = r.Next(0, _sidesPerDice);
        }
        
        var result = diceThrows.Sum() + _modifier;
        return result;
    }

    public override string ToString()
    {
        return $"{_numberOfDice}d{_sidesPerDice}+{_modifier}";
    }
}