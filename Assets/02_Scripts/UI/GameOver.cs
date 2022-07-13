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

    private void Awake()
    {
        playerData = Resources.Load<Player_data>("SO/" + "PlayerData");
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

    private void ConnectUI()
    {
        CheckHighScore();

        scoreText.text = $"SCORE : {playerData.score}";
        highScoreText.text = $"HIGHSCORE : {playerData.bestScore}";
    }

    private void CheckHighScore()
    {
        var _score = playerData.score;
        var _highScore = playerData.bestScore;

        if (_score > _highScore)
        {
            NewRecord();
            playerData.bestScore = _score;
        }
    }

    private void NewRecord()
    {
        newRecordText.gameObject.SetActive(true);
        //ShakeText(1f, 30f, 30)
        StartCoroutine(ShakeText(1f, 30f, 30));
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
                UIManager.Instance.OnClickRestartGame();
                yield break;
            }

            currentTime -= 1;
            timeSlider.fillAmount = currentTime / maxTime;
            timeText.text = $"{currentTime}초 후 메뉴화면으로 돌아갑니다";

            yield return timeDelay;
        }
    }
}
