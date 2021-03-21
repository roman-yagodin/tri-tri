using System;
using System.Collections.Generic;
using Godot;

public interface IBoard
{
	int Width { get; }

	int Height { get; }

	ICard [,] Tiles { get; set; }

	IGame Game { get; set; }

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

	public IGame Game { get; set; }

	public Board (IGame game, int width, int height)
	{
		Game = game;
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

		CheckAdjacentCards (card, boardCoords);
	}

	private void CheckAdjacentCards (ICard card, BoardCoords boardCoords)
	{
		var adjCards = GetAdjacentCards (boardCoords);

		if (adjCards.Top != null && adjCards.Top.Owner != card.Owner) {
			if (adjCards.Top.Values [2] < card.Values [0]) {
				RotateCard (adjCards.Top, RotateDirection.Vertical);
			}
		}

		if (adjCards.Right != null && adjCards.Right.Owner != card.Owner) {
			if (adjCards.Right.Values [3] < card.Values [1]) {
				RotateCard (adjCards.Right, RotateDirection.Horizontal);
			}
		}

		if (adjCards.Bottom != null && adjCards.Bottom.Owner != card.Owner) {
			if (adjCards.Bottom.Values [0] < card.Values [2]) {
				RotateCard (adjCards.Bottom, RotateDirection.VerticalBackwards);
			}
		}

		if (adjCards.Left != null && adjCards.Left.Owner != card.Owner) {
			if (adjCards.Left.Values [1] < card.Values [3]) {
				RotateCard (adjCards.Left, RotateDirection.HorizontalBackwards);
			}
		}
	}

	private void RotateCard (ICard card, RotateDirection rotateDirection)
	{
		card.Rotate (rotateDirection);

		if (card.Owner == CardOwner.Red) {
			Game.Player1.Score++;
			Game.Player2.Score--;
		}
		else if (card.Owner == CardOwner.Blue) {
			Game.Player1.Score--;
			Game.Player2.Score++;
		}

		GD.Print ("Player1 Score: " + Game.Player1.Score);
		GD.Print ("Player2 Score: " + Game.Player2.Score);
	}
}