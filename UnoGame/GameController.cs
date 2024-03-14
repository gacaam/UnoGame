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
    public IPlayer CurrentPlayer{get; private set;}
    public IPlayer NextPlayer{get; private set;}
    public int CurrentPlayerIndex {get; private set;}
    public int NextPlayerIndex {get; private set;}
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
        var firstCard = CardDeck.Draw();

        // First card cannot be a wild card
        while(firstCard.Color == CardColor.Black)
        {
            CardDeck.Cards.Push(firstCard);
            CardDeck.ShuffleDeck();
            firstCard = CardDeck.Draw();
        }
        DiscardPile.Push(firstCard);

        // Game Play
        //TODO implement game play
        CurrentPlayerIndex = 0;
        List<IPlayer> playersList = PlayersHand.Keys.ToList();
        while(true)
        {
            foreach(Player player in PlayersHand.Keys)
            {
                CurrentPlayer = player;
                if(PlayersHand[player].Count == 0){
                    break;
                }
            }
        }

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
        return(card.Color == CurrentRevealedCard.Color || card.Color == CardColor.Black || card.Type == CurrentRevealedCard.Type);
    }
    public IEnumerable<ICard> GetPossibleCards(IPlayer player){ 
        List<ICard> possibleCards = [];
        possibleCards = PlayersHand[player].FindAll(PossibleCard);
        return possibleCards;
    }

    public IEnumerable<Player> GetWinnerOrder(){ //TODO: GetWinnerOrder
        return [];
    }

    public bool PlayerPlayCard(IPlayer player, ICard card){ 
        DiscardPile.Push(card);
        CurrentRevealedCard = card;
        var type =  card.ExecuteCardEffect(this);
        Console.WriteLine($"{player.Name}: {Enum.GetName(typeof(CardType), card.Type)}{Enum.GetName(typeof(CardColor), card.Color)}");
        return true;
    }

    // public void AddTwoPenalty(){
    //     Console.WriteLine($"{currentPlayer.Name} draws 2 card.");
    //     DrawCard();
    //     DrawCard();
    // }

    //TODO: Action CallUNO
    public bool PlayerCallUNO(IPlayer player){
        // event CallUNO
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
        //TODO: Change Rotation
        return true;
    }

    public void PlayerTurn(List<IPlayer> players){
        CurrentPlayer = players[CurrentPlayerIndex];
        NextPlayer = players[NextPlayerIndex];
        
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

    public ICard PlayerDrawCard(Player player){
        var drawnCard = CardDeck.Draw();
        PlayersHand[player].Add(drawnCard);
        return drawnCard;
    }

    public IEnumerable<IPlayer> SkipTurn(){ //TODO: SkipTurn
        return [];
    }

}
