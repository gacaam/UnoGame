namespace UnoGame;
public interface ICard
{
    public int ID {get;}
    public string Name{get;}
    public CardType Type{get;}
    public CardColor Color{get;}
}
public abstract class Card : ICard
{
    public Card(int id, string name, CardColor color, CardType type)
    {
        ID = id;
        name = "Name";
        color = Color;
        type = Type;
    }

    public int ID{get; protected set;}
    public string Name{get; protected set;}
    public CardType Type{get; protected set;}
    public CardColor Color{get; protected set;}
    public virtual CardType ExecuteCardEffect(GameController gameController){

        return new CardType();
    } 
}
