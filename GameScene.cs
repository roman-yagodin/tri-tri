using Godot;
using System;

public class GameScene : Spatial
{
	protected Camera Camera => GetNode<Camera> (nameof (Camera));

	protected Spatial Board => GetNode<Spatial> (nameof (Board));

	protected CardScene Card2 => Board.GetNode<CardScene> (nameof (Card2));

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{

	}

	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent.IsActionPressed("ui_left")) {
			Board.Rotate (new Vector3 (0, 1, 0), (float) Math.PI / 16);
		}
		else if (inputEvent.IsActionPressed("ui_right")) {
			Board.Rotate (new Vector3 (0, 1, 0), -(float) Math.PI / 16);
		}
		else if (inputEvent.IsActionPressed("ui_select")) {
			Card2.StartRotate ();
		}
	}
}
