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
                if (e.PrevState == GameState.WaitForEnemy || e.PrevState == GameState.EnemyTurn) {
                    this.Transform = Transform.Identity;
                    this.Translate(new Vector3(11f, 10f, -2f));
                    this.RotateObjectLocal(new Vector3(0f, 0f, 1f), 90f);
                }
            }
            else if (e.State == GameState.WaitForEnemy || e.State == GameState.EnemyTurn) {
                if (e.PrevState == GameState.WaitForPlayer || e.PrevState == GameState.PlayerTurn) {
                    this.Transform = Transform.Identity;
                    this.Translate(new Vector3(-11f, 10f, -2f));
                    this.RotateObjectLocal(new Vector3(0f, 0f, 1f), 90f);
                }
            }
        }
    }

    ArrayMesh CreateMesh()
    {
        var st = new SurfaceTool();
        st.Begin(Mesh.PrimitiveType.TriangleStrip);
        st.AddColor(Color.ColorN("yellow"));
        
        // TODO: probably this is not tetrahedron!
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
