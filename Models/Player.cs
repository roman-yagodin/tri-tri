public interface IPlayer
{
    string Name { get; set; }

    IDeal Deal { get; set; }

    void PlayCard (int cardIdx, IBoard board, int x, int y);
}

public class Player: IPlayer
{
    public string Name { get; set; }

    public IDeal Deal { get; set; }

    public void PlayCard (int cardIdx, IBoard board, int x, int y)
    {
        var card = Deal.Cards [cardIdx];
        if (card == null) {
            return;
        }

        if (!board.CanPlaceCard (x, y)) {
            return;
        }

        board.Field [x, y] = card;
        Deal.Cards [cardIdx] = null;
    }
}