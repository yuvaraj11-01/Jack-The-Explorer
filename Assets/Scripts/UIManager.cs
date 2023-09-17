using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform interactUIPrefab;
    [SerializeField] Camera cam;
    [SerializeField] RectTransform canvas;
    [SerializeField] RectTransform SymbolInfoDisplay;

    public static UIManager Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }


    public RectTransform ShowInteractUI(Vector3 ObjectPos)
    {
        var uiObj = Instantiate(interactUIPrefab, canvas) as RectTransform;
        uiObj.position = cam.WorldToScreenPoint(ObjectPos);
        return uiObj;
    }

    public void ShowSymbolInteractUI()
    {
        SymbolInfoDisplay.localScale = Vector3.zero;
        SymbolInfoDisplay.gameObject.SetActive(true);
        SymbolInfoDisplay.DOScale(1, 0.5f).SetEase(Ease.OutSine);
    }

}
