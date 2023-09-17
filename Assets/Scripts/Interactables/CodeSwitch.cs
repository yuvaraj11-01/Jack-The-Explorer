using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeSwitch : MonoBehaviour
{
    [SerializeField] int FirstCode, SecondCode, ThirdCode;
    [SerializeField] SymbolSlot FirstSlot, SecondSlot, ThridSlot;
    [SerializeField] Door door;

    public void OnInteract()
    {
        UIManager.Instance.ShowPassCodeWindow();
    }

    bool CheckValidCode()
    {
        if (FirstSlot.isEmpty()) return false;
        else if (FirstSlot.GetBlock().GetComponent<SymbolDragable>().symbolID != FirstCode) return false;

        if (SecondSlot.isEmpty()) return false;
        else if (SecondSlot.GetBlock().GetComponent<SymbolDragable>().symbolID != SecondCode) return false;

        if (ThridSlot.isEmpty()) return false;
        else if (ThridSlot.GetBlock().GetComponent<SymbolDragable>().symbolID != ThirdCode) return false;

        return true;
    }

    public void OnSubmit()
    {
        if (!CheckValidCode())
        {
            UIManager.Instance.ShowInvalidEntry();
            return;
        }
        UIManager.Instance.ShowvalidEntry();

        UIManager.Instance.ClosePassCodeWindow();
        door.OpenDoor();


    }


}
