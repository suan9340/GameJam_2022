using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player_data playerData = null;
    public Player_State_Enum playerState;

    public static readonly WaitForSeconds checkTime = new WaitForSeconds(1f);

    public bool isLeftBtn = false;
    public bool isRightBtn = false;
    public bool isShooting = false;

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

    private void ChangeBoolen(bool _isCheck)
    {

    }

    private void CheckState()
    {
        if (!isShooting)
        {
            if (isLeftBtn)
                Debug.Log("왼쪽");

            if (isRightBtn)
                Debug.Log("오른쪽");
        }

        if (isShooting)
        {
            Debug.Log("공격");
        }

    }
}
