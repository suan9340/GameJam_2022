using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    private Player_data playerData = null;

    public Text scoreText = null;

    private void Start()
    {
        playerData = Resources.Load<Player_data>("SO/" + "PlayerData");

        UpdateUI();
    }

    private void LateUpdate()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = $"{playerData.playerScore}";
    }
}
