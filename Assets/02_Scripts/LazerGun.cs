using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerGun : MonoBehaviour
{
    public Animator anim;

    private void OnEnable()
    {
        anim.Play("rotate");
    }

    private void OnDisable()
    {
        
    }
}
