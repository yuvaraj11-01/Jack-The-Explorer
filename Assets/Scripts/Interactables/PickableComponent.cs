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
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider coll;
    public Vector2 interactionUIOffset;
    InputControll inputs;

    public bool canInteract { get => InteractOnly; set => throw new System.NotImplementedException(); }
    public bool canPick { get => PickOnly; set => throw new System.NotImplementedException(); }
    Vector2 IInteractable.interactionUIOffset { get => interactionUIOffset; set => throw new System.NotImplementedException(); }

    public bool picked;

    Vector3 initPos;

    public void Drop()
    {
        if (!picked) return;

        //Ray ray = new Ray() { direction = transform.forward, origin = transform.position };
        //if (Physics.Raycast(ray, 2, detectLayers))
        //{
        //    Debug.Log("----");
        //    return;
        //}

        var currentValue = pickUpRig.weight;
        DOTween.To(() => currentValue, x => currentValue = x, 0, .1f).OnUpdate(() =>
        {
            pickUpRig.weight = currentValue;
        }).OnComplete(() => {
            transform.parent = null;
            transform.position = pickedPos.root.position + pickedPos.root.forward;
            picked = false;
            inputs.PlayerMap.Interact.performed -= Interact_performed;
            isRegistered = false;
            rb.isKinematic = false;
            coll.enabled = true;
        });
    }

    public void Reset()
    {
        pickUpRig.weight = 0;

        transform.SetParent(null, true);
        transform.position = initPos;
        picked = false;
        rb.isKinematic = false;
        coll.enabled = true;
    }

    private void OnEnable ()
    {
        Reset();
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

    bool isRegistered;

    public void PickUp()
    {
        var currentValue = pickUpRig.weight;
        DOTween.To(() => currentValue, x => currentValue = x, 1, .1f).OnUpdate(() =>
        {
            pickUpRig.weight = currentValue;
        }).OnComplete(()=> {
            transform.parent = pickedPos;
            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.identity;
            if (!isRegistered)
            {
                inputs.PlayerMap.Interact.performed += Interact_performed;
                isRegistered = true;
            }
            rb.isKinematic = true;
            coll.enabled = false;
        });
        
    }

    private void Awake()
    {
        inputs = new InputControll();
        inputs.PlayerMap.Enable();

        initPos = transform.position;
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (picked) Drop();
    }
}
