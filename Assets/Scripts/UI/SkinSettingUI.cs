using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class SkinSettingUI : MonoBehaviour, IUI
{

    public static SkinSettingUI Instance { get; private set; }

    [SerializeField] private BirdDatabase birdDatabase;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image skinImage;
    [SerializeField] private Button cancelBtn;
    [SerializeField] private Button applyBtn;


    public event EventHandler<OnSkinChangedEventArg> onSkinChanged;
    public class OnSkinChangedEventArg: EventArgs
    {
        public SpriteLibraryAsset spriteAsset;
    }

    public GameObject gameRoot => gameObject;
    public bool CanCloseWithback => true;


    private int skinIndex;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("SkinIndex"))
        {
            skinIndex = 0;
            skinImage.sprite = birdDatabase._Birds[skinIndex].birdSprite.GetSprite("Fly", "Fly 0");
        }
        else
        {
            skinIndex = PlayerPrefs.GetInt("SkinIndex");
            skinImage.sprite = birdDatabase._Birds[skinIndex].birdSprite.GetSprite("Fly", "Fly 0");
        }
        if (Instance != null && Instance != this)
        {

            Destroy(gameObject);
            return;

        }
        Instance = this;
    }
    void Start()
    {
        applyBtn.onClick.AddListener(() =>
        {
            HandleChangeSkin(skinIndex);
        });
        cancelBtn.onClick.AddListener(() =>
        {
            UIStackManager.Instance.Back();
        });
        SettingInventoryUI.Instance.onOpenSkinManager += SettingInventoryUI_onOpenSkinManager;
        OnClose();
    }
    private void OnDestroy()
    {
        if (SettingInventoryUI.Instance != null)
        {
            SettingInventoryUI.Instance.onOpenSkinManager -= SettingInventoryUI_onOpenSkinManager;
        }
        if (Instance == this)
        {
            Instance = null;
        }
        
    }
    private void SettingInventoryUI_onOpenSkinManager(object sender, System.EventArgs e)
    {
        UIStackManager.Instance.Open(this);
    }
    public void OnOpen()
    {
        this.gameObject.SetActive (true);
    }

    public void OnClose()
    {
        this.gameObject.SetActive (false);
    }

    public void SetInteractable(bool value)
    {
        canvasGroup.interactable = value;
        canvasGroup.blocksRaycasts = value;
    }

    private void NextOption()
    {
        skinIndex++;
        if (skinIndex >= birdDatabase._BirdsCount) {
            skinIndex = 0;
        }
        
        skinImage.sprite = birdDatabase._Birds[skinIndex].birdSprite.GetSprite("Fly","Fly 0");
    }

    private void PreviousOption()
    {
        skinIndex--;
        if (skinIndex < 0)
        {
            skinIndex = birdDatabase._BirdsCount-1;
        }

        skinImage.sprite = birdDatabase._Birds[skinIndex].birdSprite.GetSprite("Fly", "Fly 0");
    }
    public void HandleChangeSkin(int index)
    {
        var item = birdDatabase._Birds[index].birdSprite;
        onSkinChanged?.Invoke(this, new OnSkinChangedEventArg {
            spriteAsset = item
        });
        PlayerPrefs.SetInt("SkinIndex", index);
        UIStackManager.Instance.Back();
    }
}
