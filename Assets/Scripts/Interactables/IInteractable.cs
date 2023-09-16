using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public bool canInteract { get; set; }
    public void Interact();
    public Vector2 interactionUIOffset { get; set; }

    public bool canPick { get; set; }
    public void PickUp();
    public void Drop();

    public void Reset();

}