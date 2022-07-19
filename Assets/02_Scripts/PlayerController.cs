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

[System.Serializable]
public class playerAttackDown
{
    public string name;
    public float score;
}

public class PlayerController : MonoBehaviour
{
    private Player_data playerData = null;

    [Header("ENUMS")]
    public Player_State_Enum playerState;


    public static readonly WaitForSeconds shootDelay = new WaitForSeconds(0.16f);
    public static readonly WaitForSeconds powerDelay = new WaitForSeconds(0.06f);
    public static readonly WaitForSeconds playerDelay = new WaitForSeconds(0.1f);

    // Ű �Է¹޴� bool
    private bool isLeftBtn = false;
    private bool isRightBtn = false;

    [Header("�Ѿ� ������")]
    public GameObject bulletObj = null;

    [Header("�Ѿ� ���õȰ�")]
    public List<playergunInfo> gunInfo = new List<playergunInfo>();

    [Header("���ݷ��� �����ϴ� �ӵ�")]
    public float upPower = 3f;

    [Header("���ݷ��� �����ϴ� �ӵ�")]
    public float downPower = 5f;

    [Header("������ ���� ���ݷ� ����")]
    public List<playerAttackDown> attackDowns = new List<playerAttackDown>();

    private void Awake()
    {
        SettingGame();
    }
    private void Start()
    {
        //GameManager.Instance.ItemGunStart();
        StartCoroutine(PlayerAction());
        AudioManager.Instance.RandomPlay();
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


        // �����ϰ� �ִ� ������ ��
        if (isLeftBtn && isRightBtn)
        {
            UIManager.Instance.TwoBtn();
            playerState = Player_State_Enum.Attacking;
        }
        else if (isRightBtn)
        {
            UIManager.Instance.RightBtnActive();
            UIManager.Instance.LeftBtnFalse();

            playerState = Player_State_Enum.RightRotating;
        } // ������Ű�� ����
        else if (isLeftBtn)
        {
            UIManager.Instance.LeftBtnActive();
            UIManager.Instance.RightBtnFalse();

            playerState = Player_State_Enum.LeftRotating;
        } // �Ѵ� ����
        else
        {
            UIManager.Instance.RightBtnFalse();
            UIManager.Instance.LeftBtnFalse();

            playerState = Player_State_Enum.Stoping;
        }
    }

    /// <summary>
    /// �÷��̾��� ���¿� ���� �÷��̾��� �ൿ�� �ٲ��ִ� �Լ�
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

                    if (GameManager.Instance.playerItemState == Player_Item_State.Staring) break;
                    PowerDown();
                    break;

                case Player_State_Enum.RightRotating:
                    RightRotate();

                    if (GameManager.Instance.playerItemState == Player_Item_State.Staring) break;
                    PowerDown();
                    break;

                case Player_State_Enum.Attacking:
                    yield return ShootReady();
                    break;

                case Player_State_Enum.Stoping:
                    PowerUP();
                    break;

                default:
                    Debug.Log("����?!");
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

        CheckScoreAndSetPlayerAttacked();
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
    }


    /// <summary>
    /// ������ ���� ���ݷ� ���� ������ �������ִ� �Լ�
    /// </summary>
    private void CheckScoreAndSetPlayerAttacked()
    {
        if (playerData.score < attackDowns[0].score)
        {
            upPower = 2.4f;
            downPower = 3.8f;
        }
        else if (playerData.score < attackDowns[1].score)
        {
            upPower = 2f;
            downPower = 4f;
        }
        else if (playerData.score < attackDowns[2].score)
        {
            upPower = 1.5f;
            downPower = 4.6f;
        }
        else if (playerData.score < attackDowns[3].score)
        {
            upPower = 1.2f;
            downPower = 5.3f;
        }
        else if (playerData.score < attackDowns[4].score)
        {
            upPower = 1f;
            downPower = 6f;
        }
        else if (playerData.score < attackDowns[5].score)
        {
            upPower = 0.7f;
            downPower = 7f;
        }
        else if (playerData.score < attackDowns[6].score)
        {
            upPower = 0.4f;
            downPower = 7.2f;
        }
    }
}
