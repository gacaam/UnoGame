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
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine("Choose a color (Red, Yellow, Green, Blue): ");
        var inputColor = Console.ReadLine(); 
        //TODO: func.Invoke() in ExecuteCardEffect
        //func in game controller 
        //methods to subscribe in Program.cs
        object result;
        while(!Enum.TryParse(typeof(CardColor), inputColor, true, out result))
        {
            Console.WriteLine("Please choose again (Red, Yellow, Green, Blue): ");
            inputColor = Console.ReadLine();
        }
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine("Draw four cards >:) \n");
        for(int i=0; i<4; i++)
        {
            gameController.PlayerDrawCard(gameController.NextPlayer);
        }
        gameController.CurrentRevealedCard.Color = (CardColor) result;
        Console.WriteLine("------------------------------------------------");
        return CardType.DrawFour;
    }

}
