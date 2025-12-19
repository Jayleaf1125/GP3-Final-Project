using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputActions;

// Credits: Samson

public class InputManager : Singleton<InputManager>, IPlayerActions
{
    public InputActions inputActions { get; private set; }

    #region Events
    public event Action<Vector2> Move = delegate { };
    public event Action<Vector2> Aim = delegate { };
    public event Action Sacrfirce = delegate { };
    public event Action Jump = delegate { };
    public event Action Shoot = delegate { };
    public event Action Dash = delegate { };
    #endregion

    public Vector2 MoveDirection => inputActions.Player.Move.ReadValue<Vector2>();

    public Vector2 AimDirection => inputActions.Player.Aim.ReadValue<Vector2>();


    private protected override void Awake()
    {
        base.Awake();
        CreatePlayerActions();
    }

    void CreatePlayerActions()
    {
        inputActions = new InputActions();
        inputActions.Player.SetCallbacks(this);
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Move.Invoke(context.ReadValue<Vector2>());
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        Aim.Invoke(context.ReadValue<Vector2>());
    }

    public void OnSacrfirce(InputAction.CallbackContext context)
    {
        if (context.performed) Sacrfirce.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) Jump.Invoke();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed) Shoot.Invoke();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed) Dash.Invoke();
    }
}
