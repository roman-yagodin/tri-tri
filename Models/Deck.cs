// TODO: Reduce deck to just collection of cards?

public interface IDeck
{
    IList<ACard> Cards { get; set; }
}

public class Deck: IDeck
{
    public IList<ACard> Cards { get; set; } = new List<ACard> ();
}