using System;
using Godot;
using System.Text;

public static class BoardExtensions
{
	public static Pair TryGetRandomEmptyTile (this IBoard board)
	{
		if (board.IsFull ()) {
			return new Pair { X = -1, Y = -1};
		}

		var rnd = new Random ();
		while (true) {
			var i = rnd.Next (board.Width);
			var j = rnd.Next (board.Height);
			if (board.Tiles [i, j] == null) {
				return new Pair { X = i, Y = j};
			}
		}
	}
}
