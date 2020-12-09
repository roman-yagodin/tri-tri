public interface IGame
{
    IBoard Board { get; set; }

    IDeal LeftDeal { get; set; }

    IDeal RightDeal { get; set; }
}

public class Game: IGame
{
    public IBoard Board { get; set; }

    public IDeal LeftDeal { get; set; }

    public IDeal RightDeal { get; set; }

    public Game ()
    {
        var dealer = new Dealer ();

        var leftDeck = CardFactory.CreateFullUniqueDeck ();
		LeftDeal = dealer.Deal (leftDeck, 5, CardOwner.Red);
        LeftDeal.IsOpen = false;

		var rightDeck = CardFactory.CreateFullUniqueDeck ();
		RightDeal = dealer.Deal (rightDeck, 5, CardOwner.Blue);
        RightDeal.IsOpen = true;

        Board = new Board (3, 3);
    }
}
