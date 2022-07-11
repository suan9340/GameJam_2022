using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player_data data;

    
    public Player_State_Enum playerState;

    private void Start()
    {
        data = Resources.Load<Player_data>("SO/" + "PlayerData");
    }

    private void Update()
    {
        InputKey();
    }

    private void InputKey()
    {

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

  
    
    private void CheckState()
    {

    }

    private void PlayerStop()
    {

    }
}
