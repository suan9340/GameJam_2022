using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[System.Serializable]
public class MusicInfo
{
    public string name;
    public AudioClip clip;
}

[System.Serializable]
public class VFXInfo
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    #region Singleton
    private static AudioManager _instance = null;

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
    #endregion

    public AudioSource BGM;
    public AudioSource FX;
    public Slider MasterSlider;
    public Slider BGMSlider;
    public Slider FXSlider;

    [Header("Infos")]
    public List<MusicInfo> musicInfo = new List<MusicInfo>();
    public List<VFXInfo> vFXInfos = new List<VFXInfo>();

    private float backVol = 1;
    private float vfxVol = 1;

    private void Start()
    {
        SetVolume();
    }

    void Update()
    {
        if (!BGM.isPlaying)
        {
            RandomPlay();
        }
    }

    private void SetVolume()
    {
        backVol = PlayerPrefs.GetFloat(ConstantManager.VOL_BACK, 1f);
        BGMSlider.value = backVol;

        vfxVol = PlayerPrefs.GetFloat(ConstantManager.VOL_VFX, 1f);
        FXSlider.value = vfxVol;
    }

    public void MasterSoundSlider()
    {
        //BGM.volume = MasterSlider.value;
        ////FX.volume = MasterSlider.value;

        //backVol = BGMSlider.value;
        ////backVol = FXSlider.value;
        //PlayerPrefs.SetFloat("backvol", backVol);

        BGM.volume = MasterSlider.value;
        FX.volume = MasterSlider.value;

        backVol = MasterSlider.value;
        vfxVol = MasterSlider.value;

        PlayerPrefs.SetFloat(ConstantManager.VOL_BACK, backVol);
        PlayerPrefs.SetFloat(ConstantManager.VOL_VFX, vfxVol);
    }

    public void BGMSoundSlider()
    {
        BGM.volume = BGMSlider.value;
        backVol = BGMSlider.value;
        PlayerPrefs.SetFloat(ConstantManager.VOL_BACK, backVol);

    }
    public void FXSoundSlider()
    {
        FX.volume = FXSlider.value;
        vfxVol = FXSlider.value;
        PlayerPrefs.SetFloat(ConstantManager.VOL_VFX, vfxVol);
    }

    public void RandomPlay()
    {
        //BGM.clip = music[Random.Range(0, music.Count)];
        BGM.clip = musicInfo[Random.Range(0, musicInfo.Count)].clip;
        BGM.Play();
    }


    // 오디오 함수에 들어가 있으니까 필요한곳에 참조해서  FX.Play();[유니티 내장 함수] 하면 실행됨 
    public void EnemyDie()
    {
        FX.clip = vFXInfos[0].clip;
        FX.Play();
    }


    public void ShootGun()
    {
        FX.clip = vFXInfos[1].clip;
        FX.Play();
    }

    public void PlayerDie()
    {
        FX.clip = vFXInfos[2].clip;
        FX.Play();
    }

    public void ItemEat()
    {
        FX.clip = vFXInfos[3].clip;
        FX.Play();
    }

    public void ClickButton()
    {
        FX.clip = vFXInfos[4].clip;
        FX.Play();
    }

}



