using System;
using Godot;

public interface IBoard
{
    int Width { get; }

    int Height { get; }

    // TODO: Rename to Tiles
    ICard [,] Field { get; set; }

    // TODO: Rename to CanPlaceCardAt
    bool CanPlaceCard (int x, int y);

    bool IsFull ();
}

public class Board: IBoard
{
    public int Width => Field.GetLength (0);

    public int Height => Field.GetLength (1);
    
    public ICard [,] Field { get; set; }

    public Board (int width, int height)
    {
        Field = new ICard [width, height];
    }

    public bool CanPlaceCard (int x, int y) => Field [x, y] == null;
    
    public bool IsFull ()
    {
        for (var i = 0; i < Width; i++) {
            for (var j = 0; j < Height; j++) {
                if (CanPlaceCard (i, j)) {
                    return false;
                }
            }
        }

        return true;
    }
}
