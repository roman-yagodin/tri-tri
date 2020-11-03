using Godot;
using System;

public class TexturedQuadMesh : Spatial
{
	protected MeshInstance QuadMesh => GetNode<MeshInstance> (nameof (QuadMesh));
	
	[Export]
	public Texture Texture {
		get { return ((SpatialMaterial) QuadMesh.GetSurfaceMaterial (0)).AlbedoTexture; }
		set {
			var templateMaterial = (SpatialMaterial) QuadMesh.GetSurfaceMaterial (0);
			var material = new SpatialMaterial ();
			material.AlbedoTexture = value;
			material.ParamsUseAlphaScissor = templateMaterial.ParamsUseAlphaScissor;
			material.ParamsAlphaScissorThreshold = templateMaterial.ParamsAlphaScissorThreshold;
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
