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

    void Start()
    {
        backVol = PlayerPrefs.GetFloat("backvol", 1f);
        BGMSlider.value = backVol;
        BGM.volume = BGMSlider.value;
    }

    void Update()
    {
        BGMSoundSlider();

        if (!BGM.isPlaying)
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
        //BGM.clip = music[Random.Range(0, music.Count)];
        BGM.clip = musicInfo[Random.Range(0, musicInfo.Count)].clip;
        BGM.Play();
    }


    // ����� �Լ��� �� �����ϱ� �ʿ��Ѱ��� �����ؼ�  FX.Play();[����Ƽ ���� �Լ�] �ϸ� ����� 
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



