using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    private Player_data playerData;

    [SerializeField] private Vector2 followTarget = Vector2.zero;
    [SerializeField] private float moveSpeed = 1f;
    [Header("체력")][SerializeField] private float enemyhp = 10f;

    [Header("UI 관련")]
    [SerializeField] private Text textObj = null;

    private Camera camera;
    private void Start()
    {
        camera = Camera.main;
        playerData = Resources.Load<Player_data>("SO/" + "PlayerData");

        UpdateEnemyHP();
    }
    private void Update()
    {
        Move();
        FollowTextUI();
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

            Destroy(collision.gameObject);
            enemyhp -= playerData.current_attackPower;

            UpdateEnemyHP();
            if (enemyhp <= 0)
            {
                ShackeCam(0.5f, 0.15f, 13);
                enemyhp = 0;
                Destroy(gameObject);
            }
        }
    }

    private void FollowTextUI()
    {
        textObj.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0, 0));
    }

    private void UpdateEnemyHP()
    {
        textObj.text = $"{(int)enemyhp}";
    }

    private void ShackeCam(float _dur, float _str, int _vib)
    {
        camera.DOShakePosition(_dur, _str, _vib);
    }
}
