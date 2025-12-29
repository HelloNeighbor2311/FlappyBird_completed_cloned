using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button playBtn;


    private bool isInventoryPanelOpened = false;


    private void Start()
    {
        playBtn.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.SceneEnum.GamePlayScene);
            Time.timeScale = 1.0f;
        });
        settingBtn.onClick.AddListener(() => {
            HandleSettingBtn();
        });
    }


    public void HandleSettingBtn()
    {
        if (isInventoryPanelOpened)
        {
            animator.SetTrigger("CloseCall");
            isInventoryPanelOpened = false;
        }
        else
        {
            animator.SetTrigger("ShowCall");
            isInventoryPanelOpened = true;
        }
    }
}
