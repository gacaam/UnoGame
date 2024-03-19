namespace UnoGame;

public class GameController
{
    public Func<string, string> GetInput; 
    public Action OnTurnChange;
    public Action<string> GameInfo;
    public Action Divider;
    public GameRotation Rotation {get; private set;}
    public Stack<ICard> DiscardPile {get; private set;} 
    public IDeck CardDeck {get; private set;} 
    public ICard CurrentRevealedCard {get; set;}
    public Dictionary<IPlayer, List<ICard>> PlayersHand {get; private set;} 
    public List<IPlayer> players;
    public IEnumerable<IPlayer> WinnerOrder {get; private set;} //TODO: Winner Order
    public IPlayer CurrentPlayer{get; private set;}
    public IPlayer NextPlayer{get; private set;}
    public int CurrentPlayerIndex {get; private set;}
    public int NextPlayerIndex {get; private set;}
    public GameController()
    {
        // Initialization
        DiscardPile = new();
        PlayersHand = new();
        Rotation = GameRotation.Clockwise;
        CardDeck = new Deck();
        CardDeck.Cards = CardDeck.ShuffleDeck();
        OnTurnChange = ChangeCurrentPlayer;
    }
    public async Task StartGame(int numOfPlayers)
    {
        // Add players & deal players' cards
        for(int i=0; i<numOfPlayers; i++)
        {
            string playerName = GetInput.Invoke($"Enter Player {i+1}'s Name:");

            // Set default name if input is empty
            if(String.IsNullOrEmpty(playerName))
            {
                playerName = $"Player{i+1}";
            }
            Player newPlayer = new(playerName, i);
            InsertPlayer(newPlayer);
            SetPlayerHand(newPlayer, 7);
        }

        players = PlayersHand.Keys.ToList();

        // Initial discard card
        var firstCard = CardDeck.Draw();
        GameInfo.Invoke("Drawing first card..");

        // First card cannot be a wild card
        while(firstCard.Color == CardColor.Black)
        {
            GameInfo.Invoke("Oops, wild card. Draw again!");
            await Task.Delay(1000);
            CardDeck.Cards.Push(firstCard);
            CardDeck.Cards = CardDeck.ShuffleDeck();
            firstCard = CardDeck.Draw();
        }

        DiscardPile.Push(firstCard);
        CurrentRevealedCard = firstCard;
        GameInfo.Invoke($"First Card: {Enum.GetName(typeof(CardType), firstCard.Type)} {Enum.GetName(typeof(CardColor), firstCard.Color)}");
        await Task.Delay(1500);

        // Game play
        // Set initial player and next player
        CurrentPlayerIndex = 0;
        NextPlayerIndex = CurrentPlayerIndex + 1;
        CurrentPlayer = players[CurrentPlayerIndex];
        NextPlayer = players[NextPlayerIndex];

        GameInfo.Invoke("Starting Game!");
        await Task.Delay(1500);
        while(true)
        {
            if(PlayersHand[CurrentPlayer].Count==0){
                GameInfo.Invoke($"Congratulations! {CurrentPlayer.Name} has won :D ");
                break;
            }
            await PlayerTurn();
            OnTurnChange.Invoke();
        }
    }

    public void InsertPlayer(IPlayer player){
        PlayersHand.Add(player, []);
    }

