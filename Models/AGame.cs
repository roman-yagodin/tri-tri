namespace TriTri;

public class GameStateChangedEventArgs: EventArgs
{
	public GameState State { get; set; }

	public GameState PrevState { get; set; }
}

public abstract class AGame
{
	public ABoard Board { get; set; }

	public APlayer Enemy { get; set; }

	public APlayer Player { get; set; }

	private GameState _state = GameState.Initial;

	public GameState State {
		get => _state;
		set {
			if (_state != value) {
				var prevState = _state;
				_state = value;
				if (OnStateChanged != null) {
					OnStateChanged (this, new GameStateChangedEventArgs { State = value, PrevState = prevState });
				}
			}
		}
	}

	public event Action<object, GameStateChangedEventArgs> OnStateChanged;

	public abstract void Start();

	public abstract void PlayerTurn (int cardIdx);

	public abstract void EnemyTurn ();

	protected virtual void GameOverCheck ()
	{
		if (IsOver ())
			AnalyzeResults ();
	}

	protected abstract void AnalyzeResults();

	public virtual bool IsOver () => Board.IsFull ();
}
