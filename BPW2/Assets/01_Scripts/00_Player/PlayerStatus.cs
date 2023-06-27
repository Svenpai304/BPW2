using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public List<OnDamageEffect> onDamageEffects;
    public List<OnHealEffect> onHealEffects;
    public List<OnDeathEffect> onDeathEffects;

    public PlayerActions actions;
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        foreach (var effect in onDamageEffects)
        {
            if (effect != null)
            {
                damage = effect.OnDamage(damage);
            }
        }
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        if (health == 0)
        {
            Die();
        }
    }
    
    public void HealDamage(int heal)
    {
        foreach (var effect in onHealEffects)
        {
            if (effect != null)
            {
                heal = effect.OnHeal(heal);
            }
        }
        health += heal;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public void Die()
    {
        foreach (var effect in onDeathEffects)
        {
            if (effect != null)
            {
                effect.OnDeath();

            }
        }
    }
}

