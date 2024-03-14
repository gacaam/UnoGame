namespace UnoGame;
using UnoGame.Interface;
using UnoGame.Enums;
using UnoGame.Cards;
using System.Security.Cryptography.X509Certificates;

public class GameController
{
    public Action<IPlayer> OnTurnChange; 
    public Action<IPlayer> CallUNO;
    public Dictionary<IPlayer, List<ICard>> PlayersHand {get; private set;}
    public Deck CardDeck {get; private set;} = new();
    public Stack<ICard> DiscardPile {get; private set;} = new();
    public ICard CurrentRevealedCard {get; private set;}
    public GameRotation Rotation {get; private set;}
    public IPlayer[] WinnerOrder {get; private set;}
    public IPlayer currentPlayer{get; private set;}
    public GameController()
    {
        // Shuffle Deck
        var ShuffledDeck = CardDeck.ShuffleDeck();

        // Add players & deal players' cards
        Console.WriteLine("Enter number of players (max 4):");
        int numOfPlayers;
        while(!int.TryParse(Console.ReadLine(), out numOfPlayers) || numOfPlayers > 4)
        {
            Console.WriteLine("\nInvalid. Enter number of players again (max 4):");
        }
        for(int i=0; i<numOfPlayers; i++)
        {
            Console.WriteLine($"Enter player {i}'s name:");
            string playerName = Console.ReadLine();

            // Set default name if null input
            playerName ??= $"Player{i}";
            Player newPlayer = new(playerName, i);
            
            InsertPlayer(newPlayer);
            SetPlayerHand(newPlayer);
        }

        // Initial discard card
        var firstCard = DrawCard();

        // First card cannot be a wild card
        while(firstCard.Color == CardColor.Black)
        {
            CardDeck.Cards.Push(firstCard);
            CardDeck.ShuffleDeck();
            firstCard = DrawCard();
        }
        DiscardPile.Push(firstCard);

        // Game Play
        //TODO implement game play
    }
    public bool InsertPlayer(IPlayer player){
        PlayersHand.Add(player, new List<ICard>());
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

    public bool possibleColor(ICard card){
        return(card.Color == CurrentRevealedCard.Color || card.Color == CardColor.Black);
    }

    public bool possibleValue(ICard card){
        return(card.Type == CurrentRevealedCard.Type);
    }

    public IEnumerable<ICard> GetPossibleCard(IPlayer player){ //TODO: GetPossibleCard
        List<ICard> possibleCards = new List<ICard>(PlayersHand[player].FindAll(possibleColor));
        possibleCards.AddRange(PlayersHand[player].FindAll(possibleValue));
        return new List<ICard>();
    }

    public IEnumerable<Player> GetWinnerOrder(){ //TODO: GetWinnerOrder
        return new List<Player>();
    }

    public bool PlayerPlayCard(IPlayer player, ICard card){ //TODO: PlayerPlayCard
        Console.WriteLine($"{player.Name}: {Enum.GetName(typeof(CardType), card.Type)}{Enum.GetName(typeof(CardColor), card.Color)}");
        return true;
    }

    public void AddTwoPenalty(){
        Console.WriteLine($"{currentPlayer.Name} draws 2 card.");
        DrawCard();
        DrawCard();
    }
    //TODO: Action CallUNO
    public bool PlayerCallUNO(IPlayer player){
        // event CallUNO
        Console.WriteLine($"{player.Name}: UNO!");
        if(PlayersHand[currentPlayer].Count == 1)
        {   
            Console.WriteLine("Successful UNO challenge >:)");
            return true;
        }
        Console.WriteLine($"Oops! False challenge... {player.Name} still has more than 1 card.");
        return false;
        
    }

    public bool ChangeRotation(){
        //TODO: Change Rotation
        return true;
    }

    public ICard DrawCard(){
        var drawnCard = CardDeck.Draw();
        PlayersHand[currentPlayer].Add(drawnCard);
        return drawnCard;
    }

    public IEnumerable<IPlayer> SkipTurn(){ //TODO: SkipTurn
        return new List<IPlayer>();
    }

}
