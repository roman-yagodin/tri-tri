using Godot;
using System;
using System.Collections.Generic;

public interface IDeal
{
	IList<ICard> Cards { get; set; }

	bool IsOpen { get; set; }
}

public class Deal: IDeal
{
	public IList<ICard> Cards { get; set; } = new List<ICard> ();

	public bool IsOpen { get; set; }
}
