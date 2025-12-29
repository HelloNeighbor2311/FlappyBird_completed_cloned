using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStackManager : MonoBehaviour
{
    public static UIStackManager Instance;

    [SerializeField]private readonly Stack<IUI> stackUI = new();
    [Header("Pause Behavior")]
    [Tooltip("If true: if there was any UI in this stack, then set Time.timeScale = 0")]
    [SerializeField] private bool pauseWhenAnyUIOpen = true;
    [SerializeField] private PauseUI pauseUIInstance;
    [SerializeField] private SkinSettingUI skinSettingUIInstance;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }
    private void Start()
    {
        if(GameInput.instance != null) 
            GameInput.instance.onPauseAction += GameInput_onPauseAction;
    }

    private void GameInput_onPauseAction(object sender, System.EventArgs e)
    {
        //Instead of using "pauseUIInstance == PeekTop()" => it might caught a warning
        if (!HasAnyUIOpen())
        {
            Open(pauseUIInstance);
        }
        else
        {
            Back();
        }
    }
    private void OnDestroy()
    {
        if (GameInput.instance != null)
            GameInput.instance.onPauseAction -= GameInput_onPauseAction;
        if(Instance == this)
        {
            Instance = null;
        }
    }
    public void Open(IUI screen)
    {
        if(screen == null || screen.gameRoot == null)
        {
            Debug.LogWarning("screen/root is null");
            return;
        }
        if (PeekTop() == screen) return;
        if(stackUI.TryPeek(out var topScreen))
        {
            topScreen.SetInteractable(false);
        }
        stackUI.Push(screen); 
        screen.gameRoot.SetActive(true);
        screen.SetInteractable(true);
        screen.OnOpen();

        RefreshTimeScale();
    }

    public void Back()
    {
        if(stackUI.Count == 0)
        {
            return;
        }

        var topScreen = stackUI.Peek();
        if(topScreen == null)
        {
            stackUI.Pop();
            RefreshTimeScale();
            return;
        }
        if (!topScreen.CanCloseWithback) return;
        if(pauseUIInstance.isSettingPanelOpened)
        {
            pauseUIInstance.isSettingPanelOpened = false;

        }
        stackUI.Pop();
        topScreen.OnClose();

        if(topScreen.gameRoot != null)
        {
            topScreen.SetInteractable(false);
            topScreen.gameRoot.SetActive(false);
        }

        if(stackUI.TryPeek(out var underScreen) && underScreen != null)
        {
            underScreen.gameRoot?.SetActive(true);
            underScreen.SetInteractable(true);
        }
        RefreshTimeScale();
    }

    public void CloseAll()
    {
        while (stackUI.Count > 0)
        {
            var screen = stackUI.Pop();
            if (screen == null) continue;

            screen.OnClose();
            if (screen.gameRoot != null) {
                screen.SetInteractable(false);
                screen.gameRoot.SetActive(false);
            }
        }

        RefreshTimeScale();
    }

    public bool HasAnyUIOpen()
    {
        return stackUI.Count > 0;
    }

    public IUI PeekTop()
    {
        return stackUI.TryPeek(out var top) ? top : null;
    }

    public void RefreshTimeScale()
    {
        if (!pauseWhenAnyUIOpen)
            return;

        Time.timeScale = (stackUI.Count > 0) ? 0f : 1f;
    }
}
