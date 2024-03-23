namespace UnoGame;

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
        gameController.Divider.Invoke();
        gameController.GameInfo.Invoke("Draw two cards >:) \n");
        for(int i=0; i<2; i++)
        {
            gameController.PlayerDrawCard(gameController.NextPlayer);
        }
        gameController.NextTurn();
        gameController.Divider.Invoke();
        return CardType.DrawFour;
    }

}
