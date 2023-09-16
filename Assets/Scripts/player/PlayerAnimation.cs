using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    [SerializeField] Rigidbody Prb;
    [SerializeField] Collider Pcoll;
    [SerializeField] Transform animRoot;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        DisableRagdoll();
        //SetKinamatic(true);

    }

    public void EnableRagdoll()
    {
        anim.enabled = false;
        Prb.isKinematic = true;
        Pcoll.enabled = false;
        SetKinamatic(false);
    }

    public void DisableRagdoll()
    {
        anim.enabled = true;
        Prb.isKinematic = false;
        Pcoll.enabled = true;
        SetKinamatic(true);
    }

    void SetKinamatic(bool value)
    {
        var rbs = animRoot.GetComponentsInChildren<Rigidbody>();
        foreach (var item in rbs)
        {
            item.isKinematic = value;
        }
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
