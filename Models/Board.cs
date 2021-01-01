using System;
using System.Collections.Generic;
using Godot;

public interface IBoard
{
	int Width { get; }

	int Height { get; }

	ICard [,] Tiles { get; set; }

	void PlaceCard (ICard card, BoardCoords boardCoords);
}

public struct AdjacentCards
{
	public ICard Top;

	public ICard Right;
	
	public ICard Bottom;

	public ICard Left;
}

public class Board: IBoard
{
	public int Width => Tiles.GetLength (0);

	public int Height => Tiles.GetLength (1);
	
	public ICard [,] Tiles { get; set; }

	public Board (int width, int height)
	{
		Tiles = new ICard [width, height];
	}

	AdjacentCards GetAdjacentCards (BoardCoords boardCoords)
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

		if (boardCoords.X < Width - 1) {
			adjCards.Left = Tiles [boardCoords.X + 1, boardCoords.Y];
		}

		return adjCards;
	}

	public void PlaceCard (ICard card, BoardCoords boardCoords)
	{
		Tiles [boardCoords.X, boardCoords.Y] = card;

		var adjCards = GetAdjacentCards (boardCoords);

		if (adjCards.Top != null && adjCards.Top.Owner != card.Owner) {
			if (adjCards.Top.Values [0] < card.Values [0]) {
				adjCards.Top.Rotate (RotateDirection.Vertical);
			}
		}

		if (adjCards.Right != null && adjCards.Right.Owner != card.Owner) {
			if (adjCards.Right.Values [1] < card.Values [1]) {
				adjCards.Right.Rotate (RotateDirection.Horizontal);
			}
		}

		if (adjCards.Bottom != null && adjCards.Bottom.Owner != card.Owner) {
			if (adjCards.Bottom.Values [2] < card.Values [2]) {
				adjCards.Bottom.Rotate (RotateDirection.VerticalBackwards);
			}
		}

		if (adjCards.Left != null && adjCards.Left.Owner != card.Owner) {
			if (adjCards.Left.Values [3] < card.Values [3]) {
				adjCards.Left.Rotate (RotateDirection.HorizontalBackwards);
			}
		}
	}
}
