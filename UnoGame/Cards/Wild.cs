namespace UnoGame.Cards;
using UnoGame.Enums;

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
        Console.WriteLine("Change card color! \n");
        Console.WriteLine("Choose a color (Red, Yellow, Green, Blue): ");
        var inputColor = Console.ReadLine();
        while(!Enum.IsDefined(typeof(CardColor), inputColor))
        {
            Console.WriteLine("Please choose again (Red, Yellow, Green, Blue): ");
            inputColor = Console.ReadLine();
        }

        gameController.CurrentRevealedCard.Color = (CardColor)Enum.Parse(typeof(CardColor), inputColor);
        return CardType.Wild;
    }
}
