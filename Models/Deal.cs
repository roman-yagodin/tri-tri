using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public interface IDeal
{
	IList<ICard> Cards { get; set; }

	bool IsOpen { get; set; }

	ICard SelectedCard { get; }

	void SelectNextCard ();

	void SelectPrevCard ();
}

public class Deal: IDeal
{
	public IList<ICard> Cards { get; set; } = new List<ICard> ();

	public bool IsOpen { get; set; }

	public ICard SelectedCard {
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

	private ICard GetNextCard(ICard card)
	{
		var cardIdx = Cards.IndexOf(card);
		if (cardIdx < 0) {
			return Cards.FirstOrDefault(c => c != null);
		}
		var tail = Cards.Skip(cardIdx + 1);
		return tail.FirstOrDefault(c => c != null);
	}

	private ICard GetPrevCard(ICard card)
	{
		var cardIdx = Cards.IndexOf(card);
		if (cardIdx < 0) {
			return Cards.LastOrDefault(c => c != null);
		}
		var head = Cards.Take(cardIdx);
		return head.LastOrDefault(c => c != null);
	}
}
