using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OptionCtrl : MonoBehaviour
{
    public GameObject panel;
    public GameObject spanel;
    void Awake()
    {
        panel.SetActive(false);
        spanel.SetActive(false);
    }

    //OptionPanel------------------------------------------------------
    public void OnClickOption()
    {
        Time.timeScale = 0;
        panel.SetActive(true);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickX()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }





    //SoundPanel---------------------------------------------------------------------
    public void OnClickSound()
    {
        spanel.SetActive(true);
    }

    public void OnClickSOnOff()
    {
    }

    public void SoundOption()
    {

    }





}
