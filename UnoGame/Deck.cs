namespace UnoGame;
using UnoGame.Enum;

using System.Collections;
using System.ComponentModel;
using UnoGame.Cards;

public interface IDeck
{
    public int ID {get;}
    
}
public class Deck : IDeck
{
    public int ID {get;}
    public List<Card> Cards {get; set;} = [];

    // constructors
    public Deck(int id, List<Card> cards)
    {
        ID = id;
        Cards = cards;
    }
    public Deck(List<Card> cards)
    {
        Cards = cards;
    }

    public Deck(int id)
    {
        ID = id;
    }
    // parameterless constructor
    public Deck()
    {
    }
    public bool ShuffleDeck()
    
    {
        return true;
    }
    // public ICard Draw(){
    //     return new Card();
    // }c
    public void GenerateDeck()
    {                           
        // Add four draw 4 & wild cards
        for(int i=0; i<4; i++)
        {
            Cards.Add(new WildDrawFour(i, CardColor.Black, CardType.DrawTwo));
            Cards.Add(new Wild(i, CardColor.Black, CardType.Wild));
        }

        foreach(CardColor color in CardColor.GetValues(typeof(CardColor)))
        {
            if (color != CardColor.Black)
            {
                foreach(CardType type in CardType.GetValues(typeof(CardType)))
                {
                    switch(type)
                    {
                        // Add one 0 card in each color
                        case CardType.Zero:
                            Cards.Add(new Normal(0, color, type));
                            break;

                        // Add two 1-9 cards in each color 
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
                                Cards.Add(new Normal(i, color, type));
                            }
                            break;

                        // Add two skip cards in each color
                        case CardType.Skip:
                            for(int i=0; i<2; i++)
                            {
                                Cards.Add(new Skip(i, color, type));
                            }
                            break;

                        // Add two reverse cards in each color
                        case CardType.Reverse:
                            for(int i=0; i<2; i++)
                            {
                                Cards.Add(new Reverse(i, color, type));
                            }
                            break;

                        // Add two draw 2 cards in each color
                        case CardType.DrawTwo:
                            for(int i=0; i<2; i++)
                            {
                                Cards.Add(new DrawTwo(i, color, type));
                            }
                            break;
                    }
                }
            }
        }
    }
}
