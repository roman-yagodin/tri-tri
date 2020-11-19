using Godot;
using System;

public class Dealer
{
	public IDeal Deal (IDeck deck, int numOfCards)
	{
		var deal = new Deal ();
		var cards = deck.Cards;
		numOfCards = Math.Min (numOfCards, cards.Count);
		var rnd = new Random ();

		for (var i = 0; i < numOfCards; i++) {
			var idx = rnd.Next (0, cards.Count);
			deal.Cards.Add (cards [idx]);
			cards.RemoveAt (idx);
		}
		
		return deal;
	}
}
