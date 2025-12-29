using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePrefabs : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] private float timer = 0f;
    private void Start()
    {
        Player.Instance.onGameOver += Player_onGameOver;
    }

    private void Player_onGameOver(object sender, System.EventArgs e)
    {
        speed = 0;
    }

    private void Update()
    {
        transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
        timer += Time.deltaTime;
        if (timer > 10f)
        {
            SimplePool2.Despawn(gameObject);
            timer = 0f;
        }
        if(speed <= 4f)
        {
            if (GameManager.Instance.difficultyTimer >= 100f)
            {
                speed += 0.1f;
            }
        }
    }
}
