namespace UnoGame;

using System.Collections;
using System.ComponentModel;
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
    public Queue<Card> CardDeck {get; set;}
    public Deck(int id, string name)
    {
        ID = id;
        Name = name;
    }
    public bool ShuffleDeck()
    
    {
        return true;
    }
    // public ICard Draw(){
    //     return new Card();
    // }c
    public void CreateDeck()
    {
        foreach(CardColor color in Enum.GetValues(typeof(CardColor)))
        {
            // Add normal cards
            foreach(CardType type in Enum.GetValues(typeof(CardType)))
            {
                switch(type)
                {
                    case CardType.Zero:
                    case CardType.One:
                    case CardType.Two:
                    case CardType.Three:
                    case CardType.Four:
                    case CardType.Five:
                    case CardType.Six:
                    case CardType.Seven:
                    case CardType.Eight:
                    case CardType.Nine:
                        for(int i=0; i<2; i++)
                        {
                            CardDeck.Enqueue
                            (
                                new Normal()
                            );
                        }
                        break;
                }
            }
        }
    }
}
