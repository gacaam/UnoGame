namespace UnoGame;
using UnoGame.Interface;
using UnoGame.Enums;
using UnoGame.Cards;
using System.Security.Cryptography.X509Certificates;

public class GameController
{
    public Action<IPlayer> OnTurnChange; 
    public Action<IPlayer> CallUNO;
    public Dictionary<IPlayer, List<ICard>> PlayersHand {get; private set;} = new();
    public Deck CardDeck {get; private set;} = new();
    public Stack<ICard> DiscardPile {get; private set;} = new();
    public ICard CurrentRevealedCard {get; private set;}
    public GameRotation Rotation {get; private set;} = GameRotation.Clockwise;
    public IPlayer[] WinnerOrder {get; private set;}
    public IPlayer CurrentPlayer{get; private set;}
    public IPlayer NextPlayer{get; private set;}
    public int CurrentPlayerIndex {get; private set;}
    public int NextPlayerIndex {get; private set;}
    public GameController()
    {
        // Shuffle Deck
        CardDeck.Cards = CardDeck.ShuffleDeck();

        // Add players & deal players' cards
        Console.WriteLine("Enter number of players (max 4):");
        int numOfPlayers;
        while(!int.TryParse(Console.ReadLine(), out numOfPlayers) || numOfPlayers > 4)
        {
            Console.WriteLine("\nInvalid. Enter number of players again (max 4):");
        }
        for(int i=0; i<numOfPlayers; i++)
        {
            Console.WriteLine($"\nEnter player {i+1}'s name:");
            string playerName;
            playerName = Console.ReadLine();

            // Set default name if null input
            playerName ??= $"Player{i+11}";
            Player newPlayer = new(playerName, i);
        
            InsertPlayer(newPlayer);
            SetPlayerHand(newPlayer);
        }

        // Initial discard card
        var firstCard = CardDeck.Draw();

        // First card cannot be a wild card
        while(firstCard.Color == CardColor.Black)
        {
            Console.WriteLine("\nOops, wild card. Draw again!");
            CardDeck.Cards.Push(firstCard);
            CardDeck.Cards = CardDeck.ShuffleDeck();
            firstCard = CardDeck.Draw();
        }
        DiscardPile.Push(firstCard);
        CurrentRevealedCard = firstCard;
        Console.WriteLine($"\nFirst Card:\t{Enum.GetName(typeof(CardType), CurrentRevealedCard.Type)} {Enum.GetName(typeof(CardColor), CurrentRevealedCard.Color)}\n");

        // Game Play
        CurrentPlayerIndex = 0;
        List<IPlayer> playersList = PlayersHand.Keys.ToList();
        CurrentPlayer = playersList[CurrentPlayerIndex];

        Console.WriteLine("Starting Game!\n");
        while(PlayersHand[CurrentPlayer].Count>0)
        {
            PlayerTurn(playersList);
            NextTurn();
            CurrentPlayer = playersList[CurrentPlayerIndex];
        }
        Console.WriteLine($"Congratulations! {CurrentPlayer.Name} has won :D ");
    }
    public bool InsertPlayer(IPlayer player){
        PlayersHand.Add(player, []);
        return true;
    }
    public bool SetPlayerHand(IPlayer player){
        for(int i=0; i<7; i++)
        {
            PlayersHand[player].Add(CardDeck.Cards.Pop());
        }
        return true;
    }
    public IEnumerable<ICard> GetPlayerHand(IPlayer player){
        return PlayersHand[player];
    }

    public bool PossibleCard(ICard card){
        return(card.Color == CurrentRevealedCard.Color || card.Type == CurrentRevealedCard.Type);
    }
    public List<ICard> GetPossibleCards(IPlayer player){ 
        List<ICard> possibleCards = PlayersHand[player].FindAll(PossibleCard);
        return possibleCards;
    }

    public IEnumerable<Player> GetWinnerOrder(){ //TODO: GetWinnerOrder
        return [];
    }

    public bool PlayerPlayCard(IPlayer player, ICard cardChosen){ 
        DiscardPile.Push(cardChosen);
        CurrentRevealedCard = cardChosen;
        PlayersHand[player].Remove(cardChosen);
        Console.WriteLine($"{player.Name} plays {Enum.GetName(typeof(CardType), cardChosen.Type)} {Enum.GetName(typeof(CardColor), cardChosen.Color)}");
        var type =  cardChosen.ExecuteCardEffect(this);
          return true;
    }

