using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float moveSpeed = 2f;
    private void Update()
    {
        Move();
        CheckLimit();
    }

    private void Move()
    {
        transform.localPosition += transform.up * moveSpeed * Time.deltaTime;
    }

    private void CheckLimit()
    {
        if (transform.localPosition.y < GameManager.Instance.MinPosition.y)
        {
            Despawn();
        }
        if (transform.localPosition.y > GameManager.Instance.MaxPosition.y)
        {
            Despawn();
        }
        if (transform.localPosition.x < GameManager.Instance.MinPosition.y)
        {
            Despawn();
        }
        if (transform.localPosition.x > GameManager.Instance.MaxPosition.x)
        {
            Despawn();
        }
    }

    private void Despawn()
    {
        transform.SetParent(GameManager.Instance.poolManager.transform, false);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
