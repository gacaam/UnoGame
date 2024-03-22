namespace UnoGame;

public class WildDrawFour : Card
{
    public WildDrawFour(int id, CardColor color, CardType type) : base(id, color, type)
    {
        ID = id;
        Color = color;
        Type = type;
    }
    public override CardType ExecuteCardEffect(GameController gameController)
    {
        gameController.Divider.Invoke();
        var inputColor = gameController.GetInput.Invoke("Choose a color (Red, Yellow, Green, Blue): ");
        object? result;
        while(!Enum.TryParse(typeof(CardColor), inputColor, true, out result))
        {
            inputColor = gameController.GetInput.Invoke("Please choose again (Red, Yellow, Green, Blue): ");
        }

        gameController.Divider.Invoke();
        gameController.GameInfo.Invoke("Draw four cards >:) \n");
        for(int i=0; i<4; i++)
        {
            gameController.PlayerDrawCard(gameController.NextPlayer);
        }

        gameController.CurrentRevealedCard.Color = (CardColor) result;
        gameController.ChangeCurrentPlayer();
        gameController.Divider.Invoke();
        return CardType.DrawFour;
    }

}
