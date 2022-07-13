using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    #region Singleton
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("AudioManager").AddComponent<AudioManager>();
                }
            }
            return _instance;
        }
    }
    private static AudioManager _instance = null;

    public AudioSource BGM;
    public AudioSource FX;
    public Slider MasterSlider;
    public Slider BGMSlider;
    public Slider FXSlider;
    public List<AudioClip> music = new List<AudioClip>();
    public List<AudioClip> vfx = new List<AudioClip>();
    private float backVol = 1;
    #endregion



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
        PlayerPrefs.SetFloat("backvol", backVol);
    }

    public void BGMSoundSlider()
    {
        BGM.volume = BGMSlider.value;
        backVol = BGMSlider.value;
        PlayerPrefs.SetFloat("backvol", backVol);

    }
    public void FXSoundSlider()
    {
        FX.volume = FXSlider.value;
        backVol = FXSlider.value;
        PlayerPrefs.SetFloat("backvol", backVol);
    }
    public void RandomPlay()
    {
        BGM.clip = music[Random.Range(0, music.Count)];
        BGM.Play();
    }


    // 오디오 함수에 들어가 있으니까 필요한곳에 참조해서  FX.Play();[유니티 내장 함수] 하면 실행됨 
    public void EnemyDieSound()
    {
        FX.clip = vfx[0];
        FX.Play();
    }


    public void GunSound()
    {
        FX.clip = vfx[1];
        FX.Play();
        Debug.Log("뿡뿡띠");
    }

    public void PlayerDieSound()
    {
        FX.clip = vfx[2];
        FX.Play();
    }
    
    public void ItemAcquisition()
    {
        FX.clip = vfx[3];
        FX.Play();
    }

    public void OnClickButton()
    {
        FX.clip = vfx[4];
        FX.Play();
    }

    

}
   


