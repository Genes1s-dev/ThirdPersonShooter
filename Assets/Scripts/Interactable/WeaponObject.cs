using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : Interactible
{
    [SerializeField] private BaseWeapon weapon;
    protected override void Interact()
    {
        Inventory.Instance.AddItem(weapon);
        this.gameObject.SetActive(false);
    }
}
