using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : BaseWeapon
{
    [SerializeField] GameObject aimingScreen;
    [SerializeField] private float aimingOffset = 7f;
    [SerializeField] private int aimingDMGBonus = 10;
    private Vector3 cameraPosition;

    private void Start()
    {
        cameraPosition = Camera.main.transform.localPosition;
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
                if (playerMotor.aiming)
                {
                    hitOject?.TakeDamage(weaponSO.damage + aimingDMGBonus);
                } else {
                    hitOject?.TakeDamage(weaponSO.damage);
                }

                //Debug.Log("Hit!");
            }
            particles.Play();
            isReloading = true;
            StartCoroutine(nameof(SmallReload));
        }
    }

    public override void Aim()
    {
        Vector3 aimingCameraPosition = new Vector3(cameraPosition.x, cameraPosition.y, cameraPosition.z + aimingOffset);

        if (playerMotor.aiming)
        {
            Camera.main.transform.localPosition = aimingCameraPosition;
            aimingScreen.SetActive(true);
        } else {
            Camera.main.transform.localPosition = cameraPosition;
            aimingScreen.SetActive(false);
        } 
    }

}
