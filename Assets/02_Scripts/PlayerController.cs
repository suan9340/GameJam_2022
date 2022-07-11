using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player_data playerData = null;
    public Player_State_Enum playerState;

    public static readonly WaitForSeconds shootDelay = new WaitForSeconds(0.2f);
    public static readonly WaitForSeconds powerDelay = new WaitForSeconds(0.06f);
    public static readonly WaitForSeconds playerDelay = new WaitForSeconds(0.1f);

    // Ű �Է¹޴� bool
    public bool isLeftBtn = false;
    public bool isRightBtn = false;
    public bool isShooting = false;
    public bool isStoping = false;

    // �ߺ���� �ȵǰ� �ϴ� bool
    private bool isLPush = false;
    private bool isRPush = false;
    private bool isSPush = false;
    private bool isSTPush = false;

    Coroutine leftCor = null;
    Coroutine rightCor = null;
    Coroutine ShootCor = null;
    Coroutine StopCor = null;

    [Header("�Ѿ� ������")]
    public GameObject bulletObj = null;

    [Header("�Ѿ� ��ġ")]
    public Transform fireTrn = null;

    [Header("���ݷ��� �����ϴ� �ӵ�")]
    public float upPower = 3f;

    [Header("���ݷ��� �����ϴ� �ӵ�")]
    public float downPower = 5f;

    private void Start()
    {
        playerData = Resources.Load<Player_data>("SO/" + "PlayerData");

        playerData.current_attackPower = playerData.max_attackPower;
        StartCoroutine(PlayerAction());
    }

    private void Update()
    {
        InputKey();
        CheckState();
        //Debug.Log(transform.localEulerAngles.z);
        //Debug.Log(transform.eulerAngles.z);
        //Debug.Log(transform.rotation.z);
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

                Debug.Log("��Ŭ");
                playerState = Player_State_Enum.LeftRotating;
            }

            if (isRightBtn)
            {
                if (isRPush) return;
                isRPush = true;

                Debug.Log("��Ŭ");
                playerState = Player_State_Enum.RightRotating;
            }


        }

        if (isShooting)
        {
            if (isSPush) return;
            isSPush = true;

            Debug.Log("����");
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
        //transform.Rotate(new Vector3(0, 0, 1) * playerData.moveSpeed * Time.deltaTime);

        transform.localEulerAngles += new Vector3(0, 0, 1) * playerData.moveSpeed * Time.deltaTime;
        //if (transform.localEulerAngles.z >= 0f && transform.localEulerAngles.z <= 180f)
        //    transform.localEulerAngles += new Vector3(0, 0, 1) * playerData.moveSpeed * Time.deltaTime;

        //else
        //    transform.localEulerAngles -= new Vector3(0, 0, 1) * playerData.moveSpeed * Time.deltaTime;
    }

    private void RightRotate()
    {
        if (playerData.current_attackPower <= 0) return;
        //transform.Rotate(new Vector3(0, 0, -1) * playerData.moveSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, -1) * playerData.moveSpeed * Time.deltaTime);

        //if (transform.localEulerAngles.z <= 0f && transform.localEulerAngles.z <= -180f)
        //    transform.localEulerAngles -= new Vector3(0, 0, 1) * playerData.moveSpeed * Time.deltaTime;

        //else
        //    transform.localEulerAngles += new Vector3(0, 0, 1) * playerData.moveSpeed * Time.deltaTime;
    }

    private IEnumerator ShootReady()
    {
        while (playerState == Player_State_Enum.Attacking)
        {
            SpawnORInstantiate();
            yield return shootDelay;
        }

    }

    private void SpawnORInstantiate()
    {
        GameObject _bullet = null;
        if (GameManager.Instance.poolManager.transform.childCount > 0)
        {
            _bullet = GameManager.Instance.poolManager.transform.GetChild(0).gameObject;
            _bullet.transform.SetParent(fireTrn.transform, false);
            _bullet.transform.position = fireTrn.position;
            _bullet.SetActive(true);
        }
        else
        {
            _bullet = Instantiate(bulletObj, fireTrn.position, transform.rotation);
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
            Debug.Log("GameOut");
        }
    }
}
