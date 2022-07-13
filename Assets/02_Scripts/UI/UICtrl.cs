using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class UICtrl : MonoBehaviour
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene(1);
    }
}
