using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject, IWeapon
{
    public string weapon_name;
    public int id;

    public void PickedUp()
    {
        // needs to set player / enemy bool to be able to dmg
        // instantiate based on string of model to model 3d object's name
    }

    public void Use()
    {
        
    }
}
// when player collides w it, move the model to player's hand position public Transform playerWeaponPosition;