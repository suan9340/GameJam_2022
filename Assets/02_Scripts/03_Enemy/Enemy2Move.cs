using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Enemy2Move : EnemyMove
{
    [Header("응애아가 적")]
    public GameObject childEnemy = null;

    protected override void EnemyDie()
    {
        base.EnemyDie();
        ComeOnBabyEnemy();

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
}
