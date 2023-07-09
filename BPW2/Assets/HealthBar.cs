using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    private RectTransform barRect;
    public RectTransform frameRect;

    private float currentScale;
    private float scaleTranslationRatio;

    private void Start()
    {
        barRect = GetComponent<RectTransform>();
        currentScale = barRect.localScale.x;
        scaleTranslationRatio = (barRect.rect.x * barRect.localScale.x)/ currentScale;
    }

    public void UpdateBarValue(float health)
    {
        bar.fillAmount = health;
    }

    public void UpdateBarMaxValue(float newScale)
    {
        barRect.localScale = new Vector3(newScale * currentScale, barRect.localScale.y, barRect.localScale.z);
        barRect.Translate(new Vector3((newScale * currentScale - currentScale) * scaleTranslationRatio, 0, 0)); 
        frameRect.localScale = new Vector3(newScale * currentScale, barRect.localScale.y, barRect.localScale.z);
        frameRect.Translate(new Vector3((newScale * currentScale - currentScale) * scaleTranslationRatio, 0, 0));
        currentScale *= newScale;
    }

}
