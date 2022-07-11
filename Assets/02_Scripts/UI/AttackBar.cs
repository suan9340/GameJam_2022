using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackBar : MonoBehaviour
{
    [SerializeField] private Image barImage = null;

    private Player_data playerData = null;

    private void Start()
    {
        playerData = Resources.Load<Player_data>("SO/" + "PlayerData");
    }

    private void Update()
    {
        ConnectBar();
    }

    private void ConnectBar()
    {
        barImage.fillAmount = playerData.current_attackPower / playerData.max_attackPower;
    }
}
