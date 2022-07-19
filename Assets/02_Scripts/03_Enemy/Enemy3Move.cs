using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Enemy3Move : EnemyMove
{
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polycollider;

    private readonly WaitForSeconds kambackDelay = new WaitForSeconds(1.8f);

    protected override void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polycollider = GetComponent<PolygonCollider2D>();

        base.Start();

        StartCoroutine(KamBacKingKing());
    }

    protected override void EnemyDamaged()
    {
        base.EnemyDamaged();
    }

    private IEnumerator KamBacKingKing()
    {
        var _time = 0.5f;

        while (true)
        {
            KamBak_FadeIn(_time);
            yield return kambackDelay;


            KamBak_FadeOut(_time);
            yield return kambackDelay;
        }
    }

    private void KamBak_FadeIn(float _time)
    {
        spriteRenderer.DOFade(0, _time).OnComplete(() => { polycollider.enabled = false; });
        textObj.DOFade(0, _time);
    }

    private void KamBak_FadeOut(float _time)
    {
        spriteRenderer.DOFade(1, _time).OnComplete(() => { polycollider.enabled = true; });
        textObj.DOFade(1, _time);
    }
}
