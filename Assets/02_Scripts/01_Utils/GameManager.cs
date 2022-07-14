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

    public BulletPoolManager poolManager { get; private set; }

    public Vector2 MinPosition { get; private set; }
    public Vector2 MaxPosition { get; private set; }

    [Header("±§º± æ∆¿Ã≈€")]
    public GameObject gunItem = null;

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
        //StartCoroutine(EnergyGun());
        gunItem.transform.DORotate(new Vector3(0, 0, 360f), 2.5f, RotateMode.FastBeyond360).OnComplete(() => { gunItem.SetActive(false); });
    }
}
