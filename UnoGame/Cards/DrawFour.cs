namespace UnoGame.Cards;
using UnoGame.Enums;
using UnoGame.Interface;

public class WildDrawFour : Card
{
    public WildDrawFour(int id, CardColor color, CardType type) : base(id, color, type)
    {
        ID = id;
        color = Color;
        type = Type;
    }
    public override CardType ExecuteCardEffect(GameController gameController)
    {
        Console.WriteLine("Choose a color (red, yellow, green, blue): ");
        var inputColor = Console.ReadLine();
        while(!Enum.IsDefined(typeof(CardColor), inputColor))
        {
            Console.WriteLine("Please choose again (red, yellow, green, blue): ");
            inputColor = Console.ReadLine();
        }

        if(Enum.Parse(typeof(CardColor), inputColor).Equals("Red"))
        {
            
        }

        for(int i=0; i<4; i++)
        {
            gameController.PlayerDrawCard(gameController.NextPlayer);
        }
        return CardType.DrawFour;
    }

}
