namespace UnoGame;
using UnoGame.Interface;

public class Player : IPlayer
{
    public string Name{get;}
    public int ID{get;}
    public Player(string name, int id)
    {
        Name = name;
        ID = id;
    }
}
