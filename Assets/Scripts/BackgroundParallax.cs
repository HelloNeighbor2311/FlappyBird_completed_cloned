using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    float floatSpeed = 0;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        Player.Instance.onGameOver += Player_onGameOver;
        GameManager.Instance.onStateChanged += GameManager_onStateChanged; 
    }
    private void GameManager_onStateChanged(GameState obj)
    {
        if(obj == GameState.Ready)
        {
            floatSpeed = 0;
        }
        if(obj == GameState.Gameplay)
        {
            floatSpeed = 0.06f;
        }
    }
    private void Player_onGameOver(object sender, System.EventArgs e)
    {
        floatSpeed = 0;
    }

    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(floatSpeed * Time.deltaTime, 0);
    }
}
