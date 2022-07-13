using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    private Player_data playerData;
    [SerializeField] private Vector2 followTarget = Vector2.zero;
    [SerializeField] private float moveSpeed = 1f;

    private void Start()
    {
        playerData = Resources.Load<Player_data>("SO/" + "PlayerData");
    }

    private void Update()
    {
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
            AudioManager.Instance.ItemEat();
            ParticleManager.Instance.AddParticle(ParticleManager.ParticleType.itemEat, transform.position);

            collision.GetComponent<BulletMove>().Despawn();

            Destroy(gameObject);

            if (playerData.playerlevel == 4) return;
            playerData.playerlevel++;
        }
    }
}
