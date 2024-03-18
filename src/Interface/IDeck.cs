namespace UnoGame.Interface;

public interface IDeck
{
    public Stack<ICard> Cards {get; set;}
    public Stack<ICard> ShuffleDeck();
    public ICard Draw();
}
