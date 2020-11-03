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

	protected ICard Card;

	string _cardName;

	[Export]
	public string CardName {
		get { return Card?.Name ?? _cardName; }
		set {
			_cardName = value;
			LoadCard ();
		}
	}

	CardOwner _cardOwner;

	[Export]
	public CardOwner CardOwner {
		get { return Card?.Owner ?? _cardOwner; }
		set {
			_cardOwner = value;
			LoadCard ();
		}
	}

	void LoadCard ()
	{
		Card = CardDatabase.GetCard (_cardName);
		Card.Owner = _cardOwner;

		DigitPlate1.Visible = !Card.IsBlank;
		DigitPlate2.Visible = !Card.IsBlank;
		DigitPlate3.Visible = !Card.IsBlank;
		DigitPlate4.Visible = !Card.IsBlank;
		TexturedQuadMesh.Visible = !Card.IsBlank;

		if (!Card.IsBlank) {
			DigitPlate1.Digit = Card.Values [0];
			DigitPlate2.Digit = Card.Values [1];
			DigitPlate3.Digit = Card.Values [2];
			DigitPlate4.Digit = Card.Values [3];
			TexturedQuadMesh.Texture = GD.Load<StreamTexture> (Card.GetTextureFilename ());
		}

		if (Card.Owner != CardOwner.Neutral) {
			var material = new SpatialMaterial ();
			if (Card.Owner == CardOwner.Red) {
				material.AlbedoColor = new Color (1f, .75f, .75f);
			}
			else if (Card.Owner == CardOwner.Blue) {
				material.AlbedoColor = new Color (.75f, .75f, 1f);
			}
			Front.SetSurfaceMaterial (0, material);
		}
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LoadCard ();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
	
	}
}
