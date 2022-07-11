using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public BulletPoolManager poolManager { get; private set; }

    public Vector2 MinPosition { get; private set; }
    public Vector2 MaxPosition { get; private set; }

    private void Start()
    {
        MinPosition = new Vector2(-5f, -7f);
        MaxPosition = new Vector2(5f, 7f);

        poolManager = FindObjectOfType<BulletPoolManager>();
    }
}
