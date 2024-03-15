namespace UnoGame.Cards;
using UnoGame.Enums;
using UnoGame.Interface;

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
        Console.WriteLine("Choose a color (Red, Yellow, Green, Blue): ");
        var inputColor = Console.ReadLine();
        while(!Enum.IsDefined(typeof(CardColor), inputColor))
        {
            Console.WriteLine("Please choose again (Red, Yellow, Green, Blue): ");
            inputColor = Console.ReadLine();
        }

        gameController.CurrentRevealedCard.Color = (CardColor)Enum.Parse(typeof(CardColor), inputColor);
        return CardType.DrawFour;
    }

}
