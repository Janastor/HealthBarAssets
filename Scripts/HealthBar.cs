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

    private const string DisplayFormat = "0";
    private const char Slash = '/';
    private Coroutine _changeHealthBarValueCoroutine;

    private float _currentHealth => _entityHealth.Health;
    private float _maxHealth => _entityHealth.MaxHealth;
    private float _normalizedHealth => _currentHealth / _maxHealth;

    private void OnEnable()
    {
        _entityHealth.HealthChanged += ChangeHealthValues;
    }
    
    private void Start()
    {
        _text.text = _currentHealth.ToString(DisplayFormat) + Slash + _maxHealth.ToString(DisplayFormat);
        _healthbar.value = _normalizedHealth;
    }

    private void OnDisable()
    {
        _entityHealth.HealthChanged -= ChangeHealthValues;
    }

    private void ChangeHealthValues()
    {
        _text.text = _currentHealth.ToString(DisplayFormat) + Slash + _maxHealth.ToString(DisplayFormat);
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
