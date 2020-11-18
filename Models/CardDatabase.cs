using Godot;
using System;
using System.Collections.Generic;

public static class CardDatabase
{
	public static IDictionary<string, ICard> Cards;

	static CardDatabase ()
	{
		Cards = new Dictionary<string, ICard> ();

		AddCard ("blue_drake", 4, 2, 5, 3);
		AddCard ("brown_drake", 2, 5, 4, 3);
		AddCard ("gray_drake", 3, 4, 2, 5);
		AddCard ("green_drake", 5, 3, 4, 2);
		AddCard ("red_drake", 2, 4, 5, 3);

		AddCard ("lime_hatchling", 3, 2, 1, 1);
		AddCard ("violet_hatchling", 1, 2, 3, 1);
		
		AddCard ("green_dragon", 3, 7, 6, 4);
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
