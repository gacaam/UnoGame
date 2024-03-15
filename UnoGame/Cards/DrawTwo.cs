namespace UnoGame.Cards;
using UnoGame.Enums;
using UnoGame.Interface;

public class DrawTwo : Card
{
    public DrawTwo(int id, CardColor color, CardType type) : base(id, color, type)
    {
        ID = id;
        Color = color;
        Type = type;
    }
    public override CardType ExecuteCardEffect(GameController gameController)
    {
        for(int i=0; i<2; i++)
        {
            gameController.PlayerDrawCard(gameController.NextPlayer);
        }
        return CardType.DrawFour;
    }

}
