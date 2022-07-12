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
    [Header("최고 체력")][SerializeField] private float highhp = 10f;
    [Header("최악 체력")][SerializeField] private float lowhp = 10f;

    [Header("UI 관련")]
    [SerializeField] private Text textObj = null;

    public float enemyhp;

    private void Start()
    {
        SetRandomHPEnemy();
        //enemyhp = 100f;

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
                AddScore(100);
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
        Camera.main.DOShakePosition(_dur, _str, _vib);
    }

    private void AddScore(int _score)
    {
        playerData.playerScore += _score;
    }

    private void SetRandomHPEnemy()
    {
        var _hp = Random.Range(lowhp, highhp);

        enemyhp = (int)_hp;
    }
}
