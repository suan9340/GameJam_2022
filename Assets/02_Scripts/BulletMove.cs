using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float moveSpeed = 2f;
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.localPosition += transform.up * moveSpeed * Time.deltaTime;
    }

    public void Despawn()
    {
        transform.SetParent(GameManager.Instance.poolManager.transform, false);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ConstantManager.TAG_DESBUL))
            Despawn();
    }
}
