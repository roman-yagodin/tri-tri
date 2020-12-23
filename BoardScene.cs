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

		CardScenes = new CardScene [Board.Width, Board.Height];
	}

	public void AddCardScene (CardScene cardScene, int x, int y)
	{
		cardScene.Translation = new Vector3 (
			-(Const.CARD_WIDTH + Const.BOARD_GRID_SPACING) * (Board.Width - 1 - x * 2) / 2,
			(Const.CARD_HEIGHT + Const.BOARD_GRID_SPACING) * (Board.Height - 1 - y * 2) / 2,
			0
		);

		cardScene.Rotation = Vector3.Zero;
		
		AddChild (cardScene);
		CardScenes [x, y] = cardScene;
	}
}
