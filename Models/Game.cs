using System;
using Godot;

public interface IGame
{
	IBoard Board { get; set; }

	IPlayer Player1 { get; set; }

	IPlayer Player2 { get; set; }

	GameState State { get; set; }

	bool IsOver ();

	event Action<object, EventArgs> OnStateChanged;
}

public class SampleGame: IGame
{
	public IBoard Board { get; set; }

	public IPlayer Player1 { get; set; }

	public IPlayer Player2 { get; set; }

	private GameState _state = GameState.GameOver;

	public GameState State {
		get => _state;
		set {
			_state = value;
			if (OnStateChanged != null) {
				OnStateChanged (this, EventArgs.Empty);
			}
		}
	}

	public event Action<object, EventArgs> OnStateChanged;

	public SampleGame ()
	{
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

	public void Start ()
	{
		GD.Print ("---");
		GD.Print ("Game started!");

		State = GameState.WaitForPlayer;
	}

	public void PlayerTurn (int cardIdx)
	{
		State = GameState.PlayerTurn;

		var ctr = new PlayCardThinkResult {
			CardIndex = cardIdx,
			BoardCoords = Board.TryGetRandomEmptyTile ()
		};

		var canPlayCard = Player2.CanPlayCard (Board, ctr);
		if (!canPlayCard) {
			State = GameState.WaitForPlayer;
			return;
		}

		Player2.PlayCard (Board, ctr);
		
		State = GameState.WaitForEnemy;

		EndTurn ();
	}

	public void EnemyTurn ()
	{
		State = GameState.EnemyTurn;

		var ai = new RandomAI ();
		var ctr = ai.ThinkOnPlayCard (Board, Player1.Deal);
		Player1.PlayCard (Board, ctr);

		State = GameState.WaitForPlayer;
		
		EndTurn ();
	}

	void EndTurn ()
	{
		if (IsOver ()) {
			CheckResult ();

			GD.Print ("---");
			GD.Print ("Press N to start new game.");

			State = GameState.GameOver;
		}
	}

	void CheckResult ()
	{
		GD.Print ("---");
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
