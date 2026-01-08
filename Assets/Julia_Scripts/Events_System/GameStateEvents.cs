using System;

public class GameStateEvents
{
    public static event Action GameStarted;
    public static event Action GamePaused;
    public static event Action GameResumed;
    public static event Action GameEnded;
    public static event Action<GameState> StateChanged;

    public static void RaiseGameStarted() => GameStarted?.Invoke();
    public static void RaiseGamePaused() => GamePaused?.Invoke();
    public static void RaiseGameResumed() => GameResumed?.Invoke();
    public static void RaiseGameEnded() => GameEnded?.Invoke();
    public static void RaiseStateChanged(GameState state) => StateChanged?.Invoke(state);
}
