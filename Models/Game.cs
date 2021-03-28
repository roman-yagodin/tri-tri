using System;
using Godot;

public interface IGame
{
	IBoard Board { get; set; }

	IPlayer Enemy { get; set; }

	IPlayer Player { get; set; }

	GameState State { get; set; }

	bool IsOver ();

	event Action<object, EventArgs> OnStateChanged;
}

public class SampleGame: IGame
{
	public IBoard Board { get; set; }

	public IPlayer Enemy { get; set; }

	public IPlayer Player { get; set; }

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

		Enemy = new Player ();
		Player = new Player ();

		Enemy.Score = 5;
		Player.Score = 5;

		var cardFactory = new CardFactory ();
		var deck1 = cardFactory.CreateFullUniqueDeck ();
		Enemy.Deal = dealer.Deal (deck1, 5, CardOwner.Red);
		Enemy.Deal.IsOpen = false;

		var deck2 = cardFactory.CreateFullUniqueDeck ();
		Player.Deal = dealer.Deal (deck2, 5, CardOwner.Blue);
		Player.Deal.IsOpen = true;

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

		var canPlayCard = Player.CanPlayCard (Board, ctr);
		if (!canPlayCard) {
			State = GameState.WaitForPlayer;
			return;
		}

		Player.PlayCard (Board, ctr);
		
		State = GameState.WaitForEnemy;

		EndTurn ();
	}

	public void EnemyTurn ()
	{
		State = GameState.EnemyTurn;

		var ai = new RandomAI ();
		var ctr = ai.ThinkOnPlayCard (Board, Enemy.Deal);
		Enemy.PlayCard (Board, ctr);

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
		if (Player.Score > Enemy.Score) {
			GD.Print ("You win!");
		}
		else if (Player.Score < Enemy.Score) {
			GD.Print ("You lose!");
		}
		else {
			GD.Print ("Draw");
		}
	}

	public bool IsOver () => Board.IsFull ();
}
