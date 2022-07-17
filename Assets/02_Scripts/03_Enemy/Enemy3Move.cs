using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Enemy3Move : EnemyMove
{
    protected override void EnemyDamaged()
    {
        base.EnemyDamaged();

        BackPos();
    }

    private void BackPos()
    {
        var _currentPos = transform.position;
    }
}
