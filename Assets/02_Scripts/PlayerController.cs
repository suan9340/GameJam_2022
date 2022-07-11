using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player_data playerData = null;
    public Player_State_Enum playerState;

    public static readonly WaitForSeconds checkTime = new WaitForSeconds(1f);

    private bool isLeftBtn = false;
    private bool isRightBtn = false;
    private bool isShooting = false;

    private void Start()
    {
        playerData = Resources.Load<Player_data>("SO/" + "PlayerData");
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
    }

    public void OnClickLeftUp()
    {
        isLeftBtn = false;
    }

    public void OnClickRightDown()
    {
        isRightBtn = true;
    }

    public void OnClickRightUp()
    {
        isRightBtn = false;
    }
    #endregion

    private void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isLeftBtn = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isLeftBtn = false;
        }



        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            isRightBtn = true;
        }
        if (Input.GetKeyUp(KeyCode.RightShift))
        {
            isRightBtn = false;
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
        }
    }

    private void CheckState()
    {
        if (!isShooting)
        {
            if (isLeftBtn)
            {
                Debug.Log("좌클");
                playerState = Player_State_Enum.LeftRotating;
            }

            if (isRightBtn)
            {
                Debug.Log("우클");
                playerState = Player_State_Enum.RightRotating;
            }
        }

        if (isShooting)
        {
            Debug.Log("공격");
            playerState = Player_State_Enum.Attacking;
        }

    }

    private void LeftRotate()
    {

    }

    private void RightRotate()
    {

    }

    private void Shoote()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ConstantManager.TAG_ENEMY))
        {
            Debug.Log("GameOut");
        }
    }
}
