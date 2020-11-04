using Godot;
using System;

public class CardScene : Spatial
{
	protected MeshInstance Front => GetNode<MeshInstance> (nameof (Front));

	protected DigitPlate DigitPlate1 => GetNode<DigitPlate> (nameof (DigitPlate1));

	protected DigitPlate DigitPlate2 => GetNode<DigitPlate> (nameof (DigitPlate2));
	
	protected DigitPlate DigitPlate3 => GetNode<DigitPlate> (nameof (DigitPlate3));
	
	protected DigitPlate DigitPlate4 => GetNode<DigitPlate> (nameof (DigitPlate4));

	protected TexturedQuadMesh TexturedQuadMesh => GetNode<TexturedQuadMesh> (nameof (TexturedQuadMesh));

	ICard _card;

	public ICard Card {
		get { return _card; }
		set {
			_card = value;
			BindCard (_card);
		}
	}

	string _cardSampleName;

	[Export]
	public string CardSampleName {
		get { return _cardSampleName; }
		set {
			_cardSampleName = value;
			_card = CardSamples.GetCard (_cardSampleName);
			BindCard (_card);
		}
	}

	void BindCard (ICard card)
	{
		DigitPlate1.Visible = !card.IsBlank;
		DigitPlate2.Visible = !card.IsBlank;
		DigitPlate3.Visible = !card.IsBlank;
		DigitPlate4.Visible = !card.IsBlank;
		TexturedQuadMesh.Visible = !card.IsBlank;

		if (!card.IsBlank) {
			DigitPlate1.Digit = card.Values [0];
			DigitPlate2.Digit = card.Values [1];
			DigitPlate3.Digit = card.Values [2];
			DigitPlate4.Digit = card.Values [3];
			TexturedQuadMesh.Texture = GD.Load<StreamTexture> (card.GetTextureFilename ());
		}

		if (card.Owner != CardOwner.Neutral) {
			var material = new SpatialMaterial ();
			if (card.Owner == CardOwner.Red) {
				material.AlbedoColor = new Color (1f, .75f, .75f);
			}
			else if (card.Owner == CardOwner.Blue) {
				material.AlbedoColor = new Color (.75f, .75f, 1f);
			}
			Front.SetSurfaceMaterial (0, material);
		}
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_card = CardSamples.GetCard (_cardSampleName);
		BindCard (_card);
	}
}
