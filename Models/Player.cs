using System;
using Godot;

public interface IPlayer
{
	string Name { get; set; }

	int Score { get; set; }

	IDeal Deal { get; set; }

	event Action<object, PlayCardEventArgs> OnPlayCard;

	void PlayCard (int cardIdx, IBoard board, int x, int y);
}

public class PlayCardEventArgs
{
	public int CardIdx { get; set; }

	public int X { get; set; }

	public int Y { get; set; }
}

public class Player: IPlayer
{
	public string Name { get; set; }

	public int Score { get; set; }

	public IDeal Deal { get; set; }

	public event Action<object, PlayCardEventArgs> OnPlayCard;
	
	public void PlayCard (int cardIdx, IBoard board, int x, int y)
	{
		var card = Deal.Cards [cardIdx];
		if (card == null) {
			GD.Print ("No card to play!");
			return;
		}

		if (!board.CanPlaceCardAt (x, y)) {
			GD.Print ("Cannot place card here!");
			return;
		}

		board.Tiles [x, y] = card;
		Deal.Cards [cardIdx] = null;

		if (OnPlayCard != null) {
			OnPlayCard (this, new PlayCardEventArgs {
				CardIdx = cardIdx,
				X = x,
				Y = y
			});
		}
	}
}
