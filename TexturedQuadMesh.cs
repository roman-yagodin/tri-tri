using Godot;
using System;

public class TexturedQuadMesh : Spatial
{
	protected MeshInstance QuadMesh => GetNode<MeshInstance> (nameof (QuadMesh));
	
	protected SpatialMaterial Material => (SpatialMaterial) QuadMesh.GetSurfaceMaterial (0);
	
	[Export]
	public Texture Texture {
		get { return Material.AlbedoTexture; }
		set {
			var material = new SpatialMaterial ();
			material.AlbedoTexture = value;
			material.ParamsUseAlphaScissor = true;
			material.ParamsAlphaScissorThreshold = 0.5f;
			QuadMesh.SetSurfaceMaterial (0, material);
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		
	}
}
