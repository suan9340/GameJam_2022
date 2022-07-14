using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class playergunInfo
{
    public string name;
    public Transform transform;
    public GameObject obj;
}

public class PlayerController : MonoBehaviour
{
    private Player_data playerData = null;
    public Player_State_Enum playerState;

    public static readonly WaitForSeconds shootDelay = new WaitForSeconds(0.16f);
    public static readonly WaitForSeconds powerDelay = new WaitForSeconds(0.06f);
    public static readonly WaitForSeconds playerDelay = new WaitForSeconds(0.1f);

    // 키 입력받는 bool
    public bool isLeftBtn = false;
    public bool isRightBtn = false;

    [Header("총알 프리팹")]
    public GameObject bulletObj = null;

    [Header("총알 관련된거")]
    public List<playergunInfo> gunInfo = new List<playergunInfo>();

    [Header("공격력이 증가하는 속도")]
    public float upPower = 3f;

    [Header("공격력이 감소하는 속도")]
    public float downPower = 5f;

    private void Awake()
    {
        SettingGame();
    }
    private void Start()
    {
        //GameManager.Instance.ItemGunStart();
        StartCoroutine(PlayerAction());
    }

    private void Update()
    {
        if (GameManager.Instance.gameState == Game_State_Enum.isDie)
        {
            playerState = Player_State_Enum.Stoping;
            return;
        }

        Key();
    }

    private void SettingGame()
    {
        playerData = Resources.Load<Player_data>("SO/" + "PlayerData");
        playerData.current_attackPower = playerData.max_attackPower;
        playerData.score = 0f;
        playerData.playerlevel = 0f;
    }

