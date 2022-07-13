using UnityEngine;

[System.Serializable]


[CreateAssetMenu(fileName = "PlayerData",
menuName = "Scriptable Object/PlayerData")]
public class Player_data : ScriptableObject
{
    public float current_attackPower;
    public float max_attackPower;
    public float moveSpeed;
    public float score;
    public float bestScore;
}
