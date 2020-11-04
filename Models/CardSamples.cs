using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Contains pre-configured cards to use in editor
/// </summary>
public static class CardSamples
{
    public static IDictionary<string, ICard> Cards;

    static CardSamples ()
    {
        Cards = new Dictionary<string, ICard> ();
        
        var card1 = CardDatabase.GetCard ("green_drake");
        Cards.Add ("green_drake_01", card1);

        var card2 = CardDatabase.GetCard ("blue_drake");
        card2.Owner = CardOwner.Blue;
        Cards.Add ("blue_drake_01", card2);

        var card3 = CardDatabase.GetCard ("red_drake");
        card3.Owner = CardOwner.Red;
        Cards.Add ("red_drake_01", card3);

        var card4 = new Card { IsBlank = true };
        Cards.Add ("blank", card4);
    }

    public static ICard GetCard (string cardSampleName)
    {
        if (cardSampleName != null && Cards.TryGetValue (cardSampleName, out ICard card)) {
            return ((Card) card).Clone ();
        }
        return null;
    }
}
