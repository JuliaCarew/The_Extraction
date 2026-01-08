using System;

public class PlayerEvents
{
    public static event Action StartedMoving;
    public static event Action StoppedMoving;
    public static event Action Died;

    public static void PlayerStartedMoving() => StartedMoving?.Invoke();
    public static void PlayerStoppedMoving() => StoppedMoving?.Invoke();
    public static void PlayerDied() => Died?.Invoke();
}
