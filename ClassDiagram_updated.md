```mermaid
---
title: Uno Card Game
---
classDiagram
    %% https://service.mattel.com/instruction_sheets/42003-Wild.pdf
    note "note: base graph"
    class CardColour{
        <<Enumeration>>
        Black,
        Blue,
        Red,
        Yellow,
        Green
    }

    class CardType{
        <<Enumeration>>
        0,
        1,
        2,
        3,
        4,
        5,
        6,
        7,
        8,
        9,
        Skip,
        Reverse,
        DrawTwo,
        Wild,
        DrawFour
    }

    class ICard{
        <<Interface>>
        +int ID {get;}
        +CardColor Color {get;}
        +CardType Type {get;}
    }

    class Card{
        <<abstract>>
        +Card(int id, CardColor color, CardType type)
        +abstract CardType ExecuteCardEffect(GameController gameController)
    }

    Card ..|> ICard : Realization
    Deck ..|> IDeck : Realization
    IPlayer <|.. Player : Realization
    GameController <|.. GameController : Realization

    CardType --* ICard : Composition
    CardColor  --* ICard : Composition
    
    

    Card <|-- DrawFour : Inheritance
    Card <|-- Wild : Inheritance
    Card <|-- DrawTwo : Inheritance
    Card <|-- Reverse : Inheritance
    Card <|-- Skip : Inheritance
    Card <|-- Normal : Inheritance

    DrawFour --* IDeck : Composition
    Wild --* IDeck : Composition
    DrawTwo --* IDeck : Composition
    Reverse --* IDeck : Composition
    Skip --* IDeck : Composition
    Normal --* IDeck : Composition

    ICard "*" --* "1" GameController : Composition
    IDeck "*" --* "1" GameController : Composition
    GameController "1" *-- "2..10" IPlayer : Composition
    GameController *-- GameRotation  : Composition
    
    class IPlayer{
        <<Interface>>
        +int ID {get;}
        +string Name {get;}
    }

    class Player{       
        +Player(int id, string name)
    }

    class DrawFour{
        ExecuteCardEffect(GameController gc)
    }

    class Wild{
        ExecuteCardEffect(GameController gc)
    }

    class DrawTwo{
        ExecuteCardEffect(GameController gc)
    }

    class Reverse{
        ExecuteCardEffect(GameController gc)
    }

    class Skip{
        ExecuteCardEffect(GameController gc)
    }

    class Normal{
        ExecuteCardEffect(GameController gc)
    }

    class IDeck{
        <<Interface>>
        +Stack~ICard~ Cards {get; set;}
        +ShuffleDeck() : Stack~ICard~
        +Draw() : ICard
    }

    class Deck{
        +Stack~ICard~ Cards {get; set;}
        +Deck()
        +Deck(Stack ~ICard~ cards)
        +ShuffleDeck() : Stack~ICard~
        +Draw() : ICard
    }
    
    class GameRotation{
        <<Enumeration>>
        Clockwise,
        CounterClockwise,
    }

    class GameController{
        +Func~string, string~ GetInput {get; set;} 
        +Action~string~ GameInfo {get; set;}
        +Action Divider {get; set;}
        +GameRotation Rotation {get; private set;}
        +Stack~ICard~ DiscardPile {get; private set;} 
        +IDeck CardDeck {get; private set;} 
        +ICard CurrentRevealedCard {get; set;} 
        +Dictionary~IPlayer, List<.ICard>~ PlayersHand {get; private set;} 
        +IEnumerable<IPlayer> WinnerOrder {get; private set;} 
        +IPlayer CurrentPlayer{get; private set;} 
        +IPlayer NextPlayer{get; private set;} 
        +int CurrentPlayerIndex {get; private set;} 
        +int NextPlayerIndex {get; private set;} 
        
        +InsertPlayer(IPlayer player) : bool
        +SetPlayerHand(IPlayer p, IEnumerable~Card~ cards) : bool
        +GetPossibleCard(IPlayer p) : IEnumerable~Card~
        +GetWinnerOrder() : IEnumerable~Player~

        %% Player Action
        +PlayCard(IPlayer p,ICard c) : bool
        +CallUNO(IPlayer p) : bool
        +ChangeRotation(GameRotation gr) : bool
        +RotateAllPlayerCard() : bool
        +SwapPlayerCard(IPlayer p1, IPlayer p2) :bool
        
        %% Game System & Player Action
        +PlayerDrawCard() : ICard
        +NextTurn() : IEnumerable~IPlayer~
        +SetDeck(params ICard) : bool
        +GetDeck() : IEnumerable~ICard~
    }

```

