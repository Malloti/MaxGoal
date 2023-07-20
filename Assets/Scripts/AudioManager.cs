using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip[] clips;
    public AudioSource backgroundMusic;

    public AudioClip[] fxClips;
    public AudioSource fxSound;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!backgroundMusic.isPlaying) {
            backgroundMusic.clip = GetRandom();
            backgroundMusic.Play();
        }
    }

    AudioClip GetRandom()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    public void PlaySoundEffect(int index)
    {
        fxSound.clip = fxClips[index];
        fxSound.Play();
    }
}
