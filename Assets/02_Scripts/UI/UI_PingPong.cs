using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_PingPong : MonoBehaviour
{
    RectTransform rt;

    private Animator anim;
    private void Start()
    {
        rt = GetComponent<RectTransform>();
        anim = GetComponent<Animator>();

        rt.DOAnchorPosY(0, 0.2f).SetEase(Ease.OutCirc);
    }

    private void OnEnable()
    {
        Debug.Log("������");
        anim.Play("PingPong_DOWN");
    }

    private void OnDisable()
    {
        Debug.Log("������");
        anim.Play("PingPong_UP");
    }

}
