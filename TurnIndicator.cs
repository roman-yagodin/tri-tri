using Godot;
using System;

public class TurnIndicator : MeshInstance
{
    public void Game_StateChanged(object sender, GameStateChangedEventArgs e)
    {
        if (e.State.IsGameOver()) {
            this.Visible = false;
        }
        else {
            // TODO: keep transform as in editor
            // TODO: where are the deals?
            this.Visible = true;
            if (e.State == GameState.WaitForPlayer || e.State == GameState.PlayerTurn) {
                this.Translation = new Vector3(11f, 10f, -2f);
            }
            else if (e.State == GameState.WaitForEnemy || e.State == GameState.EnemyTurn) {
                this.Translation = new Vector3(-11f, 10f, -2f);
            }
        }
    }
    
    public TurnIndicator()
    {
        //var mesh = CreateMesh();
        //ResourceSaver.Save("res://meshes/indicator.tres", mesh, ResourceSaver.SaverFlags.Compress);
    }

    ArrayMesh CreateMesh()
    {
        // from https://en.wikipedia.org/wiki/Tetrahedron
        var a = new Vector3(0.943f, 0f, -0.333f);
        var b = new Vector3(-0.471f, 0.816f, -0.333f);
        var c = new Vector3(-0.471f, -0.816f, -0.333f);
        var d = new Vector3(0f, 0f, 1f);

        var st = new SurfaceTool();
        st.Begin(Mesh.PrimitiveType.TriangleStrip);
        
        st.AddColor(Color.Color8(255, 255, 255)); st.AddVertex(a);
        st.AddColor(Color.Color8(255, 0, 0)); st.AddVertex(b);
        st.AddColor(Color.Color8(0, 255, 0)); st.AddVertex(c);
        st.AddColor(Color.Color8(0, 0, 255)); st.AddVertex(d);
        st.AddColor(Color.Color8(255, 255, 255)); st.AddVertex(a);
        st.AddColor(Color.Color8(255, 0, 0)); st.AddVertex(b);

        st.GenerateNormals();
        st.GenerateTangents();

        return st.Commit();
    }
}
