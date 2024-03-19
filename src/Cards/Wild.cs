namespace UnoGame;

public class Wild : Card
{
    public Wild(int id, CardColor color, CardType type) : base(id, color, type)
    {
        ID = id;
        Color = color;
        Type = type;
    }
    public override CardType ExecuteCardEffect(GameController gameController)
    {
        gameController.Divider.Invoke();    
        gameController.GameInfo.Invoke("Change color!");

        var inputColor = gameController.GetInput.Invoke("Choose a color (Red, Yellow, Green, Blue): ");
        object? result;
        while(!Enum.TryParse(typeof(CardColor), inputColor, true, out result))
        {
            inputColor = gameController.GetInput.Invoke("Please choose again (Red, Yellow, Green, Blue): ");
        }
        gameController.CurrentRevealedCard.Color = (CardColor) result;
        gameController.Divider.Invoke();
        return CardType.Wild;
    }
}
