using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractComponent : MonoBehaviour, IInteractable
{
    [SerializeField] bool InteractOnly;
    [SerializeField] bool PickOnly;
    public Vector2 interactionUIOffset;
    [SerializeField] UnityEvent OnInteract;

    public bool canInteract { get => InteractOnly; set => throw new System.NotImplementedException(); }
    public bool canPick { get => PickOnly; set => throw new System.NotImplementedException(); }
    Vector2 IInteractable.interactionUIOffset { get => interactionUIOffset; set => throw new System.NotImplementedException(); }

    public void Drop()
    {
    }

    public void Interact()
    {
        Debug.Log("Interacting");
        OnInteract?.Invoke();
    }

    public void PickUp()
    {
    }

    public void Reset()
    {
        return;
    }
}
