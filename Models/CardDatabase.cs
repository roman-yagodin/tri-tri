using Godot;
using System;
using System.Collections.Generic;

public static class CardDatabase
{
	public static IDictionary<string, ICard> Cards;

	static CardDatabase ()
	{
		Cards = new Dictionary<string, ICard> ();

		AddCard ("green_drake", 0, 1, 2, 3);
		AddCard ("blue_drake", 1, 2, 3, 4);
		AddCard ("red_drake", 2, 3, 4, 5);
		AddCard ("gray_drake", 3, 4, 5, 6);
		AddCard ("brown_drake", 4, 5, 6, 7);
	}

	static void AddCard (string cardName, params int [] values)
	{
		Cards.Add (cardName, new Card (cardName, values));
	}

	public static ICard GetCard (string cardName)
	{
		if (cardName != null && Cards.TryGetValue (cardName, out ICard card)) {
			return ((Card) card).Clone ();
		}
		return null;
	}
}
