using System;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject playerHealthbar;


    #region Events
    public event Action OnPlayerHealthChange = delegate { };
    #endregion

    private protected override void Awake()
    {
        base.Awake();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
