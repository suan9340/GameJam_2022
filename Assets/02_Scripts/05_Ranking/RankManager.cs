using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    [Header("InputField ������Ʈ")]
    public InputField nameInputField = null;
    public InputField scoreInputField = null;

    private string playerName = "";
    private string playerScore;

    [Header("Text")]
    public Text currentScoreTxt = null;
    public Text currentNameTxt = null;

    private void Update()
    {
        InputKey();
    }

    public void InputKey()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            OnClickRankSet();
        }
    }

    /// <summary>
    /// ��ŷ ��� ��ư ������ ��
    /// </summary>
    public void OnClickRankSet()
    {
        if (nameInputField.text == null || nameInputField.text == "")
        {
            Debug.Log("�̸��� �Է����ּ���!");
            return;
        }
        else
        {
            playerName = nameInputField.text;
            PlayerPrefs.SetString(ConstantManager.RANK_PL_NAME, playerName);

            Debug.Log(playerName);
        }
    }

    public void ShowRanking()
    {

    }
}
