using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using System.Linq;

public class MovePlatformer : MonoBehaviour
{

    [SerializeField] List<Vector3> waypoints;
    [SerializeField] float moveSpeed = 10;
    bool canMove;
    TweenerCore<Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> ob;

    public void Active()
    {
        canMove = true;
        ob?.Play();
    }

    public void DeActive()
    {
        canMove = false;
        ob?.Pause();
    }

    private void Start()
    {
        ob = transform.DOPath(waypoints.ToArray(), moveSpeed, PathType.Linear, PathMode.Full3D, gizmoColor: Color.red).SetLoops(-1).SetEase(Ease.Linear);
        ob.Pause();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }

}
