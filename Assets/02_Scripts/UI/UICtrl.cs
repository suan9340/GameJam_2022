using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class UICtrl : MonoBehaviour
{
    private Player_data playerData = null;

    [Header("물음표창 유아이")]
    public Image questionObj = null;

    [Header("설정창 유아이")]
    public Image suljungObj = null;


    private bool isSettingChang = false;
    private bool isQuestionChang = false;

    private bool isFirstClick = false;

    private void Start()
    {
        playerData = Resources.Load<Player_data>("SO/" + "PlayerData");
        Application.targetFrameRate = 200;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isSettingChang)
                OnClickSettingChang();

            else if (isQuestionChang)
                OnClickQuestion();
            else
                OnClickSettingChang();
        }
    }

    /// <summary>
    /// 시작 버튼 눌렀을 떄
    /// </summary>
    public void OnClickStartButton()
    {
        if (playerData.isfirst)
        {
            isFirstClick = true;

            playerData.isfirst = false;
            OnClickQuestion();
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void OnClickExitButton()
    {
        Debug.Log("Game Exit");
        Application.Quit();
    }

    public void OnClickSettingChang()
    {
        isSettingChang = !isSettingChang;
        if (isSettingChang)
        {
            GameManager.Instance.SettingGameState(Game_State_Enum.isSetting);
            suljungObj.gameObject.SetActive(true);
            suljungObj.rectTransform.DOAnchorPosY(0, 1f).SetEase(Ease.OutCirc).SetUpdate(true);
        }
        else
        {
            suljungObj.rectTransform.DOAnchorPosY(1171f, 1f).SetEase(Ease.OutCirc).SetUpdate(true);
        }
    }


    public void OnClickQuestion()
    {
        if (playerData.isfirst)
        {
            isFirstClick = true;

            playerData.isfirst = false;
        }

            isQuestionChang = !isQuestionChang;
        if (isQuestionChang)
        {
            GameManager.Instance.SettingGameState(Game_State_Enum.isSetting);
            questionObj.gameObject.SetActive(true);
            questionObj.rectTransform.DOAnchorPosY(0, 1f).SetEase(Ease.OutCirc).SetUpdate(true);
        }
        else
        {
            if (isFirstClick)
            {
                SceneManager.LoadScene(1);
            }

            else
                questionObj.rectTransform.DOAnchorPosY(-1772f, 1.5f).SetEase(Ease.OutCirc).SetUpdate(true);
        }
    }
}
