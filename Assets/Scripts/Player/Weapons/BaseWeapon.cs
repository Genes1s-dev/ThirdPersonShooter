using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class BaseWeapon : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI ammoText;
    [SerializeField] protected GameObject reloadingUI;
    [SerializeField] protected Image reloadingImage;
    [SerializeField] protected ParticleSystem particles;
    public WeaponSO weaponSO;
    [SerializeField] protected PlayerMotor playerMotor;
    public bool isReloading {get; protected set;}
    protected int currentAmmo;

    public abstract void Fire();
    public virtual void Aim(){}
    public void UpdateUI()
    {
        ammoText.text = currentAmmo.ToString();
    }

    protected IEnumerator Reload()
    {
        reloadingUI.SetActive(true);

        float elapsed = 0f;
        float percentage;

        while (elapsed < weaponSO.reloadSpeed)
        {
            percentage = elapsed / weaponSO.reloadSpeed;
            reloadingImage.fillAmount = Mathf.Lerp(0f, 1f, percentage);
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        reloadingUI.SetActive(false);
        isReloading = false;

        currentAmmo = weaponSO.ammoCapacity;
        UpdateUI();
    }

    protected IEnumerator SmallReload()
    {     
        yield return new WaitForSeconds(weaponSO.cooldown);
        isReloading = false;
    }
}
