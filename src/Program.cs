using UnoGame;

class Program
{
    // Console App Implementation
    static async Task Main()
    {   
        // insert players
        int numOfPlayers;
        Console.WriteLine("Enter Number of Players (2-4):");
        while(!int.TryParse(Console.ReadLine(), out numOfPlayers) || numOfPlayers > 4 || numOfPlayers == 1)
        {
            Console.WriteLine("\nInvalid. Enter number of players again (2-4):");
        }
        // action and function subscriptions
        GameController gameControl = new();
        gameControl.GetInput = GetConsoleInput;
        gameControl.GameInfo = ConsolePrint;
        gameControl.Divider = ConsoleDivider;
        gameControl.OnTurnChange += ConsoleTurnDivider;

        // start game
        await gameControl.StartGame(numOfPlayers);
        Console.WriteLine($"Game Winner: {gameControl.WinnerOrder.First().Name}");
        int count = 1;
        Console.WriteLine("\n[Rank]");
        foreach(IPlayer player in gameControl.WinnerOrder)
        {
            Console.WriteLine($"{count}. {player.Name}");
            count++;
        }
    }

    public static void ConsolePrint(string inputString)
    {
        Console.WriteLine(inputString);
    }

    public static string GetConsoleInput(string description)
    {
        Console.WriteLine("");
        Console.WriteLine(description);
        string consoleInput = Console.ReadLine(); 
        if(String.IsNullOrEmpty(consoleInput))
        {
            return String.Empty;
        }
        return consoleInput;
    }

    public static void ConsoleDivider()
    {
        Console.WriteLine("------------------------------------------------");
    }

    public static void ConsoleTurnDivider()
    {
        Console.Clear();
        Console.WriteLine("================================================");
    }

}