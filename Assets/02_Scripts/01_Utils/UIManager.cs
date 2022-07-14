using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
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
    private bool isQuestionChang = false;

    [Header("게임 카운트다운 TEXT")] public Text countDownText;

    [Header("설정창 UI")]
    public Image mainSettngImage = null;

    [Header("물음표창 UI")]
    public Image questionImage = null;

    [Header("물음표창 스크롤")]
    public GameObject scrollCotent = null;

    [Header("적 죽였을 때 테두리 UI")]
    public Image hitEnemyOutImage = null;
    private bool isCount = false;

    [Header("---------인게임 오브젝트---------")]
    public GameObject ingameObj = null;

    [Header("---------게임오버 오브젝트---------")]
    public GameObject gameOverObj = null;
    public Ease ease;

    private void Start()
    {
        SetResolution();

        StartCoroutine(CountDownReadyGame());
    }

    private void Update()
    {
        InputKey();
    }

    private void InputKey()
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
    /// 화면상에 있는 설정창 눌렀을 때 or ESC 눌렀을 때
    /// </summary>
    public void OnClickSettingChang()
    {
        if (isCount) return;

        isSettingChang = !isSettingChang;
        if (isSettingChang)
        {
            GameManager.Instance.SettingGameState(Game_State_Enum.isSetting);
            mainSettngImage.gameObject.SetActive(true);
            mainSettngImage.rectTransform.DOAnchorPosY(0, 1f).SetEase(Ease.OutCirc).SetUpdate(true);
            //Time.timeScale = 0f;
        }
        else
        {
            mainSettngImage.rectTransform.DOAnchorPosY(1171f, 1f).SetEase(Ease.OutCirc).SetUpdate(true);
            StartCoroutine(CountDownReadyGame());
        }
    }


    /// <summary>
    /// 상단의 ?를 눌렀을 때 보여주는 함수
    /// </summary>
    public void OnClickQuestion()
    {
        if (isCount) return;

        isQuestionChang = !isQuestionChang;
        if (isQuestionChang)
        {
            GameManager.Instance.SettingGameState(Game_State_Enum.isSetting);
            questionImage.gameObject.SetActive(true);
            questionImage.rectTransform.DOAnchorPosY(0, 1f).SetEase(Ease.OutCirc).SetUpdate(true);
            //Time.timeScale = 0f;
        }
        else
        {
            questionImage.rectTransform.DOAnchorPosY(-1772f, 1.5f).SetEase(Ease.OutCirc).SetUpdate(true);
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

        GameManager.Instance.SettingGameState(Game_State_Enum.isPlaying);
        isCount = false;
    }

    public void HitScreen()
    {
        StartCoroutine(ScreenHit());
    }

    public IEnumerator ScreenHit()
    {
        hitEnemyOutImage.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.1f);
        hitEnemyOutImage.color = new Color(1, 1, 1, 0.4f);
    }

    /// <summary>
    /// 게임 종료됬을 때 이벤트
    /// </summary>
    public void GameOver()
    {
        ingameObj.gameObject.transform.DOScale(new Vector3(1.6f, 1.6f, 1f), 3f);

        Invoke(nameof(GameOverChangShow), 1f);
    }

    private void GameOverChangShow()
    {
        gameOverObj.gameObject.SetActive(true);
        gameOverObj.gameObject.transform.DOScale(new Vector3(1, 1, 1), 2f).SetEase(ease);
    }

    public void OnClickRestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickGoToMenu()
    {
        SceneManager.LoadScene(0);
    }


    private void SetResolution()
    {
        int _width = 1920;
        int _height = 1080;

        Screen.SetResolution(_width, _height, true);
        Screen.SetResolution(Screen.width, (Screen.width * 16) / 9, true);
    }
}
