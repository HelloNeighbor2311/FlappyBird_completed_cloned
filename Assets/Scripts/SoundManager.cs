using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SoundType
{
    Wing,
    Die,
    Point,
    Hit
}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    private const string SAVED_VOLUME = "SavedVolume";
    public static SoundManager instance;
    [SerializeField]private AudioSource audioSource;

    [SerializeField] private SoundList[] soundLists;

    private void Awake()
    {
        if(instance != null && instance == this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public static void PlaySound(SoundType sound, float volume = 1f)
    {
        AudioClip[] clips = instance.soundLists[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.audioSource.PlayOneShot(randomClip, volume);
    }
    public AudioSource GetAudioSource()
    {
        return audioSource;
    }
#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundLists, names.Length);
        for (int i = 0; i < soundLists.Length; i++) {
            soundLists[i].name = names[i];
            
        }
    }
#endif
}

[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sounds; }
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sounds;
}

