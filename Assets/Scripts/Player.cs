using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D.Animation;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public event EventHandler onGameOver;
    [SerializeField] private SpriteLibrary skinAsset;

    private const string TRIGGER = "Trigger";

    private Rigidbody2D rb;
    private Animator animator;
    private bool isDead = false;
    private const float JUMP_FORCE = 4f;
    private float rotationSpeed = 8f;


    private void Awake()
    {
        if (Instance != null && Instance == this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        GameManager.Instance.onStateChanged += GameManger_onStateChanged;
        SkinSettingUI.Instance.onSkinChanged += SkinSettingUI_onSkinChanged;
        SkinSettingUI.Instance.HandleChangeSkin(PlayerPrefs.GetInt("SkinIndex"));
    }
    private void OnDestroy()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.onStateChanged -= GameManger_onStateChanged;
        }
        if(SkinSettingUI.Instance != null)
        {
            SkinSettingUI.Instance.onSkinChanged -= SkinSettingUI_onSkinChanged;
        }
        if(Instance == this)
        {
            Instance = null;
        }
        
    }
    private void SkinSettingUI_onSkinChanged(object sender, SkinSettingUI.OnSkinChangedEventArg e)
    {
        skinAsset.spriteLibraryAsset = e.spriteAsset;
    }

    private void GameManger_onStateChanged(GameState obj)
    {
        if(obj == GameState.Ready)
        {
            rb.gravityScale = 0f;
        }
        if(obj == GameState.Gameplay)
        {
            rb.gravityScale = 1f;
        }
    }

    private void Update()
    {

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.W)) { 
            rb.velocity = Vector2.up * JUMP_FORCE;
            animator.SetTrigger(TRIGGER);
            SoundManager.PlaySound(SoundType.Wing, 0.7f);
        }
    }
    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, rb.velocity.y * rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null) { 
            if(isDead) 
            {
                return;
            }
            isDead = true;
            GetComponent<Collider2D>().enabled = false;
            rb.simulated = false;
            GameManager.Instance.ChangeState(GameState.Gameover);
            animator.Play("Die");
            SoundManager.PlaySound(SoundType.Hit);
            onGameOver?.Invoke(this, EventArgs.Empty);  
        }
    }
    public void setRigidbody(float scale)
    {
        rb.gravityScale = scale;
    }

}
