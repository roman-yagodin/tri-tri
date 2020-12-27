using System;
using Godot;

public interface IPlayer
{
	string Name { get; set; }

	int Score { get; set; }

	IDeal Deal { get; set; }

	IAI AI { get; set; }

	event Action<object, PlayCardEventArgs> OnPlayCard;

	void PlayCard (IBoard board, CardResult cr);
}

public abstract class PlayerBase: IPlayer
{
	public string Name { get; set; }

	public int Score { get; set; }

	public IDeal Deal { get; set; }

	public virtual IAI AI { get; set; }

	public event Action<object, PlayCardEventArgs> OnPlayCard;
	
	public virtual void PlayCard (IBoard board, CardResult cr)
	{
		var card = Deal.Cards [cr.CardIndex];
		if (card == null) {
			GD.Print ("No card to play!");
			return;
		}

		if (!board.CanPlaceCardAt (cr.BoardCoords.X, cr.BoardCoords.Y)) {
			GD.Print ("Cannot place card here!");
			return;
		}

		board.Tiles [cr.BoardCoords.X, cr.BoardCoords.Y] = card;
		Deal.Cards [cr.CardIndex] = null;

		if (OnPlayCard != null) {
			OnPlayCard (this, new PlayCardEventArgs {
				CardIdx = cr.CardIndex,
				BoardCoords = cr.BoardCoords
			});
		}
	}
}

public class RandomPlayer: PlayerBase
{
	public override IAI AI { get; set; } = new RandomAI ();
}

public class HumanPlayer: PlayerBase
{

}
