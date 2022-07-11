using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player_data playerData = null;
    public Player_State_Enum playerState;

    public static readonly WaitForSeconds shootDelay = new WaitForSeconds(0.3f);
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
    public float upPower = 6f;

    [Header("���ݷ��� �����ϴ� �ӵ�")]
    public float downPower = 4f;

    private void Start()
    {
        playerData = Resources.Load<Player_data>("SO/" + "PlayerData");

        //playerData.current_attackPower = playerData.max_attackPower;
        StartCoroutine(PlayerAction());
    }

    private void Update()
    {
        InputKey();
        CheckState();

    }

    #region EventTrigger
    public void OnClickLeftDown()
    {
        isLeftBtn = true;

        isStoping = false;
        isSTPush = false;
    }

    public void OnClickLeftUp()
    {
        isLeftBtn = false;
        isLPush = false;
    }

    public void OnClickRightDown()
    {
        isRightBtn = true;

        isStoping = false;
        isSTPush = false;
    }

    public void OnClickRightUp()
    {
        isRightBtn = false;
        isRPush = false;
    }
    #endregion


    /// <summary>
    /// �÷��̾��� �ൿ Ű�� �Է¹޴� �Լ�
    /// </summary>
    private void InputKey()
    {
        if (isRightBtn == false && isLeftBtn == false)
        {
            isStoping = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            OnClickLeftDown();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            OnClickLeftUp();
        }


        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            OnClickRightDown();
        }
        if (Input.GetKeyUp(KeyCode.RightShift))
        {
            OnClickRightUp();
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
        transform.Rotate(new Vector3(0, 0, 1) * playerData.moveSpeed * Time.deltaTime);
    }

    private void RightRotate()
    {
        if (playerData.current_attackPower <= 0) return;
        transform.Rotate(new Vector3(0, 0, -1) * playerData.moveSpeed * Time.deltaTime);
    }

    private IEnumerator ShootReady()
    {
        while (playerState == Player_State_Enum.Attacking)
        {
            var obj = Instantiate(bulletObj);

            Destroy(obj, 2f);

            yield return shootDelay;
        }

    }

    private void PowerUP()
    {
        if (playerData.current_attackPower >= playerData.max_attackPower) return;

        playerData.current_attackPower += upPower * Time.deltaTime;
    }

    private void PowerDown()
    {
        if (playerData.current_attackPower <= 0) return;

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
