using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    Ready,
    Gameplay,
    Gameover,
    GamePaused
}
public class GameManager : MonoBehaviour
{
    public const float GRAVITY_NORMAL = 1.0f;
    public static GameManager Instance; 

    //GameState
    public GameState state { get; private set; } = GameState.Ready;
    public GameState stateBeforePause = GameState.Ready;


    //Timer
    public float GameTimer = 0;
    public float difficultyTimer { get; private set; } = 0f;

    public int gameScore = 0;

    public Action<GameState> onStateChanged;
    public event EventHandler onScoring;

    [Header("Spawn Pipes")]
    [SerializeField] GameObject greenPipePrefabs;
    [SerializeField] GameObject redPipePrefabs;


    [SerializeField]private float timer = 0;
    private float timeToSpawnPipe = 2f;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if(GameInput.instance != null)
            GameInput.instance.onPauseAction += GameInput_onPauseAction;

        ChangeState(GameState.Ready);
        Debug.Log(Time.timeScale);
    }
    private void OnDestroy()
    {
        if(GameInput.instance != null)
        {
            GameInput.instance.onPauseAction -= GameInput_onPauseAction;
        }
        if(Instance == this)
        {
            Instance = null;
        }
    }
    private void GameInput_onPauseAction(object sender, EventArgs e)
    {
        if(state != GameState.GamePaused)
        {
            stateBeforePause = state;
            state = GameState.GamePaused;
        }
        else
        {
            state = stateBeforePause;
        }
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.Ready:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.W))
                {
                    ChangeState(GameState.Gameplay);
                }
                break;
            case GameState.Gameplay:
                TickGameplay();
                break;
            case GameState.GamePaused:

                break;
            case GameState.Gameover:
                
                break;
        }
        
        
    }
    private void TickGameplay()
    {
        GameTimer += Time.deltaTime;

        timer += Time.deltaTime;
        if (timer >= timeToSpawnPipe)
        {
            timer = 0;
            SpawnPipe();

        }

        difficultyTimer += Time.deltaTime;
        if(difficultyTimer >= 100f)
        {
            difficultyTimer = 0f;
            timeToSpawnPipe = Mathf.Max(0.6f, timeToSpawnPipe - 0.2f);
        }
    }
    private void SpawnPipe()
    {
        float pipeHeight = UnityEngine.Random.Range(-2f, -5f);
        SimplePool2.Spawn(greenPipePrefabs, transform.position + new Vector3(0, pipeHeight, 0), Quaternion.identity);
    }
    public void AddScore(int score)
    {
        gameScore += score;
        onScoring?.Invoke(this, EventArgs.Empty);
    }
    public void ChangeState(GameState newState)
    {
        state = newState;
        onStateChanged?.Invoke(state);
    }
   
}
