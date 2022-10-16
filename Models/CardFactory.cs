using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public class CardFactory
{
	public IDictionary<string, ACard> Cards;

	public CardFactory ()
	{
		Cards = new Dictionary<string, ACard> ();

		AddTemplate ("blue_drake", 4, 2, 5, 3);
		AddTemplate ("brown_drake", 2, 5, 4, 3);
		AddTemplate ("gray_drake", 3, 4, 2, 5);
		AddTemplate ("green_drake", 5, 3, 4, 2);
		AddTemplate ("red_drake", 2, 4, 5, 3);
		AddTemplate ("lime_hatchling", 3, 2, 1, 1);
		AddTemplate ("violet_hatchling", 1, 2, 3, 1);
		AddTemplate ("green_dragon", 3, 7, 6, 4);
	}

	void AddTemplate (string cardName, params int [] values)
	{
		Cards.Add (cardName, new Card (cardName, values));
	}

	public ACard? CreateCard (string? cardName)
	{
		if (cardName != null && Cards.TryGetValue (cardName, out ACard card)) {
			return ((Card) card).Clone ();
		}
		return null;
	}

	public IDeck CreateFullUniqueDeck ()
	{
		return new Deck {
			Cards = Cards.Values.Select (c => c.Clone ()).ToList ()
		};
	}
}
