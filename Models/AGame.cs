using System;
using Godot;

public abstract class AGame
{
	public ABoard Board { get; set; }

	public APlayer Enemy { get; set; }

	public APlayer Player { get; set; }

	private GameState _state = GameState.GameOver;

	public GameState State {
		get => _state;
		set {
			_state = value;
			if (OnStateChanged != null) {
				OnStateChanged (this, EventArgs.Empty);
			}
		}
	}

	public event Action<object, EventArgs> OnStateChanged;

	public abstract void Start();

	public abstract void PlayerTurn (int cardIdx);

	public abstract void EnemyTurn ();

	protected virtual void GameOverCheck ()
	{
		if (IsOver ()) {
			State = GameState.GameOver;

			AnalyzeResults ();
		}
	}

	protected abstract void AnalyzeResults();

	public virtual bool IsOver () => Board.IsFull ();
}
