using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    AudioSource AS;
    public Slider BGMSlider;
    public List<AudioClip> music = new List<AudioClip>();
    private float backvol = 1;
    void Start()
    {
        AS = this.GetComponent<AudioSource>();
            
        backvol = PlayerPrefs.GetFloat("backvol", 1f);
        BGMSlider.value = backvol;
        AS.volume = BGMSlider.value;
    }

    void Update()
    {
        SoundSlider();

        if(!AS.isPlaying)
        {
            AS.Play();
        }
    }

    public void SoundSlider()
    {
        AS.volume = BGMSlider.value;
        backvol = BGMSlider.value;
        PlayerPrefs.SetFloat("backvol", backvol);

    }

    public void RandomPlay()
    {
        AS.clip = music[Random.Range(0, music.Count)];
        AS.Play();
    }
}
