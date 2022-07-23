using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    private Player_data playerData;

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

    [Header("���� ī��Ʈ�ٿ� TEXT")] public Text countDownText;

    [Header("����â UI")]
    public Image mainSettngImage = null;

    [Header("����ǥâ UI")]
    public Image questionImage = null;

    [Header("����ǥâ ��ũ��")]
    public GameObject scrollCotent = null;

    [Header("�� �׿��� �� �׵θ� UI")]
    public Image hitEnemyOutImage = null;
    private bool isCount = false;

    [Header("---------�ΰ��� ������Ʈ---------")]
    public GameObject ingameObj = null;

    [Header("---------���ӿ��� ������Ʈ---------")]
    public GameObject gameOverObj = null;
    public Ease ease;

    [Header("�� �� ��ư ������Ʈ")]
    public Button leftBtn = null;
    public Button rightBtn = null;

    [Header("���� UI")]
    public Text scoreText = null;

    [Header("�� ������ �Ծ��� �� UI")]
    public Image starAttackBar = null;

    private readonly WaitForSeconds attackBarDelay = new WaitForSeconds(0.05f);
    private void Awake()
    {
        playerData = Resources.Load<Player_data>("SO/" + "PlayerData");
    }

    private void Start()
    {
        StartCoroutine(CountDownReadyGame());
    }

    private void Update()
    {
        if (GameManager.Instance.gameState == Game_State_Enum.isDie) return;
        InputKey();
    }

    /// <summary>
    /// �÷��̾��� Ű �Է� ���¸� üũ�ϴ� �Լ�
    /// </summary>
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
    /// ȭ��� �ִ� ����â ������ �� or ESC ������ ��
    /// </summary>
    public void OnClickSettingChang()
    {
        if (isCount) return;
        AudioManager.Instance.Sound_ClickButton();

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
    /// ����� ?�� ������ �� �����ִ� �Լ�
    /// </summary>
    public void OnClickQuestion()
    {
        if (isCount) return;
        AudioManager.Instance.Sound_ClickButton();

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


    /// <summary>
    /// 3 2 1 ī��Ʈ �ٿ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// �� �׿��� �� ȭ�� �׵θ� �Ͼ�� �Ǵ°�
    /// </summary>
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
    /// ���� ������� �� �̺�Ʈ
    /// </summary>
    public void GameOver()
    {
        ingameObj.gameObject.transform.DOScale(new Vector3(1.6f, 1.6f, 1f), 3f);

        Invoke(nameof(GameOverChangShow), 1f);
    }

    private void GameOverChangShow()
    {
        gameOverObj.gameObject.SetActive(true);
        gameOverObj.gameObject.transform.DOScale(new Vector3(1, 1, 1), 1.8f).SetEase(ease);
    }

    public void OnClickRestartGame()
    {
        AudioManager.Instance.Sound_ClickButton();
        AudioManager.Instance.RandomPlay();
        SceneManager.LoadScene(1);
    }

    public void OnClickGoToMenu()
    {
        AudioManager.Instance.Sound_ClickButton();
        SceneManager.LoadScene(0);
    }

    #region ��ư �� �ٲٴ°�
    public void LeftBtnActive()
    {
        leftBtn.image.color = leftBtn.colors.pressedColor;
    }

    public void RightBtnActive()
    {
        rightBtn.image.color = rightBtn.colors.pressedColor;
    }

    public void LeftBtnFalse()
    {
        leftBtn.image.color = leftBtn.colors.normalColor;
    }

    public void RightBtnFalse()
    {
        rightBtn.image.color = rightBtn.colors.normalColor;
    }

    public void TwoBtn()
    {
        rightBtn.image.color = rightBtn.colors.selectedColor;
        leftBtn.image.color = leftBtn.colors.selectedColor;
    }
    #endregion


    /// <summary>
    /// �÷��̾� ���� ������Ʈ ���ִ� �Լ�
    /// </summary>
    public void UpdateUI()
    {
        scoreText.text = $"{playerData.score}";
    }


    /// <summary>
    /// �� ������ �Ծ��� �� ���ݷ� bar�� ���ϵ���
    /// </summary>
    public void ChangeAttackBar(bool _isCheck)
    {
        if (_isCheck)
        {
            StartCoroutine(FillIn());
        }
        else
        {
            StartCoroutine(FillOut());
        }
    }

    private IEnumerator FillIn()
    {
        var _a = 0f;

        while (true)
        {
            if (_a >= 1)
            {
                yield break;
            }
            _a += 0.02f;
            starAttackBar.fillAmount = _a;
            yield return attackBarDelay;
        }
    }

    private IEnumerator FillOut()
    {
        var _a = 1f;

        while (true)
        {
            if (_a <= 0)
            {
                GameManager.Instance.SettingItemState(Player_Item_State.Idle);
                AudioManager.Instance.ItemEatSound(false);
                yield break;
            }
            _a -= 0.02f;
            starAttackBar.fillAmount = _a;
            yield return attackBarDelay;
        }
    }
}
