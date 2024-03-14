namespace UnoGame.Cards;
using UnoGame.Enums;
using UnoGame.Interface;

public class WildDrawFour : Card
{
    public WildDrawFour(int id, CardColor color, CardType type) : base(id, color, type)
    {
        ID = id;
        color = Color;
        type = Type;
    }
    public override CardType ExecuteCardEffect(GameController gameController)
    {
        for(int i=0; i<4; i++)
        {
            gameController.DrawCard();
        }
        return CardType.DrawFour;
    }

}
