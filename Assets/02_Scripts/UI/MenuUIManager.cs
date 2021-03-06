using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    private Player_data playerData = null;

    [Header("물음표창 유아이")]
    public Image questionObj = null;

    [Header("설정창 유아이")]
    public Image suljungObj = null;


    private bool isSettingChang = false;
    private bool isQuestionChang = false;

    private int isFirstVisit;
    private bool isClickStart = false;

    private void Start()
    {
        isFirstVisit = PlayerPrefs.GetInt(ConstantManager.DATA_VISIT, 0);
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
        AudioManager.Instance.Sound_ClickButton();

        if (isFirstVisit == 0)
        {
            isClickStart = true;
            OnClickQuestion();
        }
        else
        {
            SceneManager.LoadScene(1);
        }

    }


    /// <summary>
    /// 게임 종료 버튼
    /// </summary>
    public void OnClickExitButton()
    {
        AudioManager.Instance.Sound_ClickButton();
        Debug.Log("Game Exit");
        Application.Quit();
    }


    /// <summary>
    /// 설정 창 눌렀을 때
    /// </summary>
    public void OnClickSettingChang()
    {
        AudioManager.Instance.Sound_ClickButton();
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


    /// <summary>
    /// ?표 창 눌렀을 때
    /// </summary>
    public void OnClickQuestion()
    {
        if (isFirstVisit == 0)
        {
            isFirstVisit = 1;
            PlayerPrefs.SetInt(ConstantManager.DATA_VISIT, isFirstVisit);
        }

        AudioManager.Instance.Sound_ClickButton();
        isQuestionChang = !isQuestionChang;
        if (isQuestionChang)
        {
            GameManager.Instance.SettingGameState(Game_State_Enum.isSetting);
            questionObj.gameObject.SetActive(true);
            questionObj.rectTransform.DOAnchorPosY(0, 1f).SetEase(Ease.OutCirc).SetUpdate(true);
        }
        else
        {
            if (isClickStart)
            {
                isClickStart = false;
                SceneManager.LoadScene(1);
            }

            else
                questionObj.rectTransform.DOAnchorPosY(-1772f, 1.5f).SetEase(Ease.OutCirc).SetUpdate(true);

        }
    }
}
