using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMOM : MonoBehaviour
{
    [SerializeField] private Vector2 followTarget = Vector2.zero;
    [SerializeField] private float moveSpeed = 1f;

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
            collision.GetComponent<BulletMove>().Despawn();
            ActionItem();
            gameObject.SetActive(false);
        }
    }

    protected virtual void ActionItem()
    {

    }
}
