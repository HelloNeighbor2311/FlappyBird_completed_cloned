using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour, IUI
{
    [SerializeField]private Animator inventoryAnimator;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button mainMenuBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Slider volumeSlider;
    private const string SAVED_VOLUME = "SavedVolume";

    public bool isSettingPanelOpened = false;

    public GameObject gameRoot => gameObject;

    public bool CanCloseWithback => true;

    private void Start()
    {
        gameObject.SetActive(false);
        mainMenuBtn.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.SceneEnum.MainMenu);
        });
        continueBtn.onClick.AddListener(() =>
        {
            UIStackManager.Instance.Back();
        });
        settingBtn.onClick.AddListener(() => {
            HandleSettingBtn();
        });
        
        volumeSlider.value = PlayerPrefs.GetFloat(SAVED_VOLUME);
        SoundManager.instance.GetAudioSource().volume = volumeSlider.value;
    }

    public void SetVolume(float volume)
    {
        volume = volumeSlider.value;
        SoundManager.instance.GetAudioSource().volume = volume;
        PlayerPrefs.SetFloat(SAVED_VOLUME, volume);
    }
    public void OnOpen()
    {
        this.gameObject.SetActive(true);
    }

    public void OnClose()
    {
        this.gameObject.SetActive(false);
    }

    public void SetInteractable(bool value)
    {
        canvasGroup.interactable = value;
        canvasGroup.blocksRaycasts = value;
    }
    private void HandleSettingBtn()
    {
        if (!isSettingPanelOpened) {
            inventoryAnimator.SetTrigger("ShowCall");
            isSettingPanelOpened = true;
        }
        else
        {
            inventoryAnimator.SetTrigger("CloseCall");
            isSettingPanelOpened = false;
        }
    }
}