    private void Key()
    {
        isLeftBtn = Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.F);
        isRightBtn = Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.J);


        // 공격하고 있는 상태일 때
        if (isLeftBtn && isRightBtn)
        {
            //UIManager.Instance.LeftBtnActive();
            //UIManager.Instance.RightBtnActive();
            UIManager.Instance.TwoBtn();
            playerState = Player_State_Enum.Attacking;
        }
        else if (isRightBtn)
        {
            UIManager.Instance.RightBtnActive();
            UIManager.Instance.LeftBtnFalse();

            playerState = Player_State_Enum.RightRotating;
        } // 오른쪽키를 때면
        else if (isLeftBtn)
        {
            UIManager.Instance.LeftBtnActive();
            UIManager.Instance.RightBtnFalse();

            playerState = Player_State_Enum.LeftRotating;
        } // 둘다 때면
        else
        {
            UIManager.Instance.RightBtnFalse();
            UIManager.Instance.LeftBtnFalse();

            playerState = Player_State_Enum.Stoping;
        }
    }

    /// <summary>
    /// 플레이어의 상태에 따라 플레이어의 행동을 바꿔주는 함수
    /// </summary>
    private IEnumerator PlayerAction()
    {
        while (true)
        {
            while (GameManager.Instance.gameState == Game_State_Enum.isSetting) yield return null;

            switch (playerState)
            {
                case Player_State_Enum.LeftRotating:
                    LeftRotate();
                    PowerDown();
                    break;

                case Player_State_Enum.RightRotating:
                    RightRotate();
                    PowerDown();
                    break;

                case Player_State_Enum.Attacking:
                    yield return ShootReady();
                    break;

                case Player_State_Enum.Stoping:
                    PowerUP();
                    break;

                default:
                    Debug.Log("오잉?!");
                    break;
            }

            yield return null;
        }
    }

    private void LeftRotate()
    {
        if (playerData.current_attackPower <= 0) return;

        transform.Rotate(new Vector3(0, 0, 1) * playerData.moveSpeed * Time.deltaTime);
        //transform.eulerAngles += new Vector3(0, 0, 1) * playerData.moveSpeed * Time.deltaTime;
    }

    private void RightRotate()
    {
        if (playerData.current_attackPower <= 0) return;

        transform.Rotate(new Vector3(0, 0, -1) * playerData.moveSpeed * Time.deltaTime);
        //transform.eulerAngles -= new Vector3(0, 0, 1) * playerData.moveSpeed * Time.deltaTime;
    }

    private IEnumerator ShootReady()
    {
        while (playerState == Player_State_Enum.Attacking)
        {
            switch (playerData.playerlevel)
            {
                case 0:
                    SpawnORInstantiate(gunInfo[0].transform, gunInfo[0].obj);
                    break;

                case 1:
                    SpawnORInstantiate(gunInfo[0].transform, gunInfo[0].obj);
                    SpawnORInstantiate(gunInfo[1].transform, gunInfo[1].obj);
                    break;

                case 2:
                    SpawnORInstantiate(gunInfo[0].transform, gunInfo[0].obj);
                    SpawnORInstantiate(gunInfo[1].transform, gunInfo[1].obj);
                    SpawnORInstantiate(gunInfo[2].transform, gunInfo[2].obj);
                    SpawnORInstantiate(gunInfo[3].transform, gunInfo[3].obj);
                    break;

                case 3:
                    SpawnORInstantiate(gunInfo[0].transform, gunInfo[0].obj);
                    SpawnORInstantiate(gunInfo[1].transform, gunInfo[1].obj);
                    SpawnORInstantiate(gunInfo[2].transform, gunInfo[2].obj);
                    SpawnORInstantiate(gunInfo[3].transform, gunInfo[3].obj);
                    SpawnORInstantiate(gunInfo[4].transform, gunInfo[4].obj);
                    SpawnORInstantiate(gunInfo[5].transform, gunInfo[5].obj);
                    break;

                case 4:
                    SpawnORInstantiate(gunInfo[0].transform, gunInfo[0].obj);
                    SpawnORInstantiate(gunInfo[1].transform, gunInfo[1].obj);
                    SpawnORInstantiate(gunInfo[2].transform, gunInfo[2].obj);
                    SpawnORInstantiate(gunInfo[3].transform, gunInfo[3].obj);
                    SpawnORInstantiate(gunInfo[4].transform, gunInfo[4].obj);
                    SpawnORInstantiate(gunInfo[5].transform, gunInfo[5].obj);
                    SpawnORInstantiate(gunInfo[6].transform, gunInfo[6].obj);
                    SpawnORInstantiate(gunInfo[7].transform, gunInfo[7].obj);
                    break;
            }

            AudioManager.Instance.ShootGun();
            yield return shootDelay;
        }

    }

    private void SpawnORInstantiate(Transform _posTrn, GameObject _obj)
    {
        if (_obj.activeSelf == false)
            _obj.SetActive(true);

        GameObject _bullet = null;
        if (GameManager.Instance.poolManager.transform.childCount > 0)
        {
            _bullet = GameManager.Instance.poolManager.transform.GetChild(0).gameObject;
            _bullet.transform.SetParent(_posTrn.transform, false);

            _bullet.transform.position = _posTrn.position;
            _bullet.transform.rotation = _posTrn.rotation;

            _bullet.SetActive(true);
        }
        else
        {
            _bullet = Instantiate(bulletObj, _posTrn.position, _posTrn.rotation);
        }
        if (_bullet != null)
        {
            _bullet.transform.SetParent(null);
        }
    }

    private void PowerUP()
    {
        if (playerData.current_attackPower >= playerData.max_attackPower)
        {
            playerData.current_attackPower = playerData.max_attackPower;
            return;
        }

        playerData.current_attackPower += upPower * Time.deltaTime;
    }

    private void PowerDown()
    {
        if (playerData.current_attackPower <= 0)
        {
            playerData.current_attackPower = 0;
            return;
        }

        playerData.current_attackPower -= downPower * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ConstantManager.TAG_ENEMY))
        {
            ParticleManager.Instance.AddParticle(ParticleManager.ParticleType.playerDie, transform.position);
            GameManager.Instance.SettingGameState(Game_State_Enum.isDie);

            UIManager.Instance.GameOver();
            Debug.Log("GameOut");
        }

        //if (collision.CompareTag(ConstantManager.TAG_ITEM))
        //{
        //    Destroy(collision.gameObject);
        //}
    }
}
