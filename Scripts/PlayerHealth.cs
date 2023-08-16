using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _startHealth;
    
    private int _health;
    private int _damage = 10;
    private int _heal = 10;

    public int MaxHealth => _maxHealth;
    public int Health => _health;
    
    public event Action HealthChanged;
    
    private void Awake()
    {
        _health = _startHealth;
    }

    public void TakeDamage()
    {
        if (_health > 0)
            _health -= _damage;

        _health = Mathf.Clamp(_health, 0, _maxHealth);
        
        HealthChanged?.Invoke();
    }

    public void Heal()
    {
        if (_health < _maxHealth)
            _health += _heal;

        _health = Mathf.Clamp(_health, 0, _maxHealth);

        HealthChanged?.Invoke();
    }
}
