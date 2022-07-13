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

    [Header("점수 죽였을 때")]
    public int score;

    public float enemyhp;

    private bool isGameOver = false;

    private void Awake()
    {
        FollowTextUI();
    }

    private void Start()
    {
        SetRandomHPEnemy();
        playerData = Resources.Load<Player_data>("SO/" + "PlayerData");
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
                AudioManager.Instance.EnemyDie();
                ParticleManager.Instance.AddParticle(ParticleManager.ParticleType.enmeyDie, transform.position);
                ScreentHIt();
                playerData.current_attackPower += 2f;
                AddScore(score);
                ShackeCam(0.4f, 0.15f, 13);
                enemyhp = 0;
                Destroy(gameObject);
            }
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

    private void ShackeCam(float _dur, float _str, int _vib)
    {
        Camera.main.DOShakePosition(_dur, _str, _vib);
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
