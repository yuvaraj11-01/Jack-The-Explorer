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

    public void CloseDoor()
    {
        transform.DOScaleY(1, 0.5f);
    }


}
