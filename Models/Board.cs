using System;
using Godot;

public interface IBoard
{
    int Width { get; }

    int Height { get; }

    ICard [,] Tiles { get; set; }

    bool CanPlaceCardAt (int x, int y);

    bool IsFull ();
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
}
