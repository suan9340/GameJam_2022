using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Enemy2Move : MonoBehaviour
{
    private Player_data playerData;

    [SerializeField] private Vector2 followTarget = Vector2.zero;
    [SerializeField] private float moveSpeed = 1f;
    [Header("최고 체력")][SerializeField] private float highhp = 100f;
    [Header("최악 체력")][SerializeField] private float lowhp = 10f;

    [Header("UI 관련")]
    [SerializeField] private Text textObj = null;

    [Header("점수 죽였을 때")]
    public int score;

    [Header("응애아가 적")]
    public GameObject childEnemy = null;

    public float enemyhp;

    private bool isGameOver = false;

    private void Awake()
    {
        playerData = Resources.Load<Player_data>("SO/" + "PlayerData");
        FollowTextUI();
    }

    private void Start()
    {
        SetRandomHPEnemy();
        UpdateEnemyHP();
    }
    private void Update()
    {
        if (GameManager.Instance.gameState == Game_State_Enum.isSetting) return;

        if (GameManager.Instance.gameState == Game_State_Enum.isDie)
        {
            IsGameOver();
        }

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
            ParticleManager.Instance.AddParticle(ParticleManager.ParticleType.enemyHit, transform.position);

            collision.GetComponent<BulletMove>().Despawn();

            enemyhp -= playerData.current_attackPower;

            UpdateEnemyHP();
            if (enemyhp <= 0)
            {
                EnemyDie();
            }
        }

        if(collision.CompareTag(ConstantManager.TAG_DESBUL))
        {
            EnemyDie();
        }
    }

    private void EnemyDie()
    {
        AudioManager.Instance.EnemyDie();
        ComeOnBabyEnemy();

        ParticleManager.Instance.AddParticle(ParticleManager.ParticleType.enmeyDie, transform.position);
        ScreentHIt();
        playerData.current_attackPower += 2f;
        AddScore(score);
        GameManager.Instance.ShackeCam(0.6f, 0.2f, 13);
        enemyhp = 0;
        Destroy(gameObject);
    }

    private void ComeOnBabyEnemy()
    {
        var _rand = Random.Range(1f, 4f);
        var _randRange = 0.9f;

        for (int i = 0; i < _rand; i++)
        {
            var _randposX = Random.Range(-_randRange, _randRange);
            var _randposY = Random.Range(-_randRange, _randRange);

            var _newPos = new Vector3(transform.position.x + _randposX, transform.position.y + _randposY, 0f);
            Instantiate(childEnemy, _newPos, Quaternion.identity);
        }
    }

    private void IsGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Invoke(nameof(EnemyGameOver), 1.2f);
    }

    private void EnemyGameOver()
    {
        ParticleManager.Instance.AddParticle(ParticleManager.ParticleType.enmeyDie, transform.position);
        Destroy(gameObject);
    }

    private void FollowTextUI()
    {
        textObj.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0, 0));
    }

    private void UpdateEnemyHP()
    {
        textObj.text = $"{(int)enemyhp}";
    }

    private void AddScore(int _score)
    {
        playerData.score += _score;
    }

    private void SetRandomHPEnemy()
    {
        var _hp = Random.Range(lowhp, highhp);

        enemyhp = (int)_hp;
        score = (int)_hp;
    }

    private void ScreentHIt()
    {
        UIManager.Instance.HitScreen();
    }
}
