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

    [Header("AudioSources")]
    public AudioSource BGM;
    public AudioSource FX;
    public AudioSource FX2;
    public AudioSource FX3;


    public Slider BGMSlider;
    public Slider FXSlider;

    [Header("Infos")]
    public List<MusicInfo> musicInfo = new List<MusicInfo>();
    public List<VFXInfo> vFXInfos = new List<VFXInfo>();

    private float backVol = 1;
    private float vfxVol = 1;

    public bool isBGMStop = false;
    public bool isFxStop = false;
    private void Start()
    {
        SetVolume();
    }


    private void SetVolume()
    {
        backVol = PlayerPrefs.GetFloat(ConstantManager.VOL_BACK, 1f);
        BGMSlider.value = backVol;

        vfxVol = PlayerPrefs.GetFloat(ConstantManager.VOL_VFX, 1f);
        FXSlider.value = vfxVol;
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
        FX2.volume = FXSlider.value;
        FX3.volume = FXSlider.value;

        vfxVol = FXSlider.value;
        PlayerPrefs.SetFloat(ConstantManager.VOL_VFX, vfxVol);


    }

    public void RandomPlay()
    {
        //BGM.clip = music[Random.Range(0, music.Count)];
        if (musicInfo.Count <= 0) return;

        BGM.clip = musicInfo[Random.Range(0, musicInfo.Count)].clip;
        BGM.volume = BGMSlider.value;
        BGM.Play();
    }


    // ����� �Լ��� �� �����ϱ� �ʿ��Ѱ��� �����ؼ�  FX.Play();[����Ƽ ���� �Լ�] �ϸ� ����� 
    public void EnemyDie()
    {
        FX2.clip = vFXInfos[0].clip;
        FX2.Play();
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
        FX3.clip = vFXInfos[3].clip;
        FX3.Play();
    }

    public void ClickButton()
    {
        FX.clip = vFXInfos[4].clip;
        FX.Play();
    }

    public void OnClickStart()
    {
        BGM.Stop();
    }

    public void ItemEatSound(bool _ischeck)
    {
        if (_ischeck)
        {
            BGM.pitch = 1.08f;
        }
        else
        {
            BGM.pitch = 1f;
        }
    }
}



