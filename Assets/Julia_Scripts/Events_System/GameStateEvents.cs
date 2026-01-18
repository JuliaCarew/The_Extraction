using System;

public class GameStateEvents : SingletonBase<GameStateEvents>
{
    public event Action GameStarted;
    public event Action GamePaused;
    public event Action GameResumed;
    public event Action GameEnded;
    public event Action GameOver;
    public event Action Tutorial; 

    public event Action<GameState> StateChanged;

    public void RaiseGameStarted() => GameStarted?.Invoke();
    public void RaiseGamePaused() => GamePaused?.Invoke();
    public void RaiseGameResumed() => GameResumed?.Invoke();
    public void RaiseGameEnded() => GameEnded?.Invoke();
    public void RaiseGameOver() => GameOver?.Invoke();

    public void RaiseTutorial() => Tutorial?.Invoke(); 
    public void RaiseStateChanged(GameState state) => StateChanged?.Invoke(state);
}