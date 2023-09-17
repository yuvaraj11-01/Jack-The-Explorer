using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using HeneGames.DialogueSystem;


public class UIManager : MonoBehaviour
{
    [SerializeField] Transform interactUIPrefab;
    [SerializeField] Camera cam;
    [SerializeField] RectTransform canvas;
    [SerializeField] RectTransform SymbolInfoDisplay;

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    public RectTransform ShowInteractUI(Vector3 ObjectPos)
    {
        var uiObj = Instantiate(interactUIPrefab, canvas) as RectTransform;
        uiObj.position = cam.WorldToScreenPoint(ObjectPos);
        return uiObj;
    }

    public void ShowSymbolInteractUI(Sprite symbolSprite)
    {
        PlayerStateMachineComponent.Instance.pausePlayer();

        SymbolInfoDisplay.localScale = Vector3.zero;
        SymbolInfoDisplay.Find("SymbolImage").GetComponent<Image>().sprite = symbolSprite;
        SymbolInfoDisplay.gameObject.SetActive(true);
        SymbolInfoDisplay.DOScale(1, 0.5f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            PlayerDialogueReferances.Instance.OnreadSymbol.SetActive(true);
            PlayerDialogueReferances.Instance.OnreadSymbol.GetComponent<DialogueManager>().endDialogueEvent.AddListener(OnreadFinish);
        });

    }

    void OnreadFinish()
    {

        SymbolInfoDisplay.DOScale(0, 0.5f).SetEase(Ease.InSine).OnComplete(() =>
        {
            PlayerDialogueReferances.Instance.OnreadSymbol.SetActive(false);
            PlayerDialogueReferances.Instance.OnreadSymbol.GetComponent<DialogueManager>().endDialogueEvent.RemoveListener(OnreadFinish);
            PlayerStateMachineComponent.Instance.UnpausePlayer();

        });

    }


}

