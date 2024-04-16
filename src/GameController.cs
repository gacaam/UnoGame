using Microsoft.Extensions.Logging;

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
    private ILogger<GameController>? _log;
    public GameController(IDeck deck, ILogger<GameController>? logger = null)
    {
        // Initialization
        DiscardPile = new();
        PlayersHand = new();
        Rotation = GameRotation.Clockwise;
        CardDeck = deck;
        _log = logger;
        _log?.LogInformation("Game Controller Created");
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
            _log.LogWarning($"(InsertPlayer) Player with ID {0} already in game.", player.ID);
            return false;
        }
        PlayersHand.Add(player, []);
        _log.LogInformation($"(InsertPlayer) Inserted player with ID {0} to game.", player.ID);
        return true;
    }

    public bool SetPlayerHand(IPlayer player, int numOfCards)
    {
        if(!PlayersHand.ContainsKey(player))
        {
            _log.LogWarning($"(SetPlayerHand) Player with ID {0} not added to game yet.", player.ID);
            return false;
        }
        for(int i=0; i<numOfCards; i++)
        {
            PlayersHand[player].Add(CardDeck.Cards.Pop());
        }
        _log.LogInformation($"(SetPlayerHand) Set cards to player ID {0}.", player.ID);
        return true; 
    }

    public IEnumerable<ICard> GetPlayerHand(IPlayer player)
    {
        if(!PlayersHand.ContainsKey(player))
        {
            _log.LogWarning($"(GetPlayerHand) Player with ID {0} not added to game yet.", player.ID);
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
            _log.LogWarning($"(GetPossibleCards) Player with ID {0} not added to game yet.", player.ID);
            return Enumerable.Empty<ICard>();
        }
        List<ICard> possibleCards = GetPlayerHand(player).Where(PossibleCard).ToList();
        return possibleCards;
    }

    public bool PlayerPlayCard(IPlayer player, ICard cardChosen)
    { 
        if(!PlayersHand.ContainsKey(player))
        {
            _log.LogWarning($"(PlayerPlayCard) Player with ID {0} not added to game yet.", player.ID);
            return false;
        }

        if(!GetPlayerHand(player).Contains(cardChosen))
        {
            _log.LogWarning($"(PlayerPlayCard) Card with ID {0} not in player ID {1}.", cardChosen.ID, player.ID);
            return false;
        }

        DiscardPile.Push(cardChosen);
        CurrentRevealedCard = cardChosen;
        PlayersHand[player].Remove(cardChosen);
        cardChosen.ExecuteCardEffect(this);
        _log.LogInformation($"(PlayerPlayCard) player ID {0} played {1}.", player.ID, cardChosen.Type);
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
