using Godot;
using System;

public class TurnIndicator : MeshInstance
{
    public override void _Ready()
    {
        base._Ready();

        //var mesh = CreateMesh();
        //ResourceSaver.Save("res://meshes/indicator.tres", mesh);
    }

    ArrayMesh CreateMesh()
    {
        var st = new SurfaceTool();
        st.Begin(Mesh.PrimitiveType.TriangleStrip);
        st.AddColor(Color.ColorN("yellow"));
        
        var a = new Vector3(0f, 1f, 0f);
        var b = new Vector3(-0.86f, -0.5f, -0.5f);
        var c = new Vector3(0.86f, -0.5f, -0.5f);
        var d = new Vector3(0f, -0.5f, 1f);
        
        st.AddColor(Color.ColorN("yellow"));
        st.AddVertex(a);
        st.AddVertex(c);
        st.AddVertex(b);
        
        st.AddColor(Color.ColorN("red"));
        st.AddVertex(d);
        st.AddVertex(a);

        st.AddColor(Color.ColorN("blue"));
        st.AddVertex(c);
        st.AddVertex(d);

        st.AddColor(Color.ColorN("green"));
        st.AddVertex(b);
        st.AddVertex(c);

        st.GenerateNormals();

        return st.Commit();
    }
}
