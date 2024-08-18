using System;
using UnityEngine;

public abstract class Health
{
    protected int BaseHealth;
    protected int CurrentHealth;

    public Action<float> HealthChanged;
    public Action Died;

    public Health(int health)
    {
        BaseHealth = health;
        CurrentHealth = BaseHealth;
    }

    public float GetCurrentHealthPercentage() => (float)CurrentHealth / BaseHealth;

    public void RecieveDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        CurrentHealth -= damage;
        HealthChanged?.Invoke(GetCurrentHealthPercentage());

        if (CurrentHealth <= 0)
            Died?.Invoke();
    }
}
