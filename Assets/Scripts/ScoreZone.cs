using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreZone : MonoBehaviour
{

    [SerializeField] int score;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(score);
            SoundManager.PlaySound(SoundType.Point);
        }
    }
}
