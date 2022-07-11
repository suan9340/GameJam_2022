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
}
