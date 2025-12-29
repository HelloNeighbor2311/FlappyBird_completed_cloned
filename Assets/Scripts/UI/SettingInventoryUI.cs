using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingInventoryUI : MonoBehaviour
{
    public static SettingInventoryUI Instance;
    public event EventHandler onOpenSkinManager;


    [SerializeField] private Button skinBtn;

    private void Awake()
    {
        if(Instance != null && Instance == this)
        {
            Destroy(gameObject);
        }
        Instance = this;
        
    }
    private void Start()
    {
        skinBtn.onClick.AddListener(() => {
            onOpenSkinManager?.Invoke(this, EventArgs.Empty);
        });
    }
}
