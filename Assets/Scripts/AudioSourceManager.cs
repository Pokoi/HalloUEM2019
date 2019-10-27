using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
       #region
    private static AudioSourceManager instance = null;


    public static AudioSourceManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    public AudioClip shoot;
    public AudioClip priest;
    public AudioClip explosion;
    public AudioClip deadEnemy;

    public AudioClip wc;



    public AudioSource source;

    public AudioClip clip;
    
}
