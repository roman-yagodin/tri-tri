global using Godot;
global using System;
global using System.Linq;
global using System.Collections.Generic;

namespace TriTri;

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

	protected DigitPlate Score1 => GetNode<DigitPlate> (nameof (Score1));

	protected DigitPlate Score2 => GetNode<DigitPlate> (nameof (Score2));

	protected Timer EnemyTurnTimer => GetNode<Timer> (nameof (EnemyTurnTimer));

	protected TurnIndicator TurnIndicator => GetNode<TurnIndicator> (nameof(TurnIndicator)); 

	protected Sprite3D DrawMessage => GetNode<Sprite3D>(nameof(DrawMessage));

	protected Sprite3D YouWinMessage => GetNode<Sprite3D>(nameof(YouWinMessage));

	protected Sprite3D YouLooseMessage => GetNode<Sprite3D>(nameof(YouLooseMessage));

	AGame _game;
	AGame Game {
		get { return _game; }
		set {
			_game = value;
			Bind (_game);
		}
	}

	void Bind (AGame game)
	{
		if (game == null)
			return;

		LeftDeal.Deal = game.Enemy.Deal;
		RightDeal.Deal = game.Player.Deal;
		Board.Board = game.Board;

		game.Enemy.OnPlayCard += Enemy_PlayCard;
		game.Player.OnPlayCard += Player_PlayCard;

		foreach (var card in game.Enemy.Deal.Cards) {
			card.OnRotateCard += Card_RotateCard;
		}

		foreach (var card in game.Player.Deal.Cards) {
			card.OnRotateCard += Card_RotateCard;
		}

		game.OnStateChanged += Game_StateChanged;

		Score1.Digit = game.Enemy.Score;
		Score2.Digit = game.Player.Score;

		game.Enemy.OnScoreChanged += OnPlayer1ScoreChanged;
		game.Player.OnScoreChanged += OnPlayer2ScoreChanged;

		game.OnStateChanged += TurnIndicator.Game_StateChanged;
	}

	void OnPlayer1ScoreChanged (object sender, EventArgs e)
	{
		var player = (Player)sender;
		Score1.Digit = player.Score;
	}

	void OnPlayer2ScoreChanged (object sender, EventArgs e)
	{
		var player = (Player)sender;
		Score2.Digit = player.Score;
	}

	private bool _lockPlayerControls;

	void Game_StateChanged (object sender, GameStateChangedEventArgs e)
	{
		GD.Print ("> " + e.State);
		if (e.State == GameState.WaitForPlayer || e.State.IsGameOver()) {
			_lockPlayerControls = false;
		}

		DrawMessage.Visible = e.State == GameState.GameOverDraw;
		YouWinMessage.Visible = e.State == GameState.GameOverWin;
		YouLooseMessage.Visible = e.State == GameState.GameOverLoose;
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
		if (_lockPlayerControls)
			return;

		if (inputEvent.IsActionPressed("ui_left")) {
			Board.Rotate (new Vector3 (0, 1, 0), (float) Math.PI / 16);
		}
		else if (inputEvent.IsActionPressed("ui_right")) {
			Board.Rotate (new Vector3 (0, 1, 0), -(float) Math.PI / 16);
		}
		else if (inputEvent.IsActionPressed("ui_down")) {
			if (Game.State == GameState.WaitForPlayer) {
				Game.Player.Deal.SelectNextCard();
			}
		}
		else if (inputEvent.IsActionPressed("ui_up")) {
			if (Game.State == GameState.WaitForPlayer) {
				Game.Player.Deal.SelectPrevCard();
			}
		}
		else if (inputEvent.IsActionPressed("ui_accept")) {
			if (Game.State == GameState.WaitForPlayer) {
				if (!Game.IsOver ()) {
					var selectedCard = Game.Player.Deal.SelectedCard;
					if (selectedCard != null) {
						var selectedCardIdx = Game.Player.Deal.Cards.IndexOf(selectedCard);
						_lockPlayerControls = true;
						PlayerTurn (selectedCardIdx);
					}
				}
			}
		}
		/*
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
		}*/
		else if (inputEvent.IsActionPressed("new_game")) {
			_lockPlayerControls = true;
			Game = new SampleGame ();
			Game.Start ();
		}
	}

	private void PlayerTurn (int cardIdx)
	{
		Game.PlayerTurn (cardIdx);

		if (Game.State == GameState.WaitForEnemy) {
			EnemyTurnTimer.Start ();
		}
	}

	private void _on_EnemyTurnTimer_timeout() => Game.EnemyTurn ();

	void Player_PlayCard (object sender, PlayCardEventArgs args)
	{
		var cardScene = RightDeal.CardScenes [args.PlayCardThinkResult.CardIndex];
		RightDeal.RemoveCardScene (cardScene);
		Board.AddCardScene (cardScene, args.PlayCardThinkResult.BoardCoords);
	}

	void Enemy_PlayCard (object sender, PlayCardEventArgs args)
	{
		var cardScene = LeftDeal.CardScenes [args.PlayCardThinkResult.CardIndex];
		LeftDeal.RemoveCardScene (cardScene);
		Board.AddCardScene (cardScene, args.PlayCardThinkResult.BoardCoords);
	}

	void Card_RotateCard (object sender, RotateCardEventArgs args)
	{
		var card = (ACard) sender;
		var cardScene = Board.GetCardScene (card);
		cardScene.Rotate (args.RotateDirection);
	}
}
