namespace UnoGame;
using UnoGame.Enum;
using UnoGame.Interface;

public abstract class Card : ICard
{
    public Card(int id, CardColor color, CardType type)
    {
        ID = id;
        color = Color;
        type = Type;
    }

    public int ID{get; protected set;}
    public CardType Type{get; protected set;}
    public CardColor Color{get; protected set;}
    public virtual CardType ExecuteCardEffect(GameController gameController){

        return new CardType();
    } 
}
