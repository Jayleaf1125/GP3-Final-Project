using System;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Parameters")]
    float _currentHealth;
    [SerializeField] float _maxHealth;

    [Header("Healthbar UI Parameters")]
    [SerializeField] GameObject healthbar;

    public bool PlayerDebugging = false;
    Image _healthBarFill;

    private void Awake()
    {
        _healthBarFill = healthbar.transform.GetChild(1).gameObject.GetComponent<Image>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame 
    void Update()
    {
        if (PlayerDebugging)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                DamageHealth(10f);
            }
        }
    }

    public void DamageHealth(float damage)
    {
        float reamainingHealth = _currentHealth - damage;


        if (reamainingHealth <= 0)
        {
            _currentHealth = 0;
            UpdateHealthbar();
            Destroy(gameObject);
            return;
        }

        _currentHealth = reamainingHealth;
        UpdateHealthbar();

        Debug.Log($"${this.gameObject.name} current health: {_currentHealth}");
    }

    void UpdateHealthbar()
    {
        _healthBarFill.fillAmount = (float) _currentHealth / (float) _maxHealth;
    }
}
