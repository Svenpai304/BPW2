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
    public HealthBar healthBar;
    void Start()
    {
        health = maxHealth;
        UpdateHealthBar();
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
        UpdateHealthBar();
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
        UpdateHealthBar();
    }

    public void ChangeMaxHealth(int newMax)
    {
        healthBar.UpdateBarMaxValue(newMax/maxHealth);
        maxHealth = newMax;
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        float barValue = (float)health / (float)maxHealth;
        healthBar.UpdateBarValue(barValue);
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

