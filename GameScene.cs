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
			PlayCard (RightDeal, cardIdx: 0, 0, 0);
		}
		else if (inputEvent.IsActionPressed("card2")) {
			PlayCard (RightDeal, cardIdx: 1, 1, 0);
		}
		else if (inputEvent.IsActionPressed("card3")) {
			PlayCard (RightDeal, cardIdx: 2, 2, 0);
		}
		else if (inputEvent.IsActionPressed("card4")) {
			PlayCard (RightDeal, cardIdx: 3, 0, 1);
		}
		else if (inputEvent.IsActionPressed("card5")) {
			PlayCard (RightDeal, cardIdx: 4, 1, 1);
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

	void PlayCard (DealScene dealScene, int cardIdx, int boardX, int boardY)
	{
		if (Board.Board.CanPlaceCard (boardX, boardY)) {
			var cardScene = RightDeal.CardScenes [cardIdx];
			if (cardScene != null) {
				dealScene.RemoveCardScene (cardScene);
				Board.AddCardScene (cardScene, boardX, boardY);
			}
		}
	}
}