    // public void AddTwoPenalty(){
    //     Console.WriteLine($"{currentPlayer.Name} draws 2 card.");
    //     DrawCard();
    //     DrawCard();
    // }

    //TODO: Action CallUNO
    public bool PlayerCallUNO(IPlayer player){
        Console.WriteLine($"{player.Name}: UNO!");
        if(PlayersHand[CurrentPlayer].Count == 1)
        {   
            Console.WriteLine("Successful UNO challenge >:)");
            return true;
        }
        Console.WriteLine($"Oops! False challenge... {player.Name} still has more than 1 card.");
        return false;
    }

    public bool ChangeRotation(){
        if(Rotation == GameRotation.Clockwise){
            Rotation = GameRotation.CounterClockwise;
        }else{
            Rotation = GameRotation.Clockwise;
        }
        return true;
    }

    public void PlayerTurn(List<IPlayer> players){
        Console.Clear();
        CurrentPlayer = players[CurrentPlayerIndex];
        NextPlayer = players[NextPlayerIndex];
        List<ICard> possibleCards = GetPossibleCards(CurrentPlayer);
        List<ICard> otherCards = PlayersHand[CurrentPlayer].Where(cards => !possibleCards.Contains(cards)).ToList();

        Console.WriteLine("================================================");
        Console.WriteLine($"\nLast Card Played: {Enum.GetName(typeof(CardType), CurrentRevealedCard.Type)} {Enum.GetName(typeof(CardColor), CurrentRevealedCard.Color)}\n");
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine($"{CurrentPlayer.Name}'s turn\n");

        if(possibleCards.Count>0)
        {
            Console.WriteLine("(Available cards)");
            for(int i = 0; i < possibleCards.Count; i++){
                var card = possibleCards[i];
                Console.WriteLine($"\t{i+1}. {Enum.GetName(typeof(CardType), card.Type)} {Enum.GetName(typeof(CardColor), card.Color)}");
            }
            Console.WriteLine("(Other cards in hand)");
            for(int i = 0; i < otherCards.Count; i++){
                var card = otherCards[i];
                Console.WriteLine($"\t{i+possibleCards.Count+1}. {Enum.GetName(typeof(CardType), card.Type)} {Enum.GetName(typeof(CardColor), card.Color)}");
            }
            Console.WriteLine("\nChoose a card by index: ");

            var input = Console.ReadLine();
            int indexVal;
            while(!int.TryParse(input, out indexVal) || indexVal-1 > possibleCards.Count){
                Console.WriteLine("Try again... Choose only available cards by index (ex: 1)");
                input = Console.ReadLine();
            }
            PlayerPlayCard(CurrentPlayer, possibleCards[indexVal-1]);

        }else{
            Console.WriteLine("(No available cards to play)");
            Console.WriteLine("(Cards in hand)");
            for(int i = 0; i < otherCards.Count; i++){
                var card = otherCards[i];
                Console.WriteLine($"\t{i+1}. {Enum.GetName(typeof(CardType), card.Type)} {Enum.GetName(typeof(CardColor), card.Color)}");
            }
            Console.WriteLine("");
            var newCard = PlayerDrawCard(CurrentPlayer);
            Console.WriteLine($"Drawn Card: {Enum.GetName(typeof(CardType), newCard.Type)} {Enum.GetName(typeof(CardColor), newCard.Color)}");
            
            if(PossibleCard(newCard)){
                PlayerPlayCard(CurrentPlayer,newCard);
            }
        }
        Console.WriteLine("================================================");

        Thread.Sleep(2500);
    }
    public void NextTurn()
    {
        if(Rotation == GameRotation.Clockwise){
            CurrentPlayerIndex = (CurrentPlayerIndex + 1) % PlayersHand.Count;
            NextPlayerIndex = (CurrentPlayerIndex + 1) % PlayersHand.Count;
        }else{
            CurrentPlayerIndex = (CurrentPlayerIndex + PlayersHand.Count - 1) % PlayersHand.Count;
            NextPlayerIndex = (CurrentPlayerIndex + PlayersHand.Count - 1) % PlayersHand.Count;
        }
    }
    public ICard PlayerDrawCard(IPlayer player){
        var drawnCard = CardDeck.Draw();
        PlayersHand[player].Add(drawnCard);
        Console.WriteLine($"\n{player.Name} draws a card");
        return drawnCard;
    }
}
