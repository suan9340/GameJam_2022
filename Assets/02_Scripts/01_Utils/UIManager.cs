using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool isSettingChang = false;
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
    /// ȭ��� �ִ� ����â ������ �� or ESC ������ ��
    /// </summary>
    public void OnClickSettingChang()
    {
        GameManager.Instance.gameState = Game_State_Enum.isSetting;
        Time.timeScale = 0f;
    }


}
