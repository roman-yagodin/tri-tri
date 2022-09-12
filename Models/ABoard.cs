namespace TriTri;

public abstract class ABoard
{
	public int Width => Tiles.GetLength (0);

	public int Height => Tiles.GetLength (1);
	
	public ACard [,] Tiles { get; set; }

	public AGame Game { get; set; }

	private AdjacentCards GetAdjacentCards (BoardCoords boardCoords)
	{
		var adjCards = new AdjacentCards ();

		if (boardCoords.Y > 0) {
			adjCards.Top = Tiles [boardCoords.X, boardCoords.Y - 1];
		}

		if (boardCoords.Y < Height - 1) {
			adjCards.Bottom = Tiles [boardCoords.X, boardCoords.Y + 1];
		}

		if (boardCoords.X > 0) {
			adjCards.Left = Tiles [boardCoords.X - 1, boardCoords.Y];
		}

		if (boardCoords.X < Width - 1) {
			adjCards.Left = Tiles [boardCoords.X + 1, boardCoords.Y];
		}

		return adjCards;
	}

	public void PlaceCard (ACard card, BoardCoords boardCoords)
	{
		Tiles [boardCoords.X, boardCoords.Y] = card;

		var cardsToRotate = CheckAdjacentCards (card, boardCoords);
		foreach (var cardToRotate in cardsToRotate) {
			cardToRotate.Card.Rotate (cardToRotate.RotateDirection);
			
			// adjust player score
			if (card.Owner == CardOwner.Red) {
				Game.Enemy.Score++;
				Game.Player.Score--;
			}
			else if (card.Owner == CardOwner.Blue) {
				Game.Enemy.Score--;
				Game.Player.Score++;
			}
		}

		// print player score
		GD.Print ("---");
		GD.Print ("Enemy Score: " + Game.Enemy.Score);
		GD.Print ("Player Score: " + Game.Player.Score);
	}
	
	private IEnumerable<CardToRotate> CheckAdjacentCards (ACard card, BoardCoords boardCoords)
	{
		var adjCards = GetAdjacentCards (boardCoords);

		if (adjCards.Top != null && adjCards.Top.Owner != card.Owner) {
			if (adjCards.Top.Values [2] < card.Values [0]) {
				yield return new CardToRotate {
					Card = adjCards.Top,
					RotateDirection = RotateDirection.Vertical
				};
			}
		}

		if (adjCards.Right != null && adjCards.Right.Owner != card.Owner) {
			if (adjCards.Right.Values [3] < card.Values [1]) {
				yield return new CardToRotate {
					Card = adjCards.Right,
					RotateDirection = RotateDirection.Horizontal
				};
			}
		}

		if (adjCards.Bottom != null && adjCards.Bottom.Owner != card.Owner) {
			if (adjCards.Bottom.Values [0] < card.Values [2]) {
				yield return new CardToRotate {
					Card = adjCards.Bottom,
					RotateDirection = RotateDirection.VerticalBackwards
				};
			}
		}

		if (adjCards.Left != null && adjCards.Left.Owner != card.Owner) {
			if (adjCards.Left.Values [1] < card.Values [3]) {
				yield return new CardToRotate {
					Card = adjCards.Left,
					RotateDirection = RotateDirection.HorizontalBackwards
				};
			}
		}
	}

	public bool CanPlaceCardAt (BoardCoords boardCoords) => CanPlaceCardAt (boardCoords.X, boardCoords.Y);

	public bool CanPlaceCardAt (int x, int y) => Tiles [x, y] == null;

	public bool IsFull ()
	{
		for (var i = 0; i < Width; i++) {
			for (var j = 0; j < Height; j++) {
				if (CanPlaceCardAt (i, j)) {
					return false;
				}
			}
		}
		return true;
	}

	public BoardCoords TryGetRandomEmptyTile ()
	{
		if (IsFull ()) {
			return new BoardCoords { X = -1, Y = -1};
		}

		var rnd = new Random ();
		while (true) {
			var i = rnd.Next (Width);
			var j = rnd.Next (Height);
			if (Tiles [i, j] == null) {
				return new BoardCoords { X = i, Y = j};
			}
		}
	}
}

public class Board: ABoard
{
	public Board (AGame game, int width, int height)
	{
		Game = game;
		Tiles = new ACard [width, height];
	}
}
