namespace UnoGame.Cards;
using UnoGame.Enums;

public class Reverse : Card
{
    public Reverse(int id, CardColor color, CardType type) : base(id, color, type)
    {
        ID = id;
        color = Color;
        type = Type;
    }
    public override CardType ExecuteCardEffect(GameController gameController)
    {
        return base.ExecuteCardEffect(gameController);
    }

}
