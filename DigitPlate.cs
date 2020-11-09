using Godot;
using System;

public class DigitPlate : Spatial
{

    protected Sprite3D Sprite3D => GetNode<Sprite3D> (nameof (Sprite3D));

    int _digit;

    [Export]
    public int Digit {
        get { return _digit; }
        set {
            _digit = value;
            LoadTexture ();
        }
    }

    void LoadTexture ()
    {
        Sprite3D.Texture = GD.Load<StreamTexture> ($"res://textures/digits/d{_digit}.png");
    }

    // replace base class exported prop with non-exported
    //public new Texture Texture { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        LoadTexture ();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
