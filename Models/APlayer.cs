public abstract class APlayer
{
	public string Name { get; set; }

	private int _score;
	public int Score {
		get => _score;
		set {
			if (_score != value) {
				_score = value;
				if (OnScoreChanged != null) {
					OnScoreChanged(this, EventArgs.Empty);
				}
			}
		}
	}

	public ADeal Deal { get; set; }

	public virtual AAI AI { get; set; }

	public event Action<object, PlayCardEventArgs> OnPlayCard;

	public event Action<object, EventArgs> OnScoreChanged;
	
	public virtual bool CanPlayCard (ABoard board, PlayCardThinkResult ctr)
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

	public virtual void PlayCard (ABoard board, PlayCardThinkResult ctr)
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

public class Player: APlayer
{
}
