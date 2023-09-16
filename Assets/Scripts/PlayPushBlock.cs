using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayPushBlock : MonoBehaviour
{
    [SerializeField] float Distance;
    [SerializeField] float duration;
    // Start is called before the first frame update
    void Start()
    {
        var endvalue = transform.position + transform.forward * Distance;
        transform.DOMove(endvalue, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
