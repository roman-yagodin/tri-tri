using Godot;
using System;

public class Sphere1 : ImmediateGeometry
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		/*
		foreach(var prop in typeof ().GetProperties()) {
			GD.Print (prop.Name);*/

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process (float delta)
	{
		Clear ();
		Begin (Mesh.PrimitiveType.TriangleStrip);
		AddSphere (25, 25, 0.4f, true);
		//SetColor (new Color ("red"));
		End ();
	}
}
