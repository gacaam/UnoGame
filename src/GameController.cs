namespace UnoGame;

public class GameController
{
    public Func<string, string> GetInput {get; set;} = null!;
    public Action<string>? GameInfo {get; set;}
    public Action? Divider {get; set;}
    public GameRotation Rotation {get; private set;}
    public Stack<ICard> DiscardPile {get; private set;} 
    public IDeck CardDeck {get; private set;} 
    public ICard CurrentRevealedCard {get; set;} = null!;
    public Dictionary<IPlayer, List<ICard>> PlayersHand {get; private set;} 
    public IEnumerable<IPlayer> WinnerOrder {get; private set;} = null!;
    public IPlayer CurrentPlayer{get; private set;} = null!;
    public IPlayer NextPlayer{get; private set;} = null!;
    public int CurrentPlayerIndex {get; private set;} = 0;
    public int NextPlayerIndex {get; private set;} = 1;
    public GameController()
    {
        // Initialization
        DiscardPile = new();
        PlayersHand = new();
        Rotation = GameRotation.Clockwise;
        CardDeck = new Deck();
    }

    public ICard PrepareGame()
    {
        CardDeck.ShuffleDeck();
        CurrentPlayer = PlayersHand.Keys.ElementAt(CurrentPlayerIndex);
        NextPlayer = PlayersHand.Keys.ElementAt(NextPlayerIndex);
        // Deal cards
        foreach(IPlayer player in PlayersHand.Keys)
        {
            SetPlayerHand(player, 7);
        }

        // Initial discard card
        var firstCard = CardDeck.Draw();

        // First card cannot be a wild card
        while(firstCard.Color == CardColor.Black)
        {
            CardDeck.Cards.Push(firstCard);
            CardDeck.Cards = CardDeck.ShuffleDeck();
            firstCard = CardDeck.Draw();
        }

        DiscardPile.Push(firstCard);
        CurrentRevealedCard = firstCard;
        return firstCard;
    }
    
    public bool InsertPlayer(IPlayer player)
    {
        if(PlayersHand.ContainsKey(player))
        {
            return false;
        }
        PlayersHand.Add(player, []);
        return true;
    }

    public bool SetPlayerHand(IPlayer player, int numOfCards)
    {
        if(!PlayersHand.ContainsKey(player))
        {
            return false;
        }
        for(int i=0; i<numOfCards; i++)
        {
            PlayersHand[player].Add(CardDeck.Cards.Pop());
        }
        return true; 
    }

    public IEnumerable<ICard> GetPlayerHand(IPlayer player)
    {
        if(!PlayersHand.ContainsKey(player))
        {
            return Enumerable.Empty<ICard>();
        }
        return PlayersHand[player];
    }

    public bool PossibleCard(ICard card)
    {
        return card.Color == CurrentRevealedCard?.Color || card.Color == CardColor.Black || card.Type == CurrentRevealedCard?.Type;
    }

    public IEnumerable<ICard> GetPossibleCards(IPlayer player)
    { 
        if(!PlayersHand.ContainsKey(player))
        {
            return Enumerable.Empty<ICard>();
        }
        List<ICard> possibleCards = GetPlayerHand(player).Where(PossibleCard).ToList();
        return possibleCards;
    }

    public bool PlayerPlayCard(IPlayer player, ICard cardChosen)
    { 
        if(!PlayersHand.ContainsKey(player))
        {
            return false;
        }

        if(!GetPlayerHand(player).Contains(cardChosen))
        {
            return false;
        }

        DiscardPile.Push(cardChosen);
        CurrentRevealedCard = cardChosen;
        PlayersHand[player].Remove(cardChosen);
        cardChosen.ExecuteCardEffect(this);
        return true;
    }
    
    public ICard PlayerDrawCard(IPlayer player)
    {
        var drawnCard = CardDeck.Draw();
        PlayersHand[player].Add(drawnCard);
        return drawnCard;
    }

    public void ChangeRotation()
    {
        if(Rotation == GameRotation.Clockwise){
            Rotation = GameRotation.CounterClockwise;
        }else{
            Rotation = GameRotation.Clockwise;
        }
    }

    public void NextTurn()
    {
        if(Rotation == GameRotation.Clockwise){
            CurrentPlayerIndex = (CurrentPlayerIndex + 1) % PlayersHand.Count;
            NextPlayerIndex = (CurrentPlayerIndex + 1) % PlayersHand.Count;
        }else{
            CurrentPlayerIndex = (CurrentPlayerIndex + PlayersHand.Count - 1) % PlayersHand.Count;
            NextPlayerIndex = (CurrentPlayerIndex + PlayersHand.Count - 1) % PlayersHand.Count;
        }
        CurrentPlayer = PlayersHand.Keys.ElementAt(CurrentPlayerIndex);
        NextPlayer = PlayersHand.Keys.ElementAt(NextPlayerIndex);
    }

    public IEnumerable<IPlayer> GetWinnerOrder()
    { 
        WinnerOrder = PlayersHand.OrderBy(player => player.Value.Count).
                        ToDictionary(player => player.Key, player => player.Value).Keys.ToList();
        return WinnerOrder;
    }

    public bool CheckEmptyHand()
    {
        return PlayersHand.Any(player => player.Value.Count == 0);
    }


}
