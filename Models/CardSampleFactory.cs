namespace TriTri;

/// <summary>
/// Contains pre-configured cards to use in editor
/// </summary>
public class CardSampleFactory
{
	public IDictionary<string, ACard> Cards;

	public CardSampleFactory ()
	{
		Cards = new Dictionary<string, ACard> ();
		
		var cardFactory = new CardFactory ();

		var card1 = cardFactory.CreateCard ("green_drake");
		Cards.Add ("green_drake_01", card1);

		var card2 = cardFactory.CreateCard ("blue_drake");
		card2.Owner = CardOwner.Blue;
		Cards.Add ("blue_drake_01", card2);

		var card3 = cardFactory.CreateCard ("red_drake");
		card3.Owner = CardOwner.Red;
		Cards.Add ("red_drake_01", card3);
		
		var card4 = cardFactory.CreateCard ("gray_drake");
		card4.Owner = CardOwner.Neutral;
		Cards.Add ("gray_drake_01", card4);
		
		var card5 = cardFactory.CreateCard ("brown_drake");
		card5.Owner = CardOwner.Red;
		Cards.Add ("brown_drake_01", card5);
		
		var card6 = cardFactory.CreateCard ("lime_hatchling");
		card6.Owner = CardOwner.Neutral;
		Cards.Add ("lime_hatchling_01", card6);
		
		var card7 = cardFactory.CreateCard ("violet_hatchling");
		card7.Owner = CardOwner.Neutral;
		Cards.Add ("violet_hatchling_01", card7);
		
		var card8 = cardFactory.CreateCard ("green_dragon");
		card8.Owner = CardOwner.Neutral;
		Cards.Add ("green_dragon_01", card8);
		
		var blank = new Card { IsBlank = true };
		Cards.Add ("blank", blank);
	}

	public ACard CreateCard (string cardSampleName)
	{
		if (cardSampleName != null && Cards.TryGetValue (cardSampleName, out ACard card)) {
			return ((Card) card).Clone ();
		}
		return null;
	}
}
