using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SOUNDTYPE {SWIPE= 0, PLACEOBJ, SENT, LOADER, GROUND, CLICK, ERROR, DELETE, DIALOG, TOASTIN, TOASTOUT, RECEIVED, NONE };

public class SoundManager : MonoBehaviour {
    public static SoundManager instance;

    public AudioSource source;

    [SerializeField]
    AudioClip[] clips;

    void Awake()
    {

        instance = this;

    }


    public void Play(SOUNDTYPE soundtype)
    {
        source.Stop();
        source.PlayOneShot(clips[(int)soundtype]);

    }

    public void Stop()
    {
        source.Stop();
    }

  
}
