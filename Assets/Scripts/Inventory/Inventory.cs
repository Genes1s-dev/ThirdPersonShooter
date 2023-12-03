using System.Collections;
using System.Collections.Generic;
using TMPro;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance {get; private set;}
    private List<BaseWeapon> itemsList = new List<BaseWeapon>();
    public Transform itemContent;
    public GameObject inventoryItem;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void AddItem(BaseWeapon baseWeapon)
    {
        if (itemsList.Count < 4 && !itemsList.Contains(baseWeapon))
        {
            itemsList.Add(baseWeapon);
            CreateItemSlot(baseWeapon);
        }
    }


    public void ClearList()
    {
        itemsList.Clear();
    }

    public List<BaseWeapon> GetItemsList()
    {
        return itemsList;
    }

    public void ListItems()//?
    {
        //обновляем набор предметов при открытии инвентаря
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (BaseWeapon itemToCheck in itemsList)
        {
            /*if (itemContent.Find(itemToCheck.Value.ToString()) != null)
            {
                AddItemStack(itemToCheck.Key, itemToCheck.Value);
            } else {
                CreateItemSlot(itemToCheck.Key);
            }*/
            CreateItemSlot(itemToCheck);
        }
    }

    private void CreateItemSlot(BaseWeapon weapon)
    {
        GameObject itemObject = Instantiate(inventoryItem, itemContent);//создание дочернего объекта 
        Image icon = itemObject.transform.Find("Icon").GetComponent<Image>();
        icon.sprite = weapon.weaponSO.icon;

        ItemSlot itemSlot;
        if (itemObject.TryGetComponent(out itemSlot))
        {
            itemSlot.SetItemData(weapon.weaponSO);
        }
    }
}
