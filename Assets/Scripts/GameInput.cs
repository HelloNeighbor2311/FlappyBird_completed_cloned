using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class GameInput : MonoBehaviour
{
    public static GameInput instance;
    public event EventHandler onPauseAction;

    private PlayerInput gameInput;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        gameInput = new PlayerInput();
        
        
    }
    private void OnEnable()
    {   
        //hook
        gameInput.Enable();
        gameInput.Player.Pause.performed += Pause_performed;
    }

    private void OnDisable()
    {
        //unhook
        if (gameInput != null)
            gameInput.Player.Pause.performed -= Pause_performed;

        if (gameInput != null)
            gameInput.Disable();
    }
    private void OnDestroy()
    {
        //Totally clean
        if (gameInput != null)
        {
            gameInput.Player.Pause.performed -= Pause_performed;
            gameInput.Disable();
            gameInput.Dispose();
            gameInput = null;
        }

        if (instance == this)
            instance = null;
    }
    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!obj.performed) return;
        onPauseAction?.Invoke(this, EventArgs.Empty);   
    }
}
