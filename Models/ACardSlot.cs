public abstract class ACardSlot
{
    public ACard? Card { get; set; }

    public bool IsEmpty => Card == null;
}

public class DealSlot: ACardSlot
{
    public DealSlot(ACard card)
    {
        Card = card;
    }
}

public class BoardSlot: ACardSlot
{}