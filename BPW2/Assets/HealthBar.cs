using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    public RectTransform barRect;
    public RectTransform frameRect;

    private float currentScale;
    private float scaleTranslationRatio;

    private void Start()
    {
        currentScale = barRect.localScale.x;
        scaleTranslationRatio = (barRect.rect.x * barRect.localScale.x) / currentScale;
    }

    public void UpdateBarValue(float health)
    {
        bar.fillAmount = health;
    }

    public void UpdateBarMaxValue(float newScale)
    {
        barRect.localScale = new Vector3(newScale * currentScale, barRect.localScale.y, barRect.localScale.z);
        barRect.Translate(new Vector3((currentScale - newScale * currentScale) * scaleTranslationRatio, 0, 0));
        frameRect.localScale = new Vector3(newScale * currentScale, frameRect.localScale.y, frameRect.localScale.z);
        frameRect.Translate(new Vector3((currentScale - newScale * currentScale) * scaleTranslationRatio, 0, 0));
        currentScale *= newScale;
    }

}
