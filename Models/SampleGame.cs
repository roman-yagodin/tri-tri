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
			State = GameState.GameOverWin;
			GD.Print ("You win!");

		}
		else if (Player.Score < Enemy.Score) {
			State = GameState.GameOverLoose;
			GD.Print ("You lose!");
		}
		else {
			State = GameState.GameOverDraw;
			GD.Print ("Draw!");
		}

		GD.Print ("---");
		GD.Print ("Press N to start new game.");
	}
}
