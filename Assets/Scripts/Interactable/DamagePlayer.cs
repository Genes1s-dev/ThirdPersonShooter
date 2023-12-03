using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamagePlayerCube : Interactible
{
    [SerializeField] PlayerHealth player;
    [SerializeField] float damage = 20f;
    [SerializeField] GameObject sniperRifleObject;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject healCube;
    protected override void Interact()
    {
        player.TakeDamage(damage);
        sniperRifleObject.SetActive(true);
        canvas.gameObject.SetActive(false);
        healCube.SetActive(true);
    }
}
