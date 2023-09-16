using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    bool tweeing;
    public void SetMoveSpeed(float toValue)
    {

        var currentValue = anim.GetFloat("MoveSpeed");
        DOTween.To(() => currentValue, x => currentValue = x, toValue, .1f).OnUpdate(() =>
        {
            anim.SetFloat("MoveSpeed", currentValue);
        });

    }
}
