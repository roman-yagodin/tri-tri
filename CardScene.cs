using Godot;
using System;
using System.Linq;

public class CardScene : Spatial
{
	protected MeshInstance Front => GetNode<MeshInstance> (nameof (Front));

	protected Spatial Digits => GetNode<Spatial> (nameof (Digits));

	protected DigitPlate DigitPlate1 => Digits.GetNode<DigitPlate> (nameof (DigitPlate1));

	protected DigitPlate DigitPlate2 => Digits.GetNode<DigitPlate> (nameof (DigitPlate2));
	
	protected DigitPlate DigitPlate3 => Digits.GetNode<DigitPlate> (nameof (DigitPlate3));
	
	protected DigitPlate DigitPlate4 => Digits.GetNode<DigitPlate> (nameof (DigitPlate4));

	protected Sprite3D Sprite3D => GetNode<Sprite3D> (nameof (Sprite3D));

	protected AnimationPlayer AnimationPlayer => GetNode<AnimationPlayer> (nameof (AnimationPlayer));

	protected Timer Timer => GetNode<Timer> (nameof (Timer));

	CardSampleFactory CardSampleFactory = new CardSampleFactory ();

	ACard _card;

	public ACard Card {
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
			_card = CardSampleFactory.CreateCard (_cardSampleName);
			BindCard (_card);
		}
	}

	void BindCard (ACard card)
	{
		if (card == null) {
			return;
		}

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
				material.AlbedoColor = new Color (1f, .33f, .33f);
			}
			else if (card.Owner == CardOwner.Blue) {
				material.AlbedoColor = new Color (.33f, .33f, 1f);
			}
			Front.SetSurfaceMaterial (0, material);
		}
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		UpdateDiagonalRotationAnimation ("CardRotate_D1");
		UpdateDiagonalRotationAnimation ("CardRotate_D2");
	}
	
	Vector3 GetDiagonalRotationAxis (string animName)
	{
		if (animName == "CardRotate_D1") {
			return new Vector3 (3f, 4f, 0).Normalized ();
		}
		else if (animName == "CardRotate_D2") {
			return new Vector3 (-3f, 4f, 0).Normalized ();
		}
		throw new ArgumentException ("Unsupported animation name!");
	}

	void UpdateDiagonalRotationAnimation (string name)
	{
		if (AnimationPlayer.GetAnimationList ().FirstOrDefault (n => n == name) != null) {
			AnimationPlayer.RemoveAnimation (name);
		}
		AnimationPlayer.AddAnimation (name, CreateDiagonalRotationAnimation (Translation, GetDiagonalRotationAxis (name)));
	}

	Animation CreateDiagonalRotationAnimation (Vector3 translation, Vector3 axis)
	{
		var anim = new Animation ();
		var trackIdx = anim.AddTrack (Animation.TrackType.Transform);
		anim.TrackSetPath (trackIdx, ".");
		anim.TransformTrackInsertKey (trackIdx, 0f, translation, new Quat (0f, 0f, 0f, 1f).Normalized (), Vector3.One);
		anim.TransformTrackInsertKey (trackIdx, 0.5f, translation, new Quat (axis, Mathf.Pi).Normalized (), Vector3.One);
		anim.TransformTrackInsertKey (trackIdx, 1f, translation, new Quat (axis, 2 * Mathf.Pi).Normalized (), Vector3.One);
		return anim;	
	}

	public void Rotate (RotateDirection rotateDirection)
	{
		if (!AnimationPlayer.IsPlaying ()) {
			switch (rotateDirection) {
				case RotateDirection.Horizontal: AnimationPlayer.Play ("CardRotate_H"); break;
				case RotateDirection.HorizontalBackwards: AnimationPlayer.PlayBackwards ("CardRotate_H"); break;
				case RotateDirection.Vertical: AnimationPlayer.Play ("CardRotate_V"); break;
				case RotateDirection.VerticalBackwards: AnimationPlayer.PlayBackwards ("CardRotate_V"); break;
			}
			Timer.Start ();
		}
	}
	
	public void Rotate_D1 ()
	{
		if (!AnimationPlayer.IsPlaying ()) {
			UpdateDiagonalRotationAnimation ("CardRotate_D1");
			AnimationPlayer.Play ("CardRotate_D1");
			Timer.Start ();
		}
	}
	
	public void Rotate_D2 ()
	{
		if (!AnimationPlayer.IsPlaying ()) {
			UpdateDiagonalRotationAnimation ("CardRotate_D2");
			AnimationPlayer.Play ("CardRotate_D2");
			Timer.Start ();
		}
	}
	
	private void _on_Timer_timeout()
	{
		//Card.ToggleOwner ();
		BindCard (_card);
	}
}
