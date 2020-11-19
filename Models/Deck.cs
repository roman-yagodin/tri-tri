using Godot;
using System;
using System.Collections.Generic;

public interface IDeck
{
    IList<ICard> Cards { get; set; }
}

public class Deck: IDeck
{
    public IList<ICard> Cards { get; set; }
}