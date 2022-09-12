namespace TriTri;

[Tool]
public class DigitPlate : Spatial
{
	protected Sprite3D Sprite3D => GetNode<Sprite3D> (nameof (Sprite3D));

	int _digit;

	[Export]
	public int Digit {
		get { return _digit; }
		set {
			_digit = value;
			Bind (_digit);
		}
	}

	void Bind (int digit)
	{
		if (digit < 0 || digit > 10) {
			return;
		}
		
		Sprite3D.Texture = GD.Load<StreamTexture> ($"res://textures/digits/d{_digit}.png");
	}
}
