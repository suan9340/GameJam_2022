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
    [Header("�ְ� ü��")][SerializeField] private float highhp = 10f;
    [Header("�־� ü��")][SerializeField] private float lowhp = 10f;

    [Header("UI ����")]
    [SerializeField] private Text textObj = null;

    [Header("���� �׿��� ��")]
    public int score;

    [Header("�� �׿��� �� ��� ���ݷ�")]
    public float get_attackPower = 2f;

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
        UpdateTextEnemyHP();
    }
    private void Update()
    {
        if (GameManager.Instance.gameState == Game_State_Enum.isSetting) return;

        if (GameManager.Instance.gameState == Game_State_Enum.isDie)
        {
            IsGameOver();
        }

        ReadyEnemy();
    }

    private void ReadyEnemy()
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

            PlayerDamaged();
        }
    }

    private void PlayerDamaged()
    {
        ParticleManager.Instance.AddParticle(ParticleManager.ParticleType.enemyHit, transform.position);

        enemyhp -= playerData.current_attackPower;

        UpdateTextEnemyHP();

        if (enemyhp <= 0)
        {
            EnemyDie();
        }
    }

    protected virtual void EnemyDie()
    {
        // ���� && ����Ʈ 
        AudioManager.Instance.EnemyDie();
        ParticleManager.Instance.AddParticle(ParticleManager.ParticleType.enmeyDie, transform.position);

        // ī�޶� ����ŷ �� �ǰ� �׵θ� ó��
        GameManager.Instance.ShackeCam(0.5f, 0.2f, 13);
        ScreentHIt();

        // �÷��̾��� ���ݷ� ����
        playerData.current_attackPower += get_attackPower;


        // �����߰� �� ������ ����
        AddScore(score);
        UIManager.Instance.UpdateUI();


        enemyhp = 0;
        Destroy(gameObject);
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

    private void UpdateTextEnemyHP()
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
