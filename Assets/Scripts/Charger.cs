using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Charger : MonoBehaviour
{
    public UnityEvent OnActive;

    public void Activate()
    {
        OnActive?.Invoke();
    }
}
