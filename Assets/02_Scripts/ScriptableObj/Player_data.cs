using UnityEngine;

[System.Serializable]


[CreateAssetMenu(fileName = "PlayerData",
menuName = "Scriptable Object/PlayerData")]
public class Player_data : ScriptableObject
{
    public float playerHp;
    public float attackPower;
    public float moveSpeed;
}
