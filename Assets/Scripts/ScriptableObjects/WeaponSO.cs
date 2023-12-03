using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class WeaponSO : ScriptableObject
{
    public int ammoCapacity;
    public float reloadSpeed;
    public float cooldown;
    public int damage;
    public float maxShootingDistance;
    public float minShootingDistance;
    public Sprite icon;

}
