using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlates : MonoBehaviour
{
    [SerializeField] bool active;

    [SerializeField] LayerMask layer;

    public UnityEvent OnActive, OnDeActive;

    void getBoxes()
    {
        var colls = Physics.OverlapBox(transform.position, (transform.localScale / 2) + Vector3.up, Quaternion.identity, layer);
        if (colls.Length > 0)
        {
            active = true;
            OnActive?.Invoke();
        }
        else
        {
            active = false;
            OnDeActive?.Invoke();
        }
    }

    private void Update()
    {
        getBoxes();
    }

}
