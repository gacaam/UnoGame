# Online UNO  
This is a console app implementation of the classic game UNO, built using C#. 

## Running the app
Running the console app requires an installation of the [.NET framework](https://dotnet.microsoft.com/en-us/download). Download the 'src' folder along with the Uno.sln file into the same directory. Open console and build the Uno.sln file. 
```console

dotnet build [PATH\Uno.sln]
```
Now run the UnoGame.csproj inside src.
```console

dotnet run --project [PATH\src\UnoGame.csproj]
```

## Basic rules
This online UNO was designed to be played by 2-4 players
1. Each player is initially dealt with 7 random cards from a center deck.
2. All players take turn to discard a card from their hand into the discard pile, by matching either the color or value with the last played card, or by playing wild cards (cards with undetermined colors).
3. The player that first finishes playing all cards in their hand wins the game.

## UNO deck
An entire deck of cards is comprised of 108 cards
![896px-UNO_cards_deck svg](https://github.com/gacaam/UnoGame/assets/89449970/b894522c-cae9-4bd9-8596-00a1feb4001e)


