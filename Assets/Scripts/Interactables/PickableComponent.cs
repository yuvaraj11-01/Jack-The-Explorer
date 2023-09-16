using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using DG.Tweening;

public class PickableComponent : MonoBehaviour, IInteractable
{
    [SerializeField] bool InteractOnly;
    [SerializeField] bool PickOnly;
    [SerializeField] Rig pickUpRig;
    [SerializeField] Transform pickedPos;
    [SerializeField] LayerMask detectLayers;
    public Vector2 interactionUIOffset;
    InputControll inputs;

    public bool canInteract { get => InteractOnly; set => throw new System.NotImplementedException(); }
    public bool canPick { get => PickOnly; set => throw new System.NotImplementedException(); }
    Vector2 IInteractable.interactionUIOffset { get => interactionUIOffset; set => throw new System.NotImplementedException(); }

    public bool picked;

    public void Drop()
    {
        if (!picked) return;

        Ray ray = new Ray() { direction = transform.forward, origin = transform.position };
        if (Physics.Raycast(ray, 2, detectLayers))
        {
            Debug.Log("----");
            return;
        }

        var currentValue = pickUpRig.weight;
        DOTween.To(() => currentValue, x => currentValue = x, 0, .1f).OnUpdate(() =>
        {
            pickUpRig.weight = currentValue;
        }).OnComplete(() => {
            transform.parent = null;
            transform.position = pickedPos.root.position + pickedPos.root.forward;
            picked = false;

        });
    }

    public void Interact()
    {
        if (!PickOnly) return;

        if (!picked)
        {
            PickUp();
            picked = true;
        }
    }

    public void PickUp()
    {
        var currentValue = pickUpRig.weight;
        DOTween.To(() => currentValue, x => currentValue = x, 1, .1f).OnUpdate(() =>
        {
            pickUpRig.weight = currentValue;
        }).OnComplete(()=> {
            transform.parent = pickedPos;
            transform.localPosition = Vector3.zero;
        });
        
    }

    private void Awake()
    {
        inputs = new InputControll();
        inputs.PlayerMap.Enable();

        inputs.PlayerMap.Interact.performed += Interact_performed; ;

    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (picked) Drop();
    }
}
