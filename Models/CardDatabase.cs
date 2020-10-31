using Godot;
using System;
using System.Collections.Generic;

public static class CardDatabase
{
    public static IDictionary<string, ICard> Cards;

    static CardDatabase ()
    {
        Cards = new Dictionary<string, ICard> ();

        Cards.Add ("green_drake", new Card ("green_drake", 0, 1, 2, 3));
        Cards.Add ("blue_drake", new Card ("blue_drake", 1, 2, 3, 4));
        Cards.Add ("red_drake", new Card ("red_drake", 2, 3, 4, 5));
    }

    public static ICard GetCard (string cardName)
    {
        return Cards [cardName];
    }
}
