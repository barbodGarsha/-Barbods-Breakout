using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioSource audio_source;
    public AudioClip brick_distroying, ball_hitting;

    private static SoundEffects _instance;
    public static SoundEffects instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void brick_destroyed()
    {
        audio_source.PlayOneShot(brick_distroying);
    }
    public void ball_hit()
    {
        audio_source.PlayOneShot(ball_hitting);
    }

}
