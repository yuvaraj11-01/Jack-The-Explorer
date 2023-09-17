using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SymbolDragable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] Canvas canvas;
    public int symbolID;

    RectTransform _rectTransform;
    CanvasGroup group;
    Vector2 initPos;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        group = GetComponent<CanvasGroup>();
        initPos = _rectTransform.anchoredPosition;
    }

    private void OnEnable()
    {
        _rectTransform.anchoredPosition = initPos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        group.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        group.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}
