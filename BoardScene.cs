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

	int BoardSizeN => Board.Field.GetLength (0);

	int BoardSizeM => Board.Field.GetLength (1);

	public CardScene [,] CardScenes { get; set; }

	void Bind (IBoard board)
	{
		if (board == null) {
			return;
		}

		CardScenes = new CardScene [BoardSizeN, BoardSizeM];
	}

	public void AddCardScene (CardScene cardScene, int x, int y)
	{
		cardScene.Translation = new Vector3 (
			-(Const.CARD_WIDTH + Const.BOARD_GRID_SPACING) * (BoardSizeN - 1 - x * 2) / 2,
			(Const.CARD_HEIGHT + Const.BOARD_GRID_SPACING) * (BoardSizeM - 1 - y * 2) / 2,
			0
		);

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
