using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName = "New Weapon";    
    public int weaponDMG;
    public int weaponClipSize;
    public float weaponFireRate;
    public float reloudTime;
    public float weaponRange;
    public float bulletForce;
}
