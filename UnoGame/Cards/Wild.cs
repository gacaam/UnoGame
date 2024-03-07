namespace UnoGame.Cards;

public class Wild : Card
{
    public Wild(int id, string name, CardColor color, CardType type) : base(id, name, color, type)
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
