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

        if(Enum.Parse(typeof(CardColor), inputColor).Equals("Red"))
        {
            this.Color = CardColor.Red;
        }
        
        if(Enum.Parse(typeof(CardColor), inputColor).Equals("Yellow"))
        {
            this.Color = CardColor.Yellow;
        }

        if(Enum.Parse(typeof(CardColor), inputColor).Equals("Green"))
        {
            this.Color = CardColor.Green;
        }
        if(Enum.Parse(typeof(CardColor), inputColor).Equals("Blue"))
        {
            this.Color = CardColor.Blue;
        }
        return CardType.Wild;
    }
}
