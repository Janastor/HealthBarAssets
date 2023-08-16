using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Slider _healthbar;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private float _healthbarChangeDuration;
    
    private char _slash = '/';
    private Coroutine _changeHealthBarValueCoroutine;

    private int _currentHealth => _playerHealth.Health;
    private int _maxHealth => _playerHealth.MaxHealth;

    private void OnEnable()
    {
        _playerHealth.HealthChanged += ChangeHealth;
        _healthbar.maxValue = _maxHealth;
    }
    
    private void Start()
    {
        _text.text = _currentHealth.ToString() + _slash + _maxHealth.ToString();
        _healthbar.value = _currentHealth;
    }

    private void OnDisable()
    {
        _playerHealth.HealthChanged -= ChangeHealth;
    }

    private void ChangeHealth()
    {
        _text.text = _currentHealth.ToString() + _slash + _maxHealth.ToString();
        StartChangeHealthBarCoroutine();
    }
    
    private void StartChangeHealthBarCoroutine()
    {
        if (_changeHealthBarValueCoroutine != null)
        {
            StopCoroutine(_changeHealthBarValueCoroutine);
            _changeHealthBarValueCoroutine = null;
        }
        
        _changeHealthBarValueCoroutine = StartCoroutine(ChangeHealthBar(_healthbar.value, _playerHealth.Health));
    }

    private IEnumerator ChangeHealthBar(float startingValue, float targetValue)
    {
        float timePassed = 0;
        float normalizedTime = 0;

        while (_healthbar.value != targetValue)
        {
            _healthbar.value = Mathf.MoveTowards(startingValue, targetValue, normalizedTime);
            timePassed += Time.deltaTime;
            normalizedTime = timePassed / _healthbarChangeDuration;
            
            yield return null;
        }
    }
}
