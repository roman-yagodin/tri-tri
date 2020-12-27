using System;
using Godot;
using System.Text;

public static class BoardExtensions
{
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
