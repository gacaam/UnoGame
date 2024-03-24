using UnoGame;

class Program
{
    // Console App Implementation
    static async Task Main()
    {   
        // game controller
        GameController gameControl = new(new Deck());
        gameControl.GameInfo = ConsolePrint;
        gameControl.GetInput = GetConsoleInput;
        gameControl.Divider = ConsoleDivider;

        // add players
        int numOfPlayers;
        Console.WriteLine("Enter Number of Players (2-4):");
        while(!int.TryParse(Console.ReadLine(), out numOfPlayers) || numOfPlayers > 4 || numOfPlayers < 2)
        {
            Console.WriteLine("\nInvalid. Enter number of players again (2-4):");
        }

        for(int i=0; i<numOfPlayers; i++)
        {
            string playerName = GetConsoleInput($"Enter Player {i+1}'s Name:");

            // Set default name if input is empty
            if(String.IsNullOrEmpty(playerName))
            {
                playerName = $"Player{i+1}";
            }
            Player newPlayer = new(playerName, i);
            gameControl.InsertPlayer(newPlayer);
        }

        // game start
        Console.WriteLine("Drawing first card..");
        await Task.Delay(1500);
        ICard firstCard = gameControl.PrepareGame();
        Console.WriteLine($"First Card: ");
        UserInterface.DisplayCard(firstCard);
        await Task.Delay(1500);
        Console.WriteLine("Starting Game!");
        await Task.Delay(1500);
        while(true)
        {
            if(gameControl.CheckEmptyHand())
            {
                gameControl.GetWinnerOrder();
                break;
            }
            await PlayerTurn(gameControl);
            gameControl.NextTurn();
        }

        Console.WriteLine($"Game Winner: {gameControl.WinnerOrder.First().Name}");
        int count = 1;
        Console.WriteLine("\n[Rank]");
        foreach(IPlayer player in gameControl.WinnerOrder)
        {
            Console.WriteLine($"{count}. {player.Name}");
            count++;
        }
    }

    public static async Task PlayerTurn(GameController gc){
        var CurrentPlayer = gc.CurrentPlayer;
        var CurrentRevealedCard = gc.CurrentRevealedCard;
        var possibleCards = gc.GetPossibleCards(CurrentPlayer);
        var otherCards = gc.PlayersHand[CurrentPlayer].Where(cards => !possibleCards.Contains(cards)).ToList();
        
        Console.Clear();
        Console.WriteLine($"Last Card Played:");
        UserInterface.DisplayCard(CurrentRevealedCard);
        Console.WriteLine("================================================");
        Console.WriteLine($"{CurrentPlayer.Name}'s turn");
        ConsoleDivider();

        if(possibleCards.Count() > 0)
        {
            Console.WriteLine("(Available cards)");
            for(int i = 0; i < possibleCards.Count(); i++){
                var card = possibleCards.ElementAt(i);
                Console.WriteLine($"\t{i + 1}. " + UserInterface.CardConsoleColor(card));
                Console.ResetColor();
            }

            Console.WriteLine("(Other cards in hand)");
            for(int i = 0; i < otherCards.Count; i++){
                var card = otherCards[i];
                Console.WriteLine($"\t{i + possibleCards.Count() + 1}. "  + UserInterface.CardConsoleColor(card));
                Console.ResetColor();
            }

            var input = GetConsoleInput("Choose a card by index: ");
            int indexVal;
            while(!int.TryParse(input, out indexVal) || indexVal > possibleCards.Count()){
                input = GetConsoleInput("Try again... Choose only available cards by index (ex: 1)");
            }
            var playedCard = possibleCards.ElementAt(indexVal - 1);
            gc.PlayerPlayCard(CurrentPlayer, playedCard);
            Console.WriteLine($"{CurrentPlayer.Name} plays ");
            UserInterface.DisplayCard(playedCard);
        }
        else
        {
            Console.WriteLine("(No available cards to play)");
            Console.WriteLine("(Cards in hand)");
            for(int i = 0; i < otherCards.Count; i++){
                var card = otherCards[i];
                Console.WriteLine($"\t{i + 1}. " + UserInterface.CardConsoleColor(card));
                Console.ResetColor();
            }

        
            Console.WriteLine("No cards to play... drawing card");
            await Task.Delay(2000);
            var newCard = gc.PlayerDrawCard(CurrentPlayer);
            Console.WriteLine($"Drawn Card: " + UserInterface.CardConsoleColor(newCard));
            Console.ResetColor();
            ConsoleDivider();
            
            if(gc.PossibleCard(newCard)){
                gc.PlayerPlayCard(CurrentPlayer, newCard);
                Console.WriteLine($"{CurrentPlayer.Name} plays ");
                UserInterface.DisplayCard(newCard);
            }
            await Task.Delay(1500);
        }
        await Task.Delay(2500);
    }

    public static void ConsolePrint(string inputString)
    {
        Console.WriteLine(inputString);
    }

     public static string GetConsoleInput(string description)
    {
        Console.WriteLine("");
        Console.WriteLine(description);
        string? consoleInput = Console.ReadLine(); 
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
}