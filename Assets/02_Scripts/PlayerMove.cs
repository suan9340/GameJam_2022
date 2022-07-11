using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player_data data;

    private void Start()
    {
        data = Resources.Load<Player_data>("SO/" + "PlayerData");
    }
}
