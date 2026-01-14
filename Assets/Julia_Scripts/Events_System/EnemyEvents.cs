using UnityEngine;
using System;

public class EnemyEvents : SingletonBase<EnemyEvents>
{
    public event Action<GameObject> Spawned;
    public event Action<GameObject> Died;
    public event Action<float> EnemyDiedWithDetection; // enemy died with detection level value
    public event Action SpottedPlayer;
    public event Action LostPlayer;
    public event Action<float> OnEnemyDetectionChanged; // detection level
    public event Action OnEnemyKilled;

    public void EnemySpawned(GameObject enemy) => Spawned?.Invoke(enemy);
    public void EnemyDied(GameObject enemy) => Died?.Invoke(enemy);
    public void EnemyKilled () => OnEnemyKilled?.Invoke();
    public void EnemyDiedWithDetectionLevel(float detectionLevel) => EnemyDiedWithDetection?.Invoke(detectionLevel);
    public void EnemySpottedPlayer() => SpottedPlayer?.Invoke();
    public void EnemyLostPlayer() => LostPlayer?.Invoke();
    public void EnemyDetectionChanged(float detectionLevel) => OnEnemyDetectionChanged?.Invoke(detectionLevel);
}
