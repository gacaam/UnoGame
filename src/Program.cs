using UnoGame;
using UnoGame.Interface;

class Program
{
    static void Main()
    {   
        // insert players
        int numOfPlayers;
        Console.WriteLine("Enter Number of Players (2-4):");
        while(!int.TryParse(Console.ReadLine(), out numOfPlayers) || numOfPlayers > 4 || numOfPlayers == 1)
        {
            Console.WriteLine("\nInvalid. Enter number of players again (2-4):");
        }

        // input players' names

        GameController gameControl = new(new Deck(), numOfPlayers);
    }

    public static void ConsoleTurnDivider()
    {
        Console.WriteLine("================================================");
    }

    public static void ConsoleDivider()
    {
        Console.WriteLine("------------------------------------------------");
    }



}