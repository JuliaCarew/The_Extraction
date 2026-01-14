using UnityEngine;

[CreateAssetMenu(fileName = "RoomData", menuName = "Scriptable Objects/RoomData")]
public class LevelDataSO : ScriptableObject
{
    public int enemyCount;
    public string levelName;
    // This will collect the necesary info on each level (Room)
    // this script is still in development
    
    public void ClearRoom()
    {
        PlayerEvents.Instance.RoomClear();
    }
}
