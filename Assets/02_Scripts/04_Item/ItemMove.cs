using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    protected Player_data playerData;
    [SerializeField] private Vector2 followTarget = Vector2.zero;
    [SerializeField] private float moveSpeed = 1f;

    private bool isGameOver = false;

    private bool isDie = false;
    private void Start()
    {
        playerData = Resources.Load<Player_data>("SO/" + "PlayerData");
    }

    private void Update()
    {
        if (GameManager.Instance.gameState == Game_State_Enum.isSetting) return;

        if (GameManager.Instance.gameState == Game_State_Enum.isDie)
        {
            IsGameOver();
        }

        Move();

    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, followTarget, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ConstantManager.TAG_BULLET))
        {
            if (isDie) return;
            isDie = true;

            collision.GetComponent<BulletMove>().Despawn();

            ItemEat();
        }
    }

    protected virtual void ItemEat()
    {
        AudioManager.Instance.Sound_ItemEat();
        ParticleManager.Instance.AddParticle(ParticleManager.ParticleType.itemEat, transform.position);
        CheckItemCurrent();
        Destroy(gameObject);
    }

    private void CheckItemCurrent()
    {
        if (playerData.playerlevel >= 4)
        {
            playerData.score += 200;
            UIManager.Instance.UpdateUI();
            return;
        }
        playerData.playerlevel++;
    }

    private void IsGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Invoke(nameof(ItemGameOver), 1.2f);
    }

    private void ItemGameOver()
    {
        ParticleManager.Instance.AddParticle(ParticleManager.ParticleType.itemEat, transform.position);
        Destroy(gameObject);
    }
}
