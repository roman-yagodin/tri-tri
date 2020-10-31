using Godot;
using System;

public interface ICard
{
    string Name { get; set; }

    string TextureName { get; set; }

    int [] Values { get; set; }

    int Owner { get; set; }

    string GetTextureFilename ();
}

public class Card : ICard
{
    public string Name { get; set; }
    
    public string TextureName { get; set; }
    
    public int[] Values { get; set; }

    public int Owner { get; set; }

    public string GetTextureFilename () => $"res://textures/cards/{TextureName}.png";

    public Card (string name, params int [] values)
    {
        Name = name;
        TextureName = name;
        Values = values;
    }
}
