using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSFx audioSFx;

    public AudioPlayer audioPlayer;

    public static AudioManager instance;

    void Awake()
    {
        {
            if (instance == null)
            {
                instance = this;
            }
        }
    }
    public void PlayCoinPickupSound(GameObject obj)
    {
        AudioSource.PlayClipAtPoint(audioSFx.coinPickup, obj.transform.position);
    }

    public void PlayJumpSound(GameObject obj)
    {
        AudioSource.PlayClipAtPoint(audioPlayer.jump, obj.transform.position);
    }
}
