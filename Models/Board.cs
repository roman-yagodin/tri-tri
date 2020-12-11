public interface IBoard
{
    int Width { get; }

    int Height { get; }

    ICard [,] Field { get; set; }

    bool CanPlaceCard (int x, int y);
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

    public bool CanPlaceCard (int x, int y) => Field [x, y] == null;
}
