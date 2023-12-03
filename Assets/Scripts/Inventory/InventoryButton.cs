using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] GameObject inventoryPanel;
    const string showInventory = "Show inventory";
    const string hideInventory = "Hide inventory";
    Button button;

    bool hidden;
    
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            hidden = !hidden;
            UpdateUI();
        });
    }

    void UpdateUI() 
    {
        buttonText.text = hidden ? showInventory : hideInventory;
        inventoryPanel.SetActive(!hidden);
    }
}
