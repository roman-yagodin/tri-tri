using Godot;

public interface IGame
{
	IBoard Board { get; set; }

	IPlayer Player1 { get; set; }

	IPlayer Player2 { get; set; }

	GameState State { get; set; }

	bool IsOver ();
}

public class SampleGame: IGame
{
	public IBoard Board { get; set; }

	public IPlayer Player1 { get; set; }

	public IPlayer Player2 { get; set; }

	public GameState State { get; set; }

	public SampleGame ()
	{
		State = GameState.PlayerTurn;

		var dealer = new Dealer ();

		Player1 = new Player ();
		Player2 = new Player ();

		Player1.Score = 5;
		Player2.Score = 5;

		var cardFactory = new CardFactory ();
		var deck1 = cardFactory.CreateFullUniqueDeck ();
		Player1.Deal = dealer.Deal (deck1, 5, CardOwner.Red);
		Player1.Deal.IsOpen = false;

		var deck2 = cardFactory.CreateFullUniqueDeck ();
		Player2.Deal = dealer.Deal (deck2, 5, CardOwner.Blue);
		Player2.Deal.IsOpen = true;

		Board = new Board (this, 3, 3);
	}

	public void PlayerTurn (int cardIdx)
	{
		var playerPlayedCard = Player2.PlayCard (Board, new PlayCardThinkResult {
			CardIndex = cardIdx,
			BoardCoords = Board.TryGetRandomEmptyTile ()
		});
		
		if (!playerPlayedCard) {
			return;
		}

		State = GameState.EnemyTurn;

		EndTurn ();
	}

	public void EnemyTurn ()
	{
		var ai = new RandomAI ();
		var cr = ai.ThinkOnPlayCard (Board, Player1.Deal);
		Player1.PlayCard (Board, cr);

		State = GameState.PlayerTurn;
		
		EndTurn ();
	}

	void EndTurn ()
	{
		if (IsOver ()) {
			CheckResult ();

			GD.Print ("Press N to start new game.");

			State = GameState.GameOver;
		}
	}

	void CheckResult ()
	{
		if (Player2.Score > Player1.Score) {
			GD.Print ("You win!");
		}
		else if (Player2.Score < Player1.Score) {
			GD.Print ("You lose!");
		}
		else {
			GD.Print ("Draw");
		}
	}

	public bool IsOver () => Board.IsFull ();
}
