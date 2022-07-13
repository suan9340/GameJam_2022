using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGM;
    public AudioSource FX;
    public Slider MasterSlider;
    public Slider BGMSlider;
    public Slider FXSlider;
    public List<AudioClip> music = new List<AudioClip>();
    private float backVol = 1;
    void Start()
    {
            
        backVol = PlayerPrefs.GetFloat("backvol", 1f);
        BGMSlider.value = backVol;
        BGM.volume = BGMSlider.value;
    }

    void Update()
    {
        BGMSoundSlider();

        if(!BGM.isPlaying)
        {
            RandomPlay();
        }
    }
    public void MasterSoundSlider()
    {
        BGM.volume = MasterSlider.value;
        FX.volume = MasterSlider.value;
        PlayerPrefs.SetFloat("master", backVol);
    }

    public void BGMSoundSlider()
    {
        BGM.volume = BGMSlider.value;
        backVol = BGMSlider.value;
        PlayerPrefs.SetFloat("bgmBackVol", backVol);

    }
    public void FXSoundSlider()
    {
        FX.volume = FXSlider.value;
        backVol = FXSlider.value;
        PlayerPrefs.SetFloat("fxBackVol", backVol);
    }
    public void RandomPlay()
    {
        BGM.clip = music[Random.Range(0, music.Count)];
        BGM.Play();
    }
}
