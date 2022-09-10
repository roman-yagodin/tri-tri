public enum GameState
{
    Initial,
    
    WaitForPlayer,

    PlayerTurn,

    WaitForEnemy,

    EnemyTurn,

    GameOverDraw,

    GameOverWin,

    GameOverLoose
}

public static class GameStateExtensions
{
    public static bool IsGameOver(this GameState state) =>
        state == GameState.GameOverDraw || state == GameState.GameOverLoose || state == GameState.GameOverWin;

}