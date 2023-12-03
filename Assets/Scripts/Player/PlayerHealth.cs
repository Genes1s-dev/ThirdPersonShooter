using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private float currentHealth;
    private float lerpTimer;
    [Header("Health Bar")]
    [SerializeField] float maxHealth = 500f;
    [SerializeField] float chipSpeed = 2f;  //время изменения длины полосы здоровья - 2 сек.

    [SerializeField] Image frontHealthBar;
    [SerializeField] Image backHealthBar;

    [SerializeField] TextMeshProUGUI healthText;

    [Header("Damage Overlay")]
    [SerializeField] Image overlay;
    [SerializeField] float duration;
    [SerializeField] float fadeSpeed;
    private float timerDuration;

    private void Start()
    {
        currentHealth = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);  //при старте прозрачность эффекта обнуляется
    }

    private void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        /*для теста
        if (Input.GetKeyDown(KeyCode.Z)) {
            TakeDamage(Random.Range(5, 20));
        } */

        if (Input.GetKeyDown(KeyCode.X)) {
            RestoreHealth(Random.Range(5, 20));
        }

        //создаём эффект исчезновения красной области на экране при получении урона
        if (overlay.color.a > 0) {
            if (currentHealth < 30) {  //пока здоровье ниже 30, эффект не будет пропадать
                return;
            }
            timerDuration += Time.deltaTime;
            if (timerDuration > duration) {
                //fade image
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);  //со временем изменяем прозрачность эффекта (альфу)
            }
        }
    }

    public void UpdateHealthUI()
    {
        //реализуем изменение полоски жизни при получении урона
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;

        float healthFraction = currentHealth / maxHealth; //текущий процент здоровья

        if (fillB > healthFraction) {  //когда получаем урон
            frontHealthBar.fillAmount = healthFraction;  //мнговенно меняем переднюю полосу здоровья под новое значение
            backHealthBar.color = Color.red;  //меняем цвет задней полосы здоровья и запускаем соответствующую анимацию её уменьшения

            lerpTimer += Time.deltaTime;
            float t = lerpTimer / chipSpeed; //процент пройденного времени анимации
            t *= t;  //процент пройденного времени анимации будет расти в геометрической прогрессии, т.е. чем больше времени прошло, тем больше будет заполняться полоска жизни за единицу времени!
            backHealthBar.fillAmount = Mathf.Lerp(fillB, healthFraction, t);
        }

        //обратная логика при лечении
        if (fillF < healthFraction) {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = healthFraction;

            lerpTimer += Time.deltaTime;
            float t = lerpTimer / chipSpeed;
            t *= t;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, healthFraction, t);
        }

        healthText.text = Mathf.Round(currentHealth) + "/" + Mathf.Round(maxHealth);  //обновляем УИ
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        lerpTimer = 0f;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0.75f); //при получении урона изначально эффект не является прозрачным (альфа = 0.75)
        timerDuration = 0f;

        if (currentHealth <= 0f)
        {
            GameManager.Instance.HandlePlayerDeath();
        }
    }

    public void RestoreHealth(float healAmount)
    {
        currentHealth += healAmount;
        lerpTimer = 0f;
    }

}
