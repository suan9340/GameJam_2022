using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer BGMMixer;
    public AudioMixer MasterMixer;
    public AudioMixer SFXMixer;
    public Slider BGMSlider;
    public Slider MasterSlider;
    public Slider SFXSlider;

  
    public void MasterControl()
    {
        float sound = MasterSlider.value;
        if (sound == -40f) MasterMixer.SetFloat("Master", -80);
        else MasterMixer.SetFloat("Master", sound);
    
    }  

    public void BGMControl()
    {
        float sound = BGMSlider.value;
        if (sound == -40f) BGMMixer.SetFloat("BGM", -80);
        else BGMMixer.SetFloat("BGM", sound);
    }

    
    public void SFXControl()
    {
        float sound = SFXSlider.value;
        if (sound == -40f) SFXMixer.SetFloat("SFX", -80);
        else SFXMixer.SetFloat("SFX", sound);
    
    }
}
