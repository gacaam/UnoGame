namespace UnoGame;
using UnoGame.Cards;

public interface IDeck
{
    public int ID {get;}
    public string Name {get;}
    
}
public class Deck : IDeck
{
    public int ID {get;}
    public string Name {get;}
    public Queue<Card> deck {get; set;}
    public Deck(int id, string name){
        ID = id;
        Name = name;
    }
    public bool ShuffleDeck(){
        return true;
    }
    // public ICard Draw(){
    //     return new Card();
    // }
}
