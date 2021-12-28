using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public abstract class ADeal
{
	public IList<ACard> Cards { get; set; } = new List<ACard> ();

	public bool IsOpen { get; set; }

	public ACard SelectedCard {
		get => Cards.FirstOrDefault(c => c != null && c.IsSelectedInDeal);
	}

	public void SelectNextCard ()
	{
		var nextCard = GetNextCard(SelectedCard);
		if (nextCard != null) {
			foreach (var card in Cards) {
				if (card != null) {
					card.IsSelectedInDeal = card == nextCard;
				}
			}
		}
	}

	public void SelectPrevCard ()
	{
		var prevCard = GetPrevCard(SelectedCard);
		if (prevCard != null) {
			foreach (var card in Cards) {
				if (card != null) {
					card.IsSelectedInDeal = card == prevCard;
				}
			}
		}
	}

	private ACard GetNextCard(ACard card)
	{
		var cardIdx = Cards.IndexOf(card);
		if (cardIdx < 0) {
			return Cards.FirstOrDefault(c => c != null);
		}
		var tail = Cards.Skip(cardIdx + 1);
		return tail.FirstOrDefault(c => c != null);
	}

	private ACard GetPrevCard(ACard card)
	{
		var cardIdx = Cards.IndexOf(card);
		if (cardIdx < 0) {
			return Cards.LastOrDefault(c => c != null);
		}
		var head = Cards.Take(cardIdx);
		return head.LastOrDefault(c => c != null);
	}

	public bool IsEmpty ()
    {
        return Cards.All (c => c == null);
    }

    public int TryGetRandomCardIndex ()
    {
        if (IsEmpty ()) {
            return -1;
        }

        var rnd = new Random ();
        while (true) {
            var cardIdx = rnd.Next (Cards.Count);
            if (Cards [cardIdx] != null) {
                return cardIdx;
            }
        }
    }
}

public class Deal: ADeal
{

}
