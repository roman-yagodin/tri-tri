using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public abstract class ADeal
{
	public IList<ACardSlot> CardSlots { get; set; } = new List<ACardSlot>();

	// TODO: Must be protected?
	public IEnumerable<ACard> Cards => CardSlots.Where(s => !s.IsEmpty).Select(s => s.Card);

	public bool IsOpen { get; set; }

	public ACard SelectedCard {
		get => CardSlots.FirstOrDefault(s => !s.IsEmpty && s.Card.IsSelectedInDeal)?.Card;
	}

	public void AddCard(ACard card)
	{
		CardSlots.Add(new DealSlot(card));
	}

	public void SelectNextCard ()
	{
		var nextCard = GetNextCard(SelectedCard);
		if (nextCard != null) {
			foreach (var card in Cards) {
				card.IsSelectedInDeal = card == nextCard;
			}
		}
	}

	public void SelectPrevCard ()
	{
		var prevCard = GetPrevCard(SelectedCard);
		if (prevCard != null) {
			foreach (var card in Cards) {
				card.IsSelectedInDeal = card == prevCard;
			}
		}
	}

	public int GetSlotIndex(ACard card)
	{
		for (var i = 0; i < CardSlots.Count; i++) {
			if (!CardSlots[i].IsEmpty && CardSlots[i].Card == card) {
				return i;
			}
		}
		return -1;
	}

	ACard GetNextCard(ACard card)
	{
		var idx = GetSlotIndex(card);
		if (idx < 0) {
			return CardSlots.FirstOrDefault(s => !s.IsEmpty)?.Card;
		}
		var tail = CardSlots.Skip(idx + 1);
		return tail.FirstOrDefault(s => !s.IsEmpty)?.Card;
	}

	ACard GetPrevCard(ACard card)
	{
		var idx = GetSlotIndex(card);
		if (idx < 0) {
			return CardSlots.LastOrDefault(s => !s.IsEmpty)?.Card;
		}

		var head = CardSlots.Take(idx + 1);
		return head.LastOrDefault(s => !s.IsEmpty)?.Card;
	}

	public bool IsEmpty => CardSlots.All(s => s.IsEmpty);

    public int TryGetRandomCardIndex()
    {
        if (IsEmpty) {
            return -1;
        }

        var rnd = new Random();
        while (true) {
            var idx = rnd.Next(CardSlots.Count);
            if (!CardSlots[idx].IsEmpty) {
                return idx;
            }
        }
    }
}

public class Deal: ADeal
{

}
