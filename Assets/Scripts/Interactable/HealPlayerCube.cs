using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayerCube : Interactible
{
    [SerializeField] PlayerHealth player;
    [SerializeField] Canvas canvas;
    [SerializeField] float healAmount = 20f;
    protected override void Interact()
    {
        player.RestoreHealth(healAmount);
        canvas.gameObject.SetActive(false);
    }
}
