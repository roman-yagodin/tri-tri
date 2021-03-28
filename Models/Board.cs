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

	struct CardToRotate
	{
		public ICard Card;

		public RotateDirection RotateDirection;
	}

	private IEnumerable<CardToRotate> CheckAdjacentCards (ICard card, BoardCoords boardCoords)
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
}