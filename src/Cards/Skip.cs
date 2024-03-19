namespace UnoGame;

public class Skip : Card
{
    public Skip(int id, CardColor color, CardType type) : base(id, color, type)
    {
        ID = id;
        Color = color;
        Type = type;
    }
    public override CardType ExecuteCardEffect(GameController gameController)
    {
        gameController.Divider.Invoke();    
        gameController.GameInfo.Invoke($"Skipping {gameController.NextPlayer.Name}'s turn!\n");
        gameController.ChangeCurrentPlayer();
        gameController.Divider.Invoke();
        return CardType.Skip;
    }

}
