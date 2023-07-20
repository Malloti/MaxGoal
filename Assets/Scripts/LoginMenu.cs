using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginMenu : MonoBehaviour
{
    public Sprite soundOn, soundOff;

    private Animator configAnim;
    private bool configHide;
    private AudioSource audioSource;
    private Button btnSound;

    private void Start()
    {
        configAnim = GameObject.FindGameObjectWithTag("Configurations").GetComponent<Animator>();
        audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        btnSound = GameObject.Find("btnSound").GetComponent<Button>();
    }

    public void Play()
    {
        LevelManager.LevelLoad(1);
    }

    public void Configurations()
    {
        if (configHide) {
            configAnim.Play("modalConfigShow");
            configHide = false;
        } else {
            configAnim.Play("modalConfigHide");
            configHide = true;
        }
    }

    public void SoundOnOff()
    {
        audioSource.mute = !audioSource.mute;
        
        if (audioSource.mute) {
            btnSound.image.sprite = soundOff;
        } else {
            btnSound.image.sprite = soundOn;
        }
    }

    public void OpenFacebook()
    {
        Application.OpenURL("www.google.com.br");
    }
}
