using System;
using UnityEngine;

public abstract class Health
{
    private Animator _animator;
    protected int BaseHealth;
    protected int CurrentHealth;

    public Action<float> HealthChanged;
    public Action Died;

    public Health(Animator animator,int health)
    {
        _animator = animator;
        BaseHealth = health;
        CurrentHealth = BaseHealth;
    }

    public float GetCurrentHealthPercentage() => (float)CurrentHealth / BaseHealth;

    public void RecieveDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        CurrentHealth -= damage;
        _animator.SetTrigger("Hitted");
        HealthChanged?.Invoke(GetCurrentHealthPercentage());

        if (CurrentHealth <= 0)
            Died?.Invoke();
    }
}
