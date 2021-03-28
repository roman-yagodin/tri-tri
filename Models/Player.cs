using System;
using Godot;

public interface IPlayer
{
	string Name { get; set; }

	int Score { get; set; }

	IDeal Deal { get; set; }

	IAI AI { get; set; }

	event Action<object, PlayCardEventArgs> OnPlayCard;

	bool CanPlayCard (IBoard board, PlayCardThinkResult ctr);

	void PlayCard (IBoard board, PlayCardThinkResult ctr);
}

public class Player: IPlayer
{
	public string Name { get; set; }

	public int Score { get; set; }

	public IDeal Deal { get; set; }

	public virtual IAI AI { get; set; }

	public event Action<object, PlayCardEventArgs> OnPlayCard;
	
	public virtual bool CanPlayCard (IBoard board, PlayCardThinkResult ctr)
	{
		var card = Deal.Cards [ctr.CardIndex];
		if (card == null) {
			GD.Print ("No card to play!");
			return false;
		}

		if (!board.CanPlaceCardAt (ctr.BoardCoords.X, ctr.BoardCoords.Y)) {
			GD.Print ("Cannot place card here!");
			return false;
		}

		return true;
	}

	public virtual void PlayCard (IBoard board, PlayCardThinkResult ctr)
	{
		var card = Deal.Cards [ctr.CardIndex];
		board.PlaceCard (card, ctr.BoardCoords);
		Deal.Cards [ctr.CardIndex] = null;

		if (OnPlayCard != null) {
			OnPlayCard (this, new PlayCardEventArgs {
				PlayCardThinkResult = new PlayCardThinkResult {
					CardIndex = ctr.CardIndex,
					BoardCoords = ctr.BoardCoords
				}
			});
		}
	}
}
