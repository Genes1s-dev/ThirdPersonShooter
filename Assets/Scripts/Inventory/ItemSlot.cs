using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour
{
    private WeaponSO weaponSO;
    public void SetItemData(WeaponSO weaponSO)
    {
        this.weaponSO = weaponSO;
    }

    public WeaponSO GetItemData()
    {
        return weaponSO;
    }
    
}
