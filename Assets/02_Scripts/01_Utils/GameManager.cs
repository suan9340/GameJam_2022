using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    #region SingleTon   

    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    #endregion

    public Game_State_Enum gameState;

    private Player_data playerData = null;

    public BulletPoolManager poolManager { get; private set; }

    public Vector2 MinPosition { get; private set; }
    public Vector2 MaxPosition { get; private set; }

    [Header("±§º± æ∆¿Ã≈€")]
    public GameObject gunItem = null;

    private void Awake()
    {
        playerData = Resources.Load<Player_data>("SO/" + "PlayerData");
    }

    private void OnEnable()
    {
        if (playerData.isfirst)
        {
            playerData.isfirst = false;

            UIManager.Instance.OnClickQuestion();
        }
    }

    private void Start()
    {


        MinPosition = new Vector2(-10.4f, -6.2f);
        MaxPosition = new Vector2(10.4f, 6.2f);

        poolManager = FindObjectOfType<BulletPoolManager>();
    }

    public void SettingGameState(Game_State_Enum _state)
    {
        gameState = _state;
    }

    public void ShackeCam(float _dur, float _str, int _vib)
    {
        Camera.main.DOShakePosition(_dur, _str, _vib);
    }

    public void ItemGunStart()
    {
        gunItem.SetActive(true);
    }
}
