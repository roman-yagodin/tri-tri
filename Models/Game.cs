using System;
using Godot;

public abstract class AGame
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

	public abstract void Start();

	public abstract void PlayerTurn (int cardIdx);

	public abstract void EnemyTurn ();

	protected virtual void GameOverCheck ()
	{
		if (IsOver ()) {
			State = GameState.GameOver;

			AnalyzeResults ();
		}
	}

	protected abstract void AnalyzeResults();

	public virtual bool IsOver () => Board.IsFull ();
}

public class SampleGame: AGame
{
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

	public override void Start ()
	{
		State = GameState.WaitForPlayer;

		GD.Print ("---");
		GD.Print ("Game started!");	
	}

	public override void PlayerTurn (int cardIdx)
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

		GameOverCheck ();
	}

	public override void EnemyTurn ()
	{
		State = GameState.EnemyTurn;

		var ai = new RandomAI ();
		var ctr = ai.ThinkOnPlayCard (Board, Enemy.Deal);
		Enemy.PlayCard (Board, ctr);

		State = GameState.WaitForPlayer;
		
		GameOverCheck ();
	}

	protected override void AnalyzeResults ()
	{
		GD.Print ("---");
		if (Player.Score > Enemy.Score) {
			GD.Print ("You win!");
		}
		else if (Player.Score < Enemy.Score) {
			GD.Print ("You lose!");
		}
		else {
			GD.Print ("Draw!");
		}

		GD.Print ("---");
		GD.Print ("Press N to start new game.");
	}
}
