using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Player : MonoBehaviour
{
    [SerializeField] AudioSource audiosourcee;
    [SerializeField] private static Music_Player playerInstance;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            Object.Destroy(gameObject);
        }
    }

    public void changeVolume(float volume)
    {
        audiosourcee.volume = volume;
    }
}
