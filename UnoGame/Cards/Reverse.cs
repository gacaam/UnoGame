namespace UnoGame.Cards;

public class Reverse : Card
{
    public Reverse(int id, string name, CardColor color, CardType type) : base(id, name, color, type)
    {
        ID = id;
        name = Name;
        color = Color;
        type = Type;
    }
    public override CardType ExecuteCardEffect(GameController gameController)
    {
        return base.ExecuteCardEffect(gameController);
    }

}
