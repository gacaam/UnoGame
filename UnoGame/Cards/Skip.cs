namespace UnoGame.Cards;
using UnoGame.Enums;
using UnoGame.Interface;

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
        gameController.SkipTurn();
        return CardType.Skip;
    }

}
