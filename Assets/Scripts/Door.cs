using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    public void OpenDoor()
    {
        transform.DOScaleY(0, 0.5f);
    }
}
