using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class SmoothHealthBar : MonoBehaviour
{
    [SerializeField] private EntityHealth _entityHealth;
    [SerializeField] private float _valueChangeSpeed;
    
    private Slider _healthbar;
    private Coroutine _changeHealthBarValueCoroutine;

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
        ChangeHealthBarValue();
    }
    
    private void ChangeHealthBarValue()
    {
        if (_changeHealthBarValueCoroutine != null)
            StopCoroutine(_changeHealthBarValueCoroutine);

        _changeHealthBarValueCoroutine = StartCoroutine(ChangeHealthBarValueCoroutine());
    }

    private IEnumerator ChangeHealthBarValueCoroutine()
    {
        while (_healthbar.value != _normalizedHealth)
        {
            _healthbar.value = Mathf.MoveTowards(_healthbar.value, _normalizedHealth, _valueChangeSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
