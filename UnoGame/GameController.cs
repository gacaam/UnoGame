namespace UnoGame;
using UnoGame.Interface;
using UnoGame.Enums;
using UnoGame.Cards;

public class GameController
{
    public event Action<IPlayer> OnTurnChange; 
    public event Action<IPlayer> CallUNO;
    public Dictionary<IPlayer, List<ICard>> PlayersHand {get; private set;}
    public Deck CardDeck {get; private set;} = new();
    public Stack<ICard> DiscardPile {get; private set;} = new();
    public ICard CurrentRevealedCard {get; private set;}
    public GameRotation Rotation {get; private set;}
    public IPlayer[] WinnerOrder {get; private set;}
    public IPlayer currentPlayer{get; private set;}
    public GameController(int numOfPlayers)
    {
        // Shuffle Deck
        var ShuffledDeck = CardDeck.ShuffleDeck();

        // Add players & deal players' cards
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

    public IEnumerable<Player> GetWinnerOrder(){
        return new List<Player>();
    }

    public bool PlayerPlayCard(IPlayer player, ICard card){
        Console.WriteLine($"{player.Name}: {Enum.GetName(typeof(CardType), card.Type)}{Enum.GetName(typeof(CardColor), card.Color)}");
        return true;
    }

    public void AddTwoPenalty(IPlayer player){

    }
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

        return true;
    }

    public ICard DrawCard(){
        var drawnCard = CardDeck.Draw();
        PlayersHand[currentPlayer].Add(drawnCard);
        Console.WriteLine($"{currentPlayer.Name} draws a {Enum.GetName(typeof(CardType), drawnCard.Type)}{Enum.GetName(typeof(CardColor), drawnCard.Color)} card");
        return drawnCard;
    }

    public IEnumerable<IPlayer> SkipTurn(){
        return new List<IPlayer>();
    }

    public bool SetDeck(params ICard[] cards){

        return true;
    }


}
