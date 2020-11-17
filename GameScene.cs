using Godot;
using System;

public class GameScene : Spatial
{
	protected Camera Camera => GetNode<Camera> (nameof (Camera));

	protected Spatial Board => GetNode<Spatial> (nameof (Board));

	protected CardScene Card1 => Board.GetNode<CardScene> (nameof (Card1));

	protected CardScene Card2 => Board.GetNode<CardScene> (nameof (Card2));
	
	protected CardScene Card3 => Board.GetNode<CardScene> (nameof (Card3));
	
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
		else if (inputEvent.IsActionPressed("test_rotate1")) {
			Card1.Rotate_H ();
		}
		else if (inputEvent.IsActionPressed("test_rotate2")) {
			Card2.Rotate_D1 ();
		}
		else if (inputEvent.IsActionPressed("test_rotate3")) {
			Card2.Rotate_D2 ();
		}
		else if (inputEvent.IsActionPressed("test_rotate4")) {
			Card3.Rotate_V ();
		}
	}
}
