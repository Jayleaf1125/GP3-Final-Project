using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timer;
    public float intialVal;
    public TextMeshProUGUI timerText;

    private Week10GameManager _gm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CountDown();
        _gm = Week10GameManager.Instance;
    }

    public void CountDown()
    {
        timer = 3f;
        UpdateText(timer);
    }

    public void Reset()
    {
        timer = intialVal;
        UpdateText(timer);
    }

    // Update is called once per frame
    void Update()
    {
        if (_gm.currentPhase == Week10GameManager.GamePhase.Starting) return;

        timer -= Time.deltaTime;
        UpdateText(timer);

        if (timer < 0)
        {
            switch (_gm.currentPhase)
            {
                case Week10GameManager.GamePhase.Starting:
                    Reset();
                    _gm.currentPhase = Week10GameManager.GamePhase.Started;
                    break;
                case Week10GameManager.GamePhase.Started:
                    _gm.KillAll();
                    break;
            }
        }
    }

        public void UpdateText(float time) => timerText.text = $"{time.ToString("F1")}s";
}
