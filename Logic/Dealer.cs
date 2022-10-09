using Godot;
using System;

public class Dealer
{
	public ADeal Deal (IDeck deck, int numOfCards, CardOwner cardOwner)
	{
		var deal = new Deal ();
		var cards = deck.Cards;
		numOfCards = Math.Min (numOfCards, cards.Count);
		var rnd = new Random ();

		for (var i = 0; i < numOfCards; i++) {
			var idx = rnd.Next(0, cards.Count);
			cards[idx].Owner = cardOwner;
			deal.AddCard(cards[idx]);
			cards.RemoveAt(idx);
		}
		
		return deal;
	}
}
