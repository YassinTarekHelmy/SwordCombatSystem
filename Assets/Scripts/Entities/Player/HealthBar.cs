using System;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private bool _isWorldSpace;
    [SerializeField] private Image _fillImage;
    [SerializeField] private Image _lossEffectImage;

    [SerializeField] private float _fillSpeed;
    [SerializeField] private float _timeBeforeFill = 0.5f;
    float _timeBeforeFillCounter = 0f;

    [SerializeField] private float _maxHealth;
    private float _currentHealth;
    
    public event Action OnDeath = delegate {};
    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        
        float healthPercentage = _currentHealth / _maxHealth;

        _fillImage.fillAmount = healthPercentage;


        if (_currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    private void Update() {
        if (_lossEffectImage.fillAmount > _fillImage.fillAmount)
        {
            _timeBeforeFillCounter += Time.deltaTime;
            
            if (_timeBeforeFillCounter >= _timeBeforeFill)
            {
                _lossEffectImage.fillAmount -= Time.deltaTime * _fillSpeed;
            }
        }
        else
        {
            _timeBeforeFillCounter = 0f;
            _lossEffectImage.fillAmount = _fillImage.fillAmount;
        }

    }

    private void LateUpdate() {
        if (_isWorldSpace)
        {
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }
    }

}
