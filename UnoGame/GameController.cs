namespace UnoGame;
using UnoGame.Interface;
using UnoGame.Enum;
using UnoGame.Cards;

public class GameController
{
    public event Action<IPlayer> OnTurnChange; 
    public event Action<IPlayer> CallUNO;
    public Dictionary<IPlayer, List<ICard>> PlayersHand {get; private set;}
    public IDeck CardDeck {get; private set;}
    public ICard CurrentRevealedCard {get; private set;}
    public GameRotation Rotation {get; private set;}
    public IPlayer[] WinnerOrder {get; private set;}
    public bool InsertPlayer(IPlayer player){
        return true;
    }
    public bool SetPlayerHand(IPlayer player){
        return true;
    }
    public IEnumerable<Card> GetPlayerHand(){
        return new Stack<Card>();
    }

    public IEnumerable<Player> GetWinnerOrder(){
        return new List<Player>();
    }

    public bool PlayerPlayCard(IPlayer player, ICard card){
        return true;
    }

    public bool PlayerCallUNO(IPlayer player){
        return true;
    }

    public ICard DrawCard(IPlayer player){
        // temp = cards.pop
        // playerHand.push(temp)
        return new Card();
    }

    public IEnumerable<IPlayer> SkipTurn(){
        return new List<IPlayer>();
    }

    public bool SetDeck(params ICard[] cards){
        return true;
    }
}
