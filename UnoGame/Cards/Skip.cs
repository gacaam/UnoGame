namespace UnoGame.Cards;
using UnoGame.Enums;
using UnoGame.Interface;

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
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine($"Skipping {gameController.NextPlayer.Name}'s turn!\n");
        gameController.NextTurn();
        Console.WriteLine("------------------------------------------------");
        return CardType.Skip;
    }

}
