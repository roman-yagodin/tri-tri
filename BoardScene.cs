using Godot;
using System;

public class BoardScene : Spatial
{
	IBoard _board;
	public IBoard Board {
		get { return _board; }
		set {
			_board = value;
			Bind (_board);
		}
	}

	public CardScene [,] CardScenes { get; set; }

	void Bind (IBoard board)
	{
		if (board == null) {
			return;
		}

		CardScenes = new CardScene [board.Field.GetLength (0), board.Field.GetLength (1)];
	}

	public void AddCardScene (CardScene cardScene, int x, int y)
	{
		// TODO: Calculate card translation from card size, board size and grid spacing
		var cw = 3;
		var ch = 4;
		cardScene.Translation = new Vector3 (x * (cw + 1), -y * (ch + 1), 0);
		cardScene.Rotation = Vector3.Zero;
		
		AddChild (cardScene);
		CardScenes [x, y] = cardScene;
		Board.Field [x, y] = cardScene.Card;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Bind (_board);
	}
}
