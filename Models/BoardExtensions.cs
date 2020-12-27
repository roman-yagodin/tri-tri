using System;
using Godot;
using System.Text;

public static class BoardExtensions
{
	public static bool CanPlaceCardAt (this IBoard board, BoardCoords boardCoords) => board.CanPlaceCardAt (boardCoords.X, boardCoords.Y);

	public static bool CanPlaceCardAt (this IBoard board, int x, int y) => board.Tiles [x, y] == null;

	public static bool IsFull (this IBoard board)
	{
		for (var i = 0; i < board.Width; i++) {
			for (var j = 0; j < board.Height; j++) {
				if (board.CanPlaceCardAt (i, j)) {
					return false;
				}
			}
		}
		return true;
	}

	public static BoardCoords TryGetRandomEmptyTile (this IBoard board)
	{
		if (board.IsFull ()) {
			return new BoardCoords { X = -1, Y = -1};
		}

		var rnd = new Random ();
		while (true) {
			var i = rnd.Next (board.Width);
			var j = rnd.Next (board.Height);
			if (board.Tiles [i, j] == null) {
				return new BoardCoords { X = i, Y = j};
			}
		}
	}
}
