using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
    private Player_data playerData = null;

    [Header("SCORE TEXT")]
    public Text scoreText = null;

    [Header("HIGHSCORE TEXT")]
    public Text highScoreText = null;

    [Header("new Record")]
    public Text newRecordText = null;

    [Header("Time Slider")]
    public Image timeSlider = null;

    [Header("Time Text")]
    public Text timeText = null;

    public static readonly WaitForSecondsRealtime timeDelay = new WaitForSecondsRealtime(1f);

    private readonly int maxTime = 30;
    public int currentTime;

    private int highScore;

    private void Awake()
    {
        playerData = Resources.Load<Player_data>("SO/" + "PlayerData");
        highScore = PlayerPrefs.GetInt(ConstantManager.DATA_HIGHSCORE, 0);
        currentTime = maxTime;
    }

    private void Start()
    {
        StartCoroutine(UpdateTime());
    }

    private void OnEnable()
    {
        ConnectUI();
    }

    private void OnDisable()
    {
        playerData.score = 0;
        playerData.playerlevel = 0;
    }

    private void ConnectUI()
    {
        CheckHighScore();

        scoreText.text = $"SCORE : {playerData.score}";
        highScoreText.text = $"HIGHSCORE : {PlayerPrefs.GetInt(ConstantManager.DATA_HIGHSCORE, 500)}";
    }

    private void CheckHighScore()
    {
        var _score = playerData.score;

        if (_score > highScore)
        {
            Debug.Log("최고기록 갱신!");
            NewRecord();
            highScore = (int)_score;
            PlayerPrefs.SetInt(ConstantManager.DATA_HIGHSCORE, highScore);
        }
    }

    private void NewRecord()
    {
        newRecordText.gameObject.SetActive(true);
        Debug.Log("qwe");
        StartCoroutine(ShakeText(2f, 15f, 18));
    }

    private IEnumerator ShakeText(float _dur, float _str, int _vib)
    {
        yield return new WaitForSeconds(2f);
        newRecordText.rectTransform.DOShakeRotation(_dur, _str, _vib);
    }

    private IEnumerator UpdateTime()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            if (currentTime <= 0)
            {
                UIManager.Instance.OnClickGoToMenu();
                yield break;
            }

            currentTime -= 1;
            timeSlider.fillAmount = currentTime / maxTime;
            timeText.text = $"{currentTime}초 후 메뉴화면으로 돌아갑니다";

            yield return timeDelay;
        }
    }
}
