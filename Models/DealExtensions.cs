using System;
using System.Linq;

public static class DealExtensions
{
    public static bool IsEmpty (this IDeal deal)
    {
        return deal.Cards.All (c => c == null);
    }

    public static int TryGetRandomCardIndex (this IDeal deal)
    {
        if (deal.IsEmpty ()) {
            return -1;
        }

        var rnd = new Random ();
        while (true) {
            var cardIdx = rnd.Next (deal.Cards.Count);
            if (deal.Cards [cardIdx] != null) {
                return cardIdx;
            }
        }
    }
}