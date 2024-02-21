using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthBar : MonoBehaviour
{
    [SerializeField] private EntityHealth _entityHealth;
    
    private Slider _healthbar;

    private float _currentHealth => _entityHealth.Health;
    private float _maxHealth => _entityHealth.MaxHealth;
    private float _normalizedHealth => _currentHealth / _maxHealth;

    private void OnEnable()
    {
        _entityHealth.HealthChanged += ChangeBarValue;
        _healthbar = GetComponent<Slider>();
    }
    
    private void Start()
    {
        _healthbar.value = _normalizedHealth;
        
    }

    private void OnDisable()
    {
        _entityHealth.HealthChanged -= ChangeBarValue;
    }

    private void ChangeBarValue()
    {
        _healthbar.value = _normalizedHealth;
    }
}
