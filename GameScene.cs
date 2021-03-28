using Godot;
using System;

public class GameScene : Spatial
{
	protected Camera Camera => GetNode<Camera> (nameof (Camera));

	protected Spatial TestBoard => GetNode<Spatial> (nameof (TestBoard));

	protected BoardScene Board => GetNode<BoardScene> (nameof (Board));

	protected CardScene Card1 => TestBoard.GetNode<CardScene> (nameof (Card1));

	protected CardScene Card2 => TestBoard.GetNode<CardScene> (nameof (Card2));
	
	protected CardScene Card3 => TestBoard.GetNode<CardScene> (nameof (Card3));
	
	protected CardScene Card4 => TestBoard.GetNode<CardScene> (nameof (Card4));
	
	protected DealScene LeftDeal => GetNode<DealScene> (nameof (LeftDeal));

	protected DealScene RightDeal => GetNode<DealScene> (nameof (RightDeal));

	protected Timer EnemyTurnTimer => GetNode<Timer> (nameof (EnemyTurnTimer));

	IGame _game;
	SampleGame Game {
		get { return (SampleGame) _game; }
		set {
			_game = value;
			Bind (_game);
		}
	}

	void Bind (IGame game)
	{
		if (game == null) {
			return;
		}

		LeftDeal.Deal = game.Player1.Deal;
		RightDeal.Deal = game.Player2.Deal;
		Board.Board = game.Board;

		game.Player1.OnPlayCard += Player1_PlayCard;
		game.Player2.OnPlayCard += Player2_PlayCard;

		foreach (var card in game.Player1.Deal.Cards) {
			card.OnRotateCard += Card_RotateCard;
		}

		foreach (var card in game.Player2.Deal.Cards) {
			card.OnRotateCard += Card_RotateCard;
		}

		game.OnStateChanged += Game_StateChanged;
	}

	private bool _lockPlayerControls;

	void Game_StateChanged (object sender, EventArgs e)
	{
		GD.Print (Game.State);
		if (Game.State == GameState.WaitForPlayer || Game.State == GameState.GameOver) {
			_lockPlayerControls = false;
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Game = new SampleGame ();
		Game.Start ();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
	}

	public override void _Input(InputEvent inputEvent)
	{
		if (_lockPlayerControls) {
			return;
		}

		if (inputEvent.IsActionPressed("ui_left")) {
			TestBoard.Rotate (new Vector3 (0, 1, 0), (float) Math.PI / 16);
		}
		else if (inputEvent.IsActionPressed("ui_right")) {
			TestBoard.Rotate (new Vector3 (0, 1, 0), -(float) Math.PI / 16);
		}
		else if (inputEvent.IsActionPressed("card1")) {
			if (!Game.IsOver ()) {
				_lockPlayerControls = true;
				PlayerTurn (0);
			}
		}
		else if (inputEvent.IsActionPressed("card2")) {
			if (!Game.IsOver ()) {
				_lockPlayerControls = true;
				PlayerTurn (1);
			}
		}
		else if (inputEvent.IsActionPressed("card3")) {
			if (!Game.IsOver ()) {
				_lockPlayerControls = true;
				PlayerTurn (2);
			}
		}
		else if (inputEvent.IsActionPressed("card4")) {
			if (!Game.IsOver ()) {
				_lockPlayerControls = true;
				PlayerTurn (3);
			}
		}
		else if (inputEvent.IsActionPressed("card5")) {
			if (!Game.IsOver ()) {
				_lockPlayerControls = true;
				PlayerTurn (4);
			}
		}
		else if (inputEvent.IsActionPressed("new_game")) {
			_lockPlayerControls = true;
			Game = new SampleGame ();
			Game.Start ();
		}
	}

	void PlayerTurn (int cardIdx)
	{
		Game.PlayerTurn (cardIdx);

		if (Game.State == GameState.WaitForEnemy) {
			EnemyTurnTimer.Start ();
		}
	}

	private void _on_EnemyTurnTimer_timeout()
	{
		Game.EnemyTurn ();
	}

	void Player2_PlayCard (object sender, PlayCardEventArgs args)
	{
		var cardScene = RightDeal.CardScenes [args.PlayCardThinkResult.CardIndex];
		RightDeal.RemoveCardScene (cardScene);
		Board.AddCardScene (cardScene, args.PlayCardThinkResult.BoardCoords);
	}

	void Player1_PlayCard (object sender, PlayCardEventArgs args)
	{
		var cardScene = LeftDeal.CardScenes [args.PlayCardThinkResult.CardIndex];
		LeftDeal.RemoveCardScene (cardScene);
		Board.AddCardScene (cardScene, args.PlayCardThinkResult.BoardCoords);
	}

	void Card_RotateCard (object sender, RotateCardEventArgs args)
	{
		var card = (ICard) sender;
		var cardScene = Board.GetCardScene (card);
		cardScene.Rotate (args.RotateDirection);
	}
}
