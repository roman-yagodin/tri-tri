namespace TriTri;

public class BoardScene : Spatial
{
	private ABoard _board;
	public ABoard Board {
		get { return _board; }
		set {
			_board = value;
			Bind (_board);
		}
	}

	public CardScene [,] CardScenes { get; set; }

	private void Bind (ABoard board)
	{
		if (board == null)
			return;

		Reset ();
	}

	private void Reset ()
	{
		if (CardScenes != null)
			for (var i = 0; i < Board.Width; i++)
				for (var j = 0; j < Board.Height; j++)
					if (CardScenes [i, j] != null)
						RemoveChild (CardScenes [i, j]);

		CardScenes = new CardScene [Board.Width, Board.Height];
	}

	public void AddCardScene (CardScene cardScene, BoardCoords boardCoords)
	{
		cardScene.Translation = new Vector3 (
			-(Const.CARD_WIDTH + Const.BOARD_GRID_SPACING) * (Board.Width - 1 - boardCoords.X * 2) / 2,
			(Const.CARD_HEIGHT + Const.BOARD_GRID_SPACING) * (Board.Height - 1 - boardCoords.Y * 2) / 2,
			0
		);

		cardScene.Rotation = Vector3.Zero;
		
		AddChild (cardScene);
		CardScenes [boardCoords.X, boardCoords.Y] = cardScene;
	}

	public CardScene GetCardScene (ACard card)
	{
		for (var i = 0; i < Board.Width; i++)
			for (var j = 0; j < Board.Height; j++)
				if (CardScenes [i, j] != null && CardScenes [i, j].Card == card)
					return CardScenes [i, j];

		return null;
	}
}
