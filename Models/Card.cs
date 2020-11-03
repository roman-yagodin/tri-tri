using Godot;
using System;

public interface ICard
{
    string Name { get; set; }

    string TextureName { get; set; }

    int [] Values { get; set; }

    float Rarity { get; set; }

    CardOwner Owner { get; set; }

    bool IsBlank { get; set; }

    string GetTextureFilename ();

    ICard Clone ();
}

public class Card : ICard
{
    public string Name { get; set; }
    
    public string TextureName { get; set; }
    
    public int[] Values { get; set; }

    public float Rarity { get; set; }

    public CardOwner Owner { get; set; } = CardOwner.Neutral;

    public bool IsBlank { get; set; }

    public string GetTextureFilename () => $"res://textures/cards/{TextureName}.png";

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

    public ICard Clone ()
    {
        return (ICard) this.MemberwiseClone ();
    }
}
