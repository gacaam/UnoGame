namespace UnoGame;
using UnoGame.Interface;
using UnoGame.Enums;
using UnoGame.Cards;

public class GameController
{
    public event Action<IPlayer> OnTurnChange; 
    public event Action<IPlayer> CallUNO;
    public Dictionary<IPlayer, List<ICard>> PlayersHand {get; private set;}
    public Deck CardDeck {get; private set;}
    public ICard CurrentRevealedCard {get; private set;}
    public GameRotation Rotation {get; private set;}
    public IPlayer[] WinnerOrder {get; private set;}
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
        return true;
    }

    public bool PlayerCallUNO(IPlayer player){
        // event CallUNO
        Console.WriteLine();
        return true;
    }

    public ICard DrawCard(IPlayer player){
        var drawnCard = CardDeck.Draw();
        PlayersHand[player].Add(drawnCard);
        Console.WriteLine($"{player.Name} draws a {Enum.GetName(typeof(CardType), drawnCard.Type)}{Enum.GetName(typeof(CardColor), drawnCard.Color)} card");
        return drawnCard;
    }

    public IEnumerable<IPlayer> SkipTurn(){
        return new List<IPlayer>();
    }

    public bool SetDeck(params ICard[] cards){

        return true;
    }
}
