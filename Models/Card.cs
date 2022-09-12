namespace TriTri;

public class Card: ACard
{
	public Card ()
	{
	}

	public Card (string name, params int [] values)
	{
		Name = name;
		TextureName = name;
		Values = values;
		Rarity = 1f;
	}
}