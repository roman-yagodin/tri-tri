using Godot;
using System;

public interface ICard
{
	string Name { get; set; }

	string TextureName { get; set; }

	int [] Values { get; set; }

	float Rarity { get; set; }

	CardOwner Owner { get; set; }

	bool IsBlank { get; set; }

	bool IsSelectedInDeal { get; set; }

	string GetTextureFilename ();

	ICard Clone ();
	
	void ToggleOwner ();

	void Rotate (RotateDirection rotateDirection);

	event Action<object, RotateCardEventArgs> OnRotateCard;

	event Action<object, EventArgs> OnIsSelectInDealChanged;
}

public class Card : ICard
{
	public string Name { get; set; }
	
	public string TextureName { get; set; }
	
	public int[] Values { get; set; }

	public float Rarity { get; set; }

	public CardOwner Owner { get; set; } = CardOwner.Neutral;

	public bool IsBlank { get; set; }

	private bool _isSelectedInDeal;
	public bool IsSelectedInDeal {
		get => _isSelectedInDeal;
		set {
			if (_isSelectedInDeal != value) {
				_isSelectedInDeal = value;
				if (OnIsSelectInDealChanged != null) {
					OnIsSelectInDealChanged(this, EventArgs.Empty);
				}
			}
		}
	}

	public string GetTextureFilename () => $"res://textures/cards/{TextureName}.png";

	public event Action<object, RotateCardEventArgs> OnRotateCard;

	public event Action<object, EventArgs> OnIsSelectInDealChanged;

	public Card ()
	{
	}

	public Card (string name, params int [] values)
	{
		Name = name;
		TextureName = name;
		Values = values;
		Rarity = 1f;
	}

	public ICard Clone ()
	{
		return (ICard) this.MemberwiseClone ();
	}
	
	public void ToggleOwner ()
	{
		if (Owner == CardOwner.Red) {
			Owner = CardOwner.Blue;
		}
		else if (Owner == CardOwner.Blue) {
			Owner = CardOwner.Red;
		}
	}

	public void Rotate (RotateDirection rotateDirection)
	{
		ToggleOwner ();

		if (OnRotateCard != null) {
			OnRotateCard (this, new RotateCardEventArgs {
				RotateDirection = rotateDirection
			});
		}
	}
}
