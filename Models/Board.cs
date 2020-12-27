using System;
using Godot;

public interface IBoard
{
    int Width { get; }

    int Height { get; }

    ICard [,] Tiles { get; set; }
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
}
