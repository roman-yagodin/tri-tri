using Godot;
using System;

public class DealScene : Spatial
{
	IDeal _deal;

	public IDeal Deal {
		get { return _deal; }
		set {
			_deal = value;
			BindDeal (_deal);
		}
	}

	[Export]
	public float CardSpacing { get; set; } = 2.5f;

	[Export]
	public float CardRotation { get; set; } = 1;

	void BindDeal (IDeal deal)
	{
		if (_deal == null) {
			return;
		}

		var cardSceneRes = ResourceLoader.Load<PackedScene> ("res://CardScene.tscn");
		var idx = 0;
		foreach (var card in deal.Cards) {
			var cardScene = (CardScene) cardSceneRes.Instance ();
			cardScene.Card = card;
			cardScene.Name = "Card" + idx;
			cardScene.Translation = new Vector3 (0f, (deal.Cards.Count - 1) * CardSpacing / 2f - (idx * CardSpacing), 0f);
			cardScene.Rotate (new Vector3 (1f, 0f, 0f), Mathf.Deg2Rad (CardRotation));
			AddChild (cardScene);
			idx++;
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BindDeal (_deal);
	}
}
