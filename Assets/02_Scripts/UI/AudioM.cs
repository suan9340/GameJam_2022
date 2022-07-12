using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioM : MonoBehaviour
{

    public AudioMixer BGMMixer;
    public AudioMixer MasterMixer;
    public AudioMixer SFXMixer;
    public Slider audioSlider;

   //public void SetLevel(float sliderval)
   // {
   //     BGMMixer.SetFloat("BGM",Mathf.Log10(sliderval)*20);
   // }

    public void BGMControl()
    {
        float sound = audioSlider.value;
        if (sound == -40f) BGMMixer.SetFloat("BGM", -80);
        else BGMMixer.SetFloat("BGM", sound);
    }

    public void MasterControl()
    {
        float sound = audioSlider.value;
        if (sound == -40f) MasterMixer.SetFloat("Master", -80);
        else MasterMixer.SetFloat("Master", sound);
    
    }  
    
    public void SFXControl()
    {
        float sound = audioSlider.value;
        if (sound == -40f) SFXMixer.SetFloat("SFX", -80);
        else SFXMixer.SetFloat("SFX", sound);
    
    }
}
