using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Slider _healthbar;
    [SerializeField] private EntityHealth _entityHealth;
    [SerializeField] private float _valueChangeSpeed;
    
    private char _slash = '/';
    private Coroutine _changeHealthBarValueCoroutine;

    private float _currentHealth => _entityHealth.Health;
    private float _maxHealth => _entityHealth.MaxHealth;
    private float _normalizedHealth => _currentHealth / _maxHealth;

    private void OnEnable()
    {
        _entityHealth.HealthChanged += ChangeValue;
    }
    
    private void Start()
    {
        _text.text = _currentHealth.ToString("0") + _slash + _maxHealth.ToString("0");
        _healthbar.value = _normalizedHealth;
    }

    private void OnDisable()
    {
        _entityHealth.HealthChanged -= ChangeValue;
    }

    private void ChangeValue()
    {
        _text.text = _currentHealth.ToString("0") + _slash + _maxHealth.ToString("0");
        ChangeHealthBarValue();
    }
    
    private void ChangeHealthBarValue()
    {
        if (_changeHealthBarValueCoroutine != null)
        {
            StopCoroutine(_changeHealthBarValueCoroutine);
        }
        
        print(_normalizedHealth);
        
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
