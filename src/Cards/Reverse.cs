namespace UnoGame.Cards;
using UnoGame.Enums;
using UnoGame.Interface;

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
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine("Reverse!\n");
        gameController.ChangeRotation();
        Console.WriteLine("------------------------------------------------");
        return CardType.Reverse;
    }

}
