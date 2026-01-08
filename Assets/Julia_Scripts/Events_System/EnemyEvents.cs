using UnityEngine;
using System;

public class EnemyEvents
{
    public static event Action<GameObject> Spawned;
    public static event Action<GameObject> Died;
    public static event Action SpottedPlayer;
    public static event Action LostPlayer;
    public static event Action<float> OnEnemyDetectionChanged; // detection level

    public static void EnemySpawned(GameObject enemy) => Spawned?.Invoke(enemy);
    public static void EnemyDied(GameObject enemy) => Died?.Invoke(enemy);
    public static void EnemySpottedPlayer() => SpottedPlayer?.Invoke();
    public static void EnemyLostPlayer() => LostPlayer?.Invoke();
    public static void EnemyDetectionChanged(float detectionLevel) => OnEnemyDetectionChanged?.Invoke(detectionLevel);
}
