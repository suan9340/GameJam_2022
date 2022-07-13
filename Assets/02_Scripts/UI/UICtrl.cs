using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class UICtrl : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    public void OnClickStartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }
}
