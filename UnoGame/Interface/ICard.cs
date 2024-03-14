namespace UnoGame.Interface;
using UnoGame.Enums;

public interface ICard
{
    public int ID {get;}
    public CardType Type{get;}
    public CardColor Color{get;}
}
