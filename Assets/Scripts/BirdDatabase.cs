using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BirdDatabase : ScriptableObject
{
    public Bird[] _Birds;

    public int _BirdsCount
    {
        get
        {
            return _Birds.Length;
        }
    }

    public Bird getBird(int index)
    {
        return _Birds[index];
    }
}
