namespace UnoGame.Interface;
using UnoGame.Enum;

public interface ICard
{
    public int ID {get;}
    public CardType Type{get;}
    public CardColor Color{get;}
}
