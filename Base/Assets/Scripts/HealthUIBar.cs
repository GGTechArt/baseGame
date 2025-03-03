using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIBar : MonoBehaviour
{
    [SerializeField] HealthComponent health;

    [SerializeField] Image bar;

    float newValue;

    private void Start()
    {
        if (health != null)
            health.HealthChanged += UpdateBar;

        UpdateBar(health.GetHealth());
    }
    public void UpdateBar(float currentHealth)
    {
        newValue = (1 / health.GetMaxHealth()) * currentHealth;
        //StartCoroutine(UpdateBarCoroutine(currentHealth, 5f));
    }

    private void Update()
    {
        if (newValue != bar.fillAmount)
        {
            bar.fillAmount = Mathf.MoveTowards(bar.fillAmount, newValue, 5 * Time.deltaTime);
        }
    }

    //private void OnEnable()
    //{
    //    if (damageable != null)
    //        damageable.healthChanged += UpdateBar;
    //}

    private void OnDestroy()
    {
        if (health != null)
            health.HealthChanged -= UpdateBar;
    }

    IEnumerator UpdateBarCoroutine(float endValue, float speed)
    {
        float distance = Mathf.Abs(endValue - bar.fillAmount);
        float totalTime = distance / speed;
        float currentTime = 0f;

        while (currentTime < totalTime)
        {
            currentTime += Time.deltaTime;
            float percent = currentTime / totalTime;
            bar.fillAmount = (1 / health.GetMaxHealth()) * Mathf.Lerp(bar.fillAmount, endValue, percent);
            yield return null;
        }

        bar.fillAmount = (1 / health.GetMaxHealth()) * endValue;
    }
}
