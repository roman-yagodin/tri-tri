using Godot;

public interface IGame
{
	IBoard Board { get; set; }

	IPlayer Player1 { get; set; }

	IPlayer Player2 { get; set; }

	bool IsOver ();
}

public class SampleGame: IGame
{
	public IBoard Board { get; set; }

	public IPlayer Player1 { get; set; }

	public IPlayer Player2 { get; set; }

	public SampleGame ()
	{
		var dealer = new Dealer ();

		Player1 = new Player ();
		Player2 = new Player ();

		var deck1 = CardFactory.CreateFullUniqueDeck ();
		Player1.Deal = dealer.Deal (deck1, 5, CardOwner.Red);
		Player1.Deal.IsOpen = false;

		var deck2 = CardFactory.CreateFullUniqueDeck ();
		Player2.Deal = dealer.Deal (deck2, 5, CardOwner.Blue);
		Player2.Deal.IsOpen = true;

		Board = new Board (3, 3);
	}

	public void Player2Turn (int cardIdx)
	{
		var player2PlayedCard = Player2.PlayCard (Board, new PlayCardThinkResult {
			CardIndex = cardIdx,
			BoardCoords = Board.TryGetRandomEmptyTile ()
		});

		if (!player2PlayedCard) {
			return;
		}

		if (IsOver ()) {
			GD.Print ("Game over!");
			return;
		}

		var ai = new RandomAI ();
		var cr = ai.ThinkOnPlayCard (Board, Player1.Deal);
		Player1.PlayCard (Board, cr);
	}

	public bool IsOver () => Board.IsFull ();
}
