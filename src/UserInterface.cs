namespace UnoGame;

public static class UserInterface
{
    public static void DisplayCard(ICard card)
    {
        string symbol = "";
        switch(card.Type)
        {
            case CardType.Zero: symbol = " 0"; 
                break;
            case CardType.One: symbol = " 1"; 
                break;
            case CardType.Two: symbol = " 2"; 
                break;
            case CardType.Three: symbol = " 3"; 
                break;
            case CardType.Four: symbol = " 4"; 
                break;
            case CardType.Five: symbol = " 5"; 
                break;
            case CardType.Six: symbol = " 6"; 
                break;
            case CardType.Seven: symbol = " 7"; 
                break;
            case CardType.Eight: symbol = " 8"; 
                break;
            case CardType.Nine: symbol = " 9"; 
                break;
            case CardType.Reverse: symbol = " ↺"; 
                break;
            case CardType.Skip: symbol = " ⊵"; 
                break;
            case CardType.DrawTwo: symbol = "+2"; 
                break;
            case CardType.DrawFour: symbol = "+4"; 
                break;
        }
        switch(card.Color)
        {
            case CardColor.Black: Console.ForegroundColor = ConsoleColor.White; break;
            case CardColor.Blue: Console.ForegroundColor = ConsoleColor.Blue; break;
            case CardColor.Red: Console.ForegroundColor = ConsoleColor.Red; break;
            case CardColor.Yellow: Console.ForegroundColor = ConsoleColor.Yellow; break;
            case CardColor.Green: Console.ForegroundColor = ConsoleColor.Green; break;
        }
        Console.Write($"{Enum.GetName(typeof(CardType), card.Type)} {Enum.GetName(typeof(CardColor), card.Color)}\n");
        Console.WriteLine("=======");
        Console.WriteLine("|     |");
        Console.WriteLine($"| {symbol}  |");
        Console.WriteLine("|     |");
        Console.WriteLine("=======");
        Console.ResetColor();
    }
}
