using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicManager : MonoBehaviour
{
    private static MusicManager Music;

    public static MusicManager instance
    {
        get
        {
            if (Music == null)
            {
                Music = GameObject.FindObjectOfType<MusicManager>();
                DontDestroyOnLoad(Music.gameObject);
            }
            return Music;
        }
    }

    void Awake()
    {
        if (Music == null)
        {
            Music = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != Music)
                Destroy(Music.gameObject);
        }
    }
}
