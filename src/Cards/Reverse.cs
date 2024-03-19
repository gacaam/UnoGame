namespace UnoGame;
public class Reverse : Card
{
    public Reverse(int id, CardColor color, CardType type) : base(id, color, type)
    {
        ID = id;
        Color = color;
        Type = type;
    }
    public override CardType ExecuteCardEffect(GameController gameController)
    {
        gameController.Divider.Invoke();    
        gameController.GameInfo.Invoke("Reverse!\n");
        gameController.ChangeRotation();
        gameController.Divider.Invoke();    
        return CardType.Reverse;
    }

}
