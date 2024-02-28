using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : Item
{
    public List<OnHealEffect> onHealEffects = new List<OnHealEffect>();
    public List<OnDamageEffect> onDamageEffects = new List<OnDamageEffect>();
    public List<OnDeathEffect> onDeathEffects = new List<OnDeathEffect>();
    public List<OnAddEffect> onAddEffects = new List<OnAddEffect>();

    private void Start()
    {
        foreach (OnHealEffect effect in GetComponents<OnHealEffect>())
        {
            onHealEffects.Add(effect);
        }
        foreach (OnDamageEffect effect in GetComponents<OnDamageEffect>())
        {
            onDamageEffects.Add(effect);
        }
        foreach (OnDeathEffect effect in GetComponents<OnDeathEffect>())
        {
            onDeathEffects.Add(effect);
        }
        foreach (OnAddEffect effect in GetComponents<OnAddEffect>())
        {
            onAddEffects.Add(effect);
        }
    }

    public override void OnItemAdd()
    {
        base.OnItemAdd();
        foreach (OnHealEffect effect in onHealEffects)
        {
            playerStatus.onHealEffects.Add(effect);
        }
        foreach (OnDamageEffect effect in onDamageEffects)
        {
            playerStatus.onDamageEffects.Add(effect);
        }
        foreach (OnDeathEffect effect in onDeathEffects)
        {
            playerStatus.onDeathEffects.Add(effect);
        }
        foreach (OnAddEffect effect in GetComponents<OnAddEffect>())
        {
            effect.OnAdd();
        }
    }

    public override void OnItemRelease()
    {
        foreach (OnHealEffect effect in onHealEffects)
        {
            playerStatus.onHealEffects.Remove(effect);
        }
        foreach (OnDamageEffect effect in onDamageEffects)
        {
            playerStatus.onDamageEffects.Remove(effect);
        }
        foreach (OnDeathEffect effect in onDeathEffects)
        {
            playerStatus.onDeathEffects.Remove(effect);
        }
        foreach (OnAddEffect effect in GetComponents<OnAddEffect>())
        {
            effect.OnRemove();
        }
        base.OnItemRelease(); 
    }
}
