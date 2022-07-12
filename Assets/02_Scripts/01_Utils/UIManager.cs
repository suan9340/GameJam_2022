using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region SingleTon   

    private static UIManager _instance = null;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("UIManager").AddComponent<UIManager>();
                }
            }
            return _instance;
        }
    }

    #endregion
    public static readonly WaitForSecondsRealtime readyDelay = new WaitForSecondsRealtime(1f);

    private bool isSettingChang = false;

    [Header("게임 카운트다운 TEXT")] public Text countDownText;

    [Header("설정창 UI")]
    public GameObject mainSettngUI = null;

    private void Update()
    {
        InputKey();
    }

    private void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClickSettingChang();
        }
    }

    /// <summary>
    /// 화면상에 있는 설정창 눌렀을 때 or ESC 눌렀을 때
    /// </summary>
    public void OnClickSettingChang()
    {
        isSettingChang = !isSettingChang;
        if (isSettingChang)
        {
            GameManager.Instance.gameState = Game_State_Enum.isSetting;
            mainSettngUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            StartCoroutine(CountDownReadyGame());
            mainSettngUI.SetActive(false);
        }
    }

    private IEnumerator CountDownReadyGame()
    {
        countDownText.text = $"3";
        yield return readyDelay;

        countDownText.text = $"2";
        yield return readyDelay;

        countDownText.text = $"1";
        yield return readyDelay;

        countDownText.text = $" ";

        Time.timeScale = 1f;
    }
}
