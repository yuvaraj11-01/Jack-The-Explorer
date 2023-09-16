using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    IInteractable avilableInteraction;
    InputControll inputs;
    RectTransform UiObject;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IInteractable>( out IInteractable interactable) && avilableInteraction == null)
        {
            Debug.Log($"[PlayerInteraction] Enter {other.name}");
            UiObject = UIManager.Instance.ShowInteractUI(other.transform.position);
            avilableInteraction = interactable;
            UiObject.position += (Vector3)avilableInteraction.interactionUIOffset;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            if (interactable == avilableInteraction)
            {
                Debug.Log($"[PlayerInteraction] Exit {other.name}");
                if (UiObject)
                    Destroy(UiObject.gameObject);
                avilableInteraction = null;
            }
        }
    }


    private void Awake()
    {
        inputs = new InputControll();
        inputs.PlayerMap.Enable();

        inputs.PlayerMap.Interact.performed += Interact_performed;

    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (avilableInteraction != null)
        {
            avilableInteraction.Interact();
            if(UiObject)
                Destroy(UiObject.gameObject);

        }
        else Debug.LogWarning("Interaction not found");
    }
}
