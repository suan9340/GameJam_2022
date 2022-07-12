using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class UICtrl: MonoBehaviour
{

    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClickOption()
    {
    }
    public void OnClickStartButton()
    {
        SceneManager.LoadScene(0);
    }
}
