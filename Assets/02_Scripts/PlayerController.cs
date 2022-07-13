using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player_data playerData = null;
    public Player_State_Enum playerState;

    public static readonly WaitForSeconds shootDelay = new WaitForSeconds(0.16f);
    public static readonly WaitForSeconds powerDelay = new WaitForSeconds(0.06f);
    public static readonly WaitForSeconds playerDelay = new WaitForSeconds(0.1f);

    // Ű �Է¹޴� bool
    private bool isLeftBtn = false;
    private bool isRightBtn = false;
    private bool isShooting = false;
    private bool isStoping = false;

    // �ߺ���� �ȵǰ� �ϴ� bool
    private bool isLPush = false;
    private bool isRPush = false;
    private bool isSPush = false;
    private bool isSTPush = false;

    [Header("�Ѿ� ������")]
    public GameObject bulletObj = null;

    [Header("�Ѿ� ��ġ")]
    public Transform frontfireTrn = null;
    public Transform backfireTrn = null;
    public Transform leftfireTrn = null;
    public Transform rightfireTrn = null;
    public Transform leftupdiafireTrn = null;
    public Transform rightupdiafireTrn = null;
    public Transform leftdowndiafireTrn = null;
    public Transform rightdowndiafireTrn = null;

    [Header("���ݷ��� �����ϴ� �ӵ�")]
    public float upPower = 3f;

    [Header("���ݷ��� �����ϴ� �ӵ�")]
    public float downPower = 5f;

    private void Start()
    {
        SettingGame();
        StartCoroutine(PlayerAction());
    }

    private void Update()
    {
        if (GameManager.Instance.gameState == Game_State_Enum.isDie)
        {
            playerState = Player_State_Enum.Stoping;
            return;
        }

        InputKey();
        CheckState();
    }
    private void SettingGame()
    {
        playerData = Resources.Load<Player_data>("SO/" + "PlayerData");
        playerData.current_attackPower = playerData.max_attackPower;
        playerData.score = 0f;
        playerData.playerlevel = 0f;
    }

    #region EventTrigger

    public void OnClickLeftDown()
    {
        LeftDown();
    }

    public void OnClickLeftUp()
    {
        LeftUp();
    }

    public void OnClickRightDown()
    {
        RightDown();
    }

    public void OnClickRightUp()
    {
        RightUp();
    }

    #endregion

    private void LeftDown()
    {
        isLeftBtn = true;

        isStoping = false;
        isSTPush = false;
    }

    private void LeftUp()
    {
        isLeftBtn = false;
        isLPush = false;
    }

    private void RightDown()
    {
        isRightBtn = true;

        isStoping = false;
        isSTPush = false;
    }

    private void RightUp()
    {
        isRightBtn = false;
        isRPush = false;
    }

    /// <summary>
    /// �÷��̾��� �ൿ Ű�� �Է¹޴� �Լ�
    /// </summary>
    /// 
    private void InputKey()
    {
        if (isRightBtn == false && isLeftBtn == false)
        {
            isStoping = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            LeftDown();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            LeftUp();
        }


        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            RightDown();
        }
        if (Input.GetKeyUp(KeyCode.RightShift))
        {
            RightUp();
        }

        if (isRightBtn && isLeftBtn)
        {
            if (isShooting) return;

            isShooting = true;
        }
        else
        {
            if (isShooting == false) return;
            isShooting = false;
            isSPush = false;
        }
    }


    /// <summary>
    /// �Է¹��� Ű�� ������� �÷��̾��� ���¸� �����ϴ� �Լ�
    /// </summary>
    private void CheckState()
    {
        if (isStoping)
        {
            if (isSTPush) return;
            isSTPush = true;

            playerState = Player_State_Enum.Stoping;
        }

        if (!isShooting)
        {
            if (isLeftBtn)
            {
                if (isLPush) return;
                isLPush = true;

                //Debug.Log("��Ŭ");
                playerState = Player_State_Enum.LeftRotating;
            }

            if (isRightBtn)
            {
                if (isRPush) return;
                isRPush = true;

                //Debug.Log("��Ŭ");
                playerState = Player_State_Enum.RightRotating;
            }


        }

        if (isShooting)
        {
            if (isSPush) return;
            isSPush = true;

            //Debug.Log("����");
            playerState = Player_State_Enum.Attacking;
        }
    }


    /// <summary>
    /// �÷��̾��� ���¿� ���� �÷��̾��� �ൿ�� �ٲ��ִ� �Լ�
    /// </summary>
    private IEnumerator PlayerAction()
    {
        while (true)
        {
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
                    Debug.Log("����?!");
                    break;
            }

            yield return null;
        }
    }

    private void LeftRotate()
    {
        if (playerData.current_attackPower <= 0) return;

        transform.eulerAngles += new Vector3(0, 0, 1) * playerData.moveSpeed * Time.deltaTime;
    }

    private void RightRotate()
    {
        if (playerData.current_attackPower <= 0) return;

        transform.eulerAngles -= new Vector3(0, 0, 1) * playerData.moveSpeed * Time.deltaTime;
    }

    private IEnumerator ShootReady()
    {
        while (playerState == Player_State_Enum.Attacking)
        {
            switch (playerData.playerlevel)
            {
                case 0:
                    SpawnORInstantiate(frontfireTrn);
                    break;

                case 1:
                    SpawnORInstantiate(frontfireTrn);
                    SpawnORInstantiate(backfireTrn);
                    break;

                case 2:
                    SpawnORInstantiate(frontfireTrn);
                    SpawnORInstantiate(backfireTrn);
                    SpawnORInstantiate(rightfireTrn);
                    SpawnORInstantiate(leftfireTrn);
                    break;

                case 3:
                    SpawnORInstantiate(frontfireTrn);
                    SpawnORInstantiate(backfireTrn);
                    SpawnORInstantiate(rightfireTrn);
                    SpawnORInstantiate(leftfireTrn);
                    SpawnORInstantiate(leftupdiafireTrn);
                    SpawnORInstantiate(rightdowndiafireTrn);
                    break;

                case 4:
                    SpawnORInstantiate(frontfireTrn);
                    SpawnORInstantiate(backfireTrn);
                    SpawnORInstantiate(rightfireTrn);
                    SpawnORInstantiate(leftfireTrn);
                    SpawnORInstantiate(leftdowndiafireTrn);
                    SpawnORInstantiate(rightdowndiafireTrn);
                    SpawnORInstantiate(leftupdiafireTrn);
                    SpawnORInstantiate(rightupdiafireTrn);
                    break;
            }

            AudioManager.Instance.ShootGun();
            yield return shootDelay;
        }

    }

    private void SpawnORInstantiate(Transform _posTrn)
    {
        GameObject _bullet = null;
        if (GameManager.Instance.poolManager.transform.childCount > 0)
        {
            _bullet = GameManager.Instance.poolManager.transform.GetChild(0).gameObject;
            _bullet.transform.SetParent(frontfireTrn.transform, false);

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
    }
}
