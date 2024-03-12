namespace UnoGame.Cards;

public class DrawTwo : Card
{
    public DrawTwo(int id, CardColor color, CardType type) : base(id, color, type)
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
