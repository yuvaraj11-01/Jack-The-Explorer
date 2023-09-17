using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SymbolSlot : MonoBehaviour, IDropHandler
{
    RectTransform dropBlock;

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
                GetComponent<RectTransform>().anchoredPosition;
            dropBlock = eventData.pointerDrag.GetComponent<RectTransform>();
        }
    }

    public bool isEmpty()
    {
        if (dropBlock == null) return true;

        if (dropBlock.anchoredPosition !=
                GetComponent<RectTransform>().anchoredPosition) return true;

        return false;

    }

    public RectTransform GetBlock()
    {
        return dropBlock;
    }

}