    public bool SetPlayerHand(IPlayer player, int numOfCards){
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

    public IEnumerable<ICard> GetPlayerHand(IPlayer player){
        return PlayersHand[player];
    }

    public bool PossibleCard(ICard card){
        return card.Color == CurrentRevealedCard.Color || card.Color == CardColor.Black || card.Type == CurrentRevealedCard.Type;
    }

    public List<ICard> GetPossibleCards(IPlayer player){ 
        List<ICard> possibleCards = PlayersHand[player].FindAll(PossibleCard);
        return possibleCards;
    }

    public bool PlayerPlayCard(IPlayer player, ICard cardChosen){ 
        if(!PlayersHand.ContainsKey(player))
        {
            return false;
        }

        if(!PlayersHand[player].Contains(cardChosen))
        {
            return false;
        }

        DiscardPile.Push(cardChosen);
        CurrentRevealedCard = cardChosen;
        PlayersHand[player].Remove(cardChosen);

        GameInfo.Invoke($"{player.Name} plays {Enum.GetName(typeof(CardType), cardChosen.Type)} {Enum.GetName(typeof(CardColor), cardChosen.Color)}");
        cardChosen.ExecuteCardEffect(this);
        return true;
    }
    
    public ICard PlayerDrawCard(IPlayer player){
        var drawnCard = CardDeck.Draw();
        PlayersHand[player].Add(drawnCard);
        GameInfo.Invoke($"{player.Name} draws a card");
        return drawnCard;
    }

    public void ChangeRotation(){
        if(Rotation == GameRotation.Clockwise){
            Rotation = GameRotation.CounterClockwise;
        }else{
            Rotation = GameRotation.Clockwise;
        }
    }

    public async Task PlayerTurn(){
        List<ICard> possibleCards = GetPossibleCards(CurrentPlayer);
        List<ICard> otherCards = PlayersHand[CurrentPlayer].Where(cards => !possibleCards.Contains(cards)).ToList();

        GameInfo.Invoke($"Last Card Played: {Enum.GetName(typeof(CardType), CurrentRevealedCard.Type)} {Enum.GetName(typeof(CardColor), CurrentRevealedCard.Color)}");
        GameInfo.Invoke($"{CurrentPlayer.Name}'s turn");

        if(possibleCards.Count>0)
        {
            GameInfo.Invoke("(Available cards)");
            for(int i = 0; i < possibleCards.Count; i++){
                var card = possibleCards[i];
                GameInfo.Invoke($"\t{i+1}. {Enum.GetName(typeof(CardType), card.Type)} {Enum.GetName(typeof(CardColor), card.Color)}");
            }

            GameInfo.Invoke("(Other cards in hand)");
            for(int i = 0; i < otherCards.Count; i++){
                var card = otherCards[i];
                GameInfo.Invoke($"\t{i+possibleCards.Count+1}. {Enum.GetName(typeof(CardType), card.Type)} {Enum.GetName(typeof(CardColor), card.Color)}");
            }

            var input = GetInput.Invoke("Choose a card by index: ");
            int indexVal;
            while(!int.TryParse(input, out indexVal) || indexVal-1 > possibleCards.Count){
                input = GetInput.Invoke("Try again... Choose only available cards by index (ex: 1)");
            }
            PlayerPlayCard(CurrentPlayer, possibleCards[indexVal-1]);

        }else{
            GameInfo.Invoke("(No available cards to play)");
            GameInfo.Invoke("(Cards in hand)");
            for(int i = 0; i < otherCards.Count; i++){
                var card = otherCards[i];
                GameInfo.Invoke($"\t{i+1}. {Enum.GetName(typeof(CardType), card.Type)} {Enum.GetName(typeof(CardColor), card.Color)}");
            }

            GameInfo.Invoke("No cards to play... drawing card");
            await Task.Delay(2000);
            var newCard = PlayerDrawCard(CurrentPlayer);
            GameInfo.Invoke($"Drawn Card: {Enum.GetName(typeof(CardType), newCard.Type)} {Enum.GetName(typeof(CardColor), newCard.Color)}");
            
            if(PossibleCard(newCard)){
                PlayerPlayCard(CurrentPlayer,newCard);
            }
            await Task.Delay(1500);
        }
        await Task.Delay(2500);
    }
    
    public void ChangeCurrentPlayer()
    {
        if(Rotation == GameRotation.Clockwise){
            CurrentPlayerIndex = (CurrentPlayerIndex + 1) % PlayersHand.Count;
            NextPlayerIndex = (CurrentPlayerIndex + 1) % PlayersHand.Count;
        }else{
            CurrentPlayerIndex = (CurrentPlayerIndex + PlayersHand.Count - 1) % PlayersHand.Count;
            NextPlayerIndex = (CurrentPlayerIndex + PlayersHand.Count - 1) % PlayersHand.Count;
        }
        CurrentPlayer = players[CurrentPlayerIndex];
        NextPlayer = players[NextPlayerIndex];
    }

    //TODO: GetWinnerOrder
    public IEnumerable<IPlayer> GetWinnerOrder(){ 
        WinnerOrder = PlayersHand.OrderBy(player => player.Value.Count).
                        ToDictionary(player => player.Key, player => player.Value).Keys.ToList();
        return WinnerOrder;
    }
}
