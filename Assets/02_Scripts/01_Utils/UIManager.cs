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

    [Header("���� ī��Ʈ�ٿ� TEXT")] public Text countDownText;

    [Header("����â UI")]
    public Image mainSettngUI = null;

    private bool isCount = false;

    private void Update()
    {
        InputKey();
    }

    private void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isCount) return;
            OnClickSettingChang();
        }
    }

    /// <summary>
    /// ȭ��� �ִ� ����â ������ �� or ESC ������ ��
    /// </summary>
    public void OnClickSettingChang()
    {
        isSettingChang = !isSettingChang;
        if (isSettingChang)
        {
            GameManager.Instance.gameState = Game_State_Enum.isSetting;
            mainSettngUI.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            mainSettngUI.gameObject.SetActive(false);
            StartCoroutine(CountDownReadyGame());
        }
    }

    private IEnumerator CountDownReadyGame()
    {
        isCount = true;
        countDownText.text = $"3";
        yield return readyDelay;

        countDownText.text = $"2";
        yield return readyDelay;

        countDownText.text = $"1";
        yield return readyDelay;

        countDownText.text = $" ";

        isCount = false;    
        Time.timeScale = 1f;
    }
}
