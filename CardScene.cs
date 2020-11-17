using Godot;
using System;

public class CardScene : Spatial
{
	protected MeshInstance Front => GetNode<MeshInstance> (nameof (Front));

	protected DigitPlate DigitPlate1 => GetNode<DigitPlate> (nameof (DigitPlate1));

	protected DigitPlate DigitPlate2 => GetNode<DigitPlate> (nameof (DigitPlate2));
	
	protected DigitPlate DigitPlate3 => GetNode<DigitPlate> (nameof (DigitPlate3));
	
	protected DigitPlate DigitPlate4 => GetNode<DigitPlate> (nameof (DigitPlate4));

	protected Sprite3D Sprite3D => GetNode<Sprite3D> (nameof (Sprite3D));

	protected AnimationPlayer AnimationPlayer => GetNode<AnimationPlayer> (nameof (AnimationPlayer));

	protected Timer Timer => GetNode<Timer> (nameof (Timer));

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

	void BindCard ()
	{
		BindCard (_card);
	}

	void BindCard (ICard card)
	{
		DigitPlate1.Visible = !card.IsBlank;
		DigitPlate2.Visible = !card.IsBlank;
		DigitPlate3.Visible = !card.IsBlank;
		DigitPlate4.Visible = !card.IsBlank;
		Sprite3D.Visible = !card.IsBlank;

		if (!card.IsBlank) {
			DigitPlate1.Digit = card.Values [0];
			DigitPlate2.Digit = card.Values [1];
			DigitPlate3.Digit = card.Values [2];
			DigitPlate4.Digit = card.Values [3];
			Sprite3D.Texture = GD.Load<StreamTexture> (card.GetTextureFilename ());
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
		InitAnimation ();
	}
	
	void InitAnimation ()
	{
		var axis1 = new Vector3 (3f, 4f, 0).Normalized ();
		AnimationPlayer.AddAnimation ("CardRotate_D1_Cw", CreateQuatAnimation (axis1));
		
		var axis2 = new Vector3 (-3f, 4f, 0).Normalized ();
		AnimationPlayer.AddAnimation ("CardRotate_D2_Cw", CreateQuatAnimation (axis2));	
	}
	
	Animation CreateQuatAnimation (Vector3 axis)
	{
		var anim = new Animation ();
		var trackIdx = anim.AddTrack (Animation.TrackType.Transform);
		anim.TrackSetPath (trackIdx, ".");
		anim.TransformTrackInsertKey (trackIdx, 0f, Vector3.Zero, new Quat (0f, 0f, 0f, 1f).Normalized (), Vector3.One);
		anim.TransformTrackInsertKey (trackIdx, 0.5f, Vector3.Zero, new Quat (axis, Mathf.Pi).Normalized (), Vector3.One);
		anim.TransformTrackInsertKey (trackIdx, 1f, Vector3.Zero, new Quat (axis, 2 * Mathf.Pi).Normalized (), Vector3.One);
		return anim;	
	}

	public void Rotate_H ()
	{
		AnimationPlayer.Play ("CardRotate_H_Cw");
		Timer.Start ();
	}
	
	public void Rotate_V ()
	{
		AnimationPlayer.Play ("CardRotate_V_Cw");
		Timer.Start ();
	}
	
	public void Rotate_D1 ()
	{
		AnimationPlayer.Play ("CardRotate_D1_Cw");
		Timer.Start ();
	}
	
	public void Rotate_D2 ()
	{
		AnimationPlayer.Play ("CardRotate_D2_Cw");
		Timer.Start ();
	}
	
	private void _on_Timer_timeout()
	{
		Card.ToggleOwner ();
		BindCard ();
	}
}
