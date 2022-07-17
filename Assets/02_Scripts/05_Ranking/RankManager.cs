using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    [Header("InputField 오브젝트")]
    public InputField nameInputField = null;
    public InputField scoreInputField = null;

    private string name;
    private int score;

    /// <summary>
    /// Name InputField 값을 가져오는 함수
    /// </summary>
    public void GetNameValue(Text _text)
    {
        name = _text.text;
        //Debug.Log($"입력 이름 값 : {name}");


        _text.text = nameInputField.text;
    }


    /// <summary>
    /// Score InputField 값을 가져오는 함수
    /// </summary>
    public void GetScoreInputField(Text _text)
    {
        score = int.Parse(_text.text);
        //Debug.Log($"입력 점수 값 : {score}");

        _text.text = scoreInputField.text;
    }

    /// <summary>
    /// 랭킹 등록 버튼 눌렀을 때
    /// </summary>
    public void OnClickRankSet()
    {
        Debug.Log($"이름 : {name} , 점수 {score}");
    }
}
