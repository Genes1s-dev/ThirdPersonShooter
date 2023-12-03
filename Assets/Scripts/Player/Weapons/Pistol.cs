using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pistol : BaseWeapon
{
    private void Start()
    {
        currentAmmo = weaponSO.ammoCapacity;
        UpdateUI();
    }

    public override void Fire()
    {
        currentAmmo--;
        UpdateUI();

        if (currentAmmo <= 0)
        {
            isReloading = true;
            StartCoroutine(nameof(Reload));
        } else {
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Ray ray = Camera.main.ScreenPointToRay(screenCenter); //луч от центра экрана в направлении взгляда игрока
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, weaponSO.maxShootingDistance))
            {
                IDamageable hitOject = hit.collider.gameObject.GetComponent<IDamageable>();
                hitOject?.TakeDamage(weaponSO.damage);
                //Debug.Log("Hit!");
            }
            particles.Play();
            isReloading = true;
            StartCoroutine(nameof(SmallReload));
        }
    }
}
