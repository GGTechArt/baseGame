using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIBar : MonoBehaviour
{
    //[SerializeField] BaseDamageable damageable;

    //[SerializeField] Image bar;

    //float newValue;

    //private void Start()
    //{
    //    if (damageable != null)
    //        damageable.healthChanged += UpdateBar;

    //    UpdateBar(damageable.health);
    //}
    //public void UpdateBar(float currentHealth)
    //{
    //    newValue = (1 / damageable.maxHealth) * currentHealth;
    //    //StartCoroutine(UpdateBarCoroutine(currentHealth, 5f));
    //}

    //private void Update()
    //{
    //    if (newValue != bar.fillAmount)
    //    {
    //        bar.fillAmount = Mathf.MoveTowards(bar.fillAmount, newValue, 5 * Time.deltaTime);
    //    }
    //}

    ////private void OnEnable()
    ////{
    ////    if (damageable != null)
    ////        damageable.healthChanged += UpdateBar;
    ////}

    //private void OnDestroy()
    //{
    //    if (damageable != null)
    //        damageable.healthChanged -= UpdateBar;
    //}

    //IEnumerator UpdateBarCoroutine(float endValue, float speed)
    //{
    //    float distance = Mathf.Abs(endValue - bar.fillAmount);
    //    float totalTime = distance / speed;
    //    float currentTime = 0f;

    //    while (currentTime < totalTime)
    //    {
    //        currentTime += Time.deltaTime;
    //        float percent = currentTime / totalTime;
    //        bar.fillAmount = (1 / damageable.maxHealth) * Mathf.Lerp(bar.fillAmount, endValue, percent);
    //        yield return null;
    //    }

    //    bar.fillAmount = (1 / damageable.maxHealth) * endValue;
    //}
}
