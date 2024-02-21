using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]

public class HealthText : MonoBehaviour
{
    [SerializeField] private EntityHealth _entityHealth;

    private TMP_Text _text;
    private const string DisplayFormat = "0";
    private const char Slash = '/';
    private Coroutine _changeHealthBarValueCoroutine;

    private float _currentHealth => _entityHealth.Health;
    private float _maxHealth => _entityHealth.MaxHealth;

    private void OnEnable()
    {
        _entityHealth.HealthChanged += ChangeHealtValue;
    }
    
    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _text.text = _currentHealth.ToString(DisplayFormat) + Slash + _maxHealth.ToString(DisplayFormat);
    }

    private void OnDisable()
    {
        _entityHealth.HealthChanged -= ChangeHealtValue;
    }

    private void ChangeHealtValue()
    {
        _text.text = _currentHealth.ToString(DisplayFormat) + Slash + _maxHealth.ToString(DisplayFormat);
    }
}
