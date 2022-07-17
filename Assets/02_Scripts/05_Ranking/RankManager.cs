using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    [Header("InputField ������Ʈ")]
    public InputField nameInputField = null;
    public InputField scoreInputField = null;

    private string name;
    private int score;

    /// <summary>
    /// Name InputField ���� �������� �Լ�
    /// </summary>
    public void GetNameValue(Text _text)
    {
        name = _text.text;
        //Debug.Log($"�Է� �̸� �� : {name}");


        _text.text = nameInputField.text;
    }


    /// <summary>
    /// Score InputField ���� �������� �Լ�
    /// </summary>
    public void GetScoreInputField(Text _text)
    {
        score = int.Parse(_text.text);
        //Debug.Log($"�Է� ���� �� : {score}");

        _text.text = scoreInputField.text;
    }

    /// <summary>
    /// ��ŷ ��� ��ư ������ ��
    /// </summary>
    public void OnClickRankSet()
    {
        Debug.Log($"�̸� : {name} , ���� {score}");
    }
}
