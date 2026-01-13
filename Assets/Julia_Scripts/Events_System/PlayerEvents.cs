using System;

public class PlayerEvents : SingletonBase<PlayerEvents>
{
    public event Action StartedMoving;
    public event Action StoppedMoving;
    public event Action Died;
    public event Action RoomCleared;
    public event Action toothCollected;
    public event Action moneyCollected;
    public event Action pickedUpWeapon;

    public void PlayerStartedMoving() => StartedMoving?.Invoke();
    public void PlayerStoppedMoving() => StoppedMoving?.Invoke();
    public void PlayerDied() => Died?.Invoke();
    public void RoomClear() => RoomCleared?.Invoke();
    public void ToothCollected() => toothCollected?.Invoke();
    public void MoneyCollected() => moneyCollected?.Invoke();
    public void WeaponPickedUp() => pickedUpWeapon?.Invoke();

}
