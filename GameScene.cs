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

	IGame _game;
	IGame Game {
		get { return _game; }
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

		game.Player2.OnPlayCard += Player2_PlayCard;
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Game = new Game ();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
	}

	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent.IsActionPressed("ui_left")) {
			TestBoard.Rotate (new Vector3 (0, 1, 0), (float) Math.PI / 16);
		}
		else if (inputEvent.IsActionPressed("ui_right")) {
			TestBoard.Rotate (new Vector3 (0, 1, 0), -(float) Math.PI / 16);
		}
		else if (inputEvent.IsActionPressed("card1")) {
			PlayCard (cardIdx: 0, Game.Board.TryGetRandomEmptyTile ());
		}
		else if (inputEvent.IsActionPressed("card2")) {
			PlayCard (cardIdx: 1, Game.Board.TryGetRandomEmptyTile ());
		}
		else if (inputEvent.IsActionPressed("card3")) {
			PlayCard (cardIdx: 2, Game.Board.TryGetRandomEmptyTile ());
		}
		else if (inputEvent.IsActionPressed("card4")) {
			PlayCard (cardIdx: 3, Game.Board.TryGetRandomEmptyTile ());
		}
		else if (inputEvent.IsActionPressed("card5")) {
			PlayCard (cardIdx: 4, Game.Board.TryGetRandomEmptyTile ());
		}
		/*
		else if (inputEvent.IsActionPressed("test_rotate1")) {
			Card1.Rotate_H ();
		}
		else if (inputEvent.IsActionPressed("test_rotate2")) {
			Card2.Rotate_V ();
		}
		else if (inputEvent.IsActionPressed("test_rotate3")) {
			Card3.Rotate_D1 ();
		}
		else if (inputEvent.IsActionPressed("test_rotate4")) {
			Card4.Rotate_D2 ();
		}*/
	}

	void PlayCard (int cardIdx, Pair boardXY)
	{
		Game.Player2.PlayCard (cardIdx, Board.Board, boardXY.X, boardXY.Y);
	}

	void Player2_PlayCard (object sender, PlayCardEventArgs args)
	{
		var cardScene = RightDeal.CardScenes [args.CardIdx];
		RightDeal.RemoveCardScene (cardScene);
		Board.AddCardScene (cardScene, args.X, args.Y);
	}
}
