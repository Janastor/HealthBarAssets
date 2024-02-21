using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : MonoBehaviour
{
    private float _startingHealth = 60f;
    private float _maxHealth = 100f;
    
    public event UnityAction OutOfHealth;
    public event UnityAction HealthChanged;

    public float Health { get; private set; }
    public float MaxHealth { get; private set; }

    private void Awake()
    {
        MaxHealth = _maxHealth;
        SetHealth(_startingHealth);
    }

    public void SetHealth(float health)
    {
        Health = health;
    }

    public void DecreaseHealth(float amount)
    {
        Health -= amount;

        if (Health <= 0)
        {
            OutOfHealth?.Invoke();
            Health = 0;
        }
        
        HealthChanged?.Invoke();
    }

    public void AddHealth(float amount)
    {
        Health += amount;
        
        if (Health > _maxHealth)
            Health = _maxHealth;

        HealthChanged?.Invoke();
    }
}
