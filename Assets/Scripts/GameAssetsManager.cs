using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssetsManager : MonoBehaviour
{

    public static GameAssetsManager instance;

    public Transform groundTransform;
    public Transform backgroundTransform;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

}
