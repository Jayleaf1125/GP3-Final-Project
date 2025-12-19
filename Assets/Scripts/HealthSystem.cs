using System;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro.EditorUtilities;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Parameters")]
    float _currentHealth;
    [SerializeField] float _maxHealth;

    [Header("Healthbar UI Parameters")]
    [SerializeField] GameObject healthbar;
    [SerializeField] float colorChangeDelay;

    public bool PlayerDebugging = false;
    Image _healthBarFill;
    SpriteRenderer _sr;
    SoundManager _soundManager;

    private void Awake()
    {
        _healthBarFill = healthbar.transform.GetChild(1).gameObject.GetComponent<Image>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentHealth = _maxHealth;
        _sr = GetComponent<SpriteRenderer>();
        _soundManager = SoundManager.instance;
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

        if (gameObject.CompareTag("Player"))
        {
            _soundManager.PlayPlayerDamageSound();
        } else
        {
            _soundManager.PlayEnemyDamageSound();
        }

            StartCoroutine(DamageColor());

        if (reamainingHealth <= 0)
        {
            _currentHealth = 0;

            if (gameObject.CompareTag("Player"))
            {
                _soundManager.PlayPlayerDeathSound();
            } else
            {
                _soundManager.PlayEnemyDeathSound();
            }

                UpdateHealthbar();
            Destroy(gameObject);
            return;
        }

        _currentHealth = reamainingHealth;
        UpdateHealthbar();
    }

    public void HealHealth(float heal)
    {
        float reamainingHealth = _currentHealth + heal;
        StartCoroutine(HealColor());

        if (reamainingHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
            UpdateHealthbar();
            return;
        }

        _currentHealth = reamainingHealth;
        UpdateHealthbar();
    }

    IEnumerator DamageColor()
    {
        Color orginalColor = _sr.color;
        _sr.color = Color.red;
        yield return new WaitForSeconds(colorChangeDelay);
        _sr.color = orginalColor;
    }

    IEnumerator HealColor()
    {
        Color orginalColor = _sr.color;
        _sr.color = Color.green;
        yield return new WaitForSeconds(colorChangeDelay);
        _sr.color = orginalColor;
    }

    void UpdateHealthbar()
    {
        _healthBarFill.fillAmount = (float) _currentHealth / (float) _maxHealth;
    }
}
