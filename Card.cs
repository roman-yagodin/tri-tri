using Godot;
using System;

public class Card : Spatial
{
	protected DigitPlate DigitPlate1 => GetNode<DigitPlate> (nameof (DigitPlate1));

	protected DigitPlate DigitPlate2 => GetNode<DigitPlate> (nameof (DigitPlate2));
	
	protected DigitPlate DigitPlate3 => GetNode<DigitPlate> (nameof (DigitPlate3));
	
	protected DigitPlate DigitPlate4 => GetNode<DigitPlate> (nameof (DigitPlate4));

	protected TexturedQuadMesh TexturedQuadMesh => GetNode<TexturedQuadMesh> (nameof (TexturedQuadMesh));

	[Export]
	public int Digit1 {
		get { return DigitPlate1.Digit; }
		set { DigitPlate1.Digit = value; }
	}

	[Export]
	public int Digit2 {
		get { return DigitPlate2.Digit; }
		set { DigitPlate2.Digit = value; }
	}

	[Export]
	public int Digit3 {
		get { return DigitPlate3.Digit; }
		set { DigitPlate3.Digit = value; }
	}

	[Export]
	public int Digit4 {
		get { return DigitPlate4.Digit; }
		set { DigitPlate4.Digit = value; }
	}

	[Export]
	public Texture Texture {
		get { return TexturedQuadMesh.Texture; }
		set { TexturedQuadMesh.Texture = value; }
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
	
	}
}
