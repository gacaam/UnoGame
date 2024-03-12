namespace UnoGame.Cards;
using UnoGame.Enum;

public class Skip : Card
{
    public Skip(int id, CardColor color, CardType type) : base(id, color, type)
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
