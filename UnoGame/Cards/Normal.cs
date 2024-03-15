namespace UnoGame.Cards;
using UnoGame.Enums;
using UnoGame.Interface;

public class Normal : Card
{
    public Normal(int id, CardColor color, CardType type) : base(id, color, type)
    {
        ID = id;
        Color = color;
        Type = type;
    }
    public override CardType ExecuteCardEffect(GameController gameController)
    {
        return CardType.Zero;
    }

}
