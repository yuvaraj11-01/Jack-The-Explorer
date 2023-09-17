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
    [SerializeField] RectTransform PassCodeWindow;
    [SerializeField] GameObject errorText, successText;


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
            SymbolInfoDisplay.gameObject.SetActive(false);

        });

    }

    public void ShowPassCodeWindow()
    {
        PlayerStateMachineComponent.Instance.pausePlayer();

        PassCodeWindow.localScale = Vector3.zero;
        PassCodeWindow.gameObject.SetActive(true);
        errorText.SetActive(false);
        successText.SetActive(false);
        PassCodeWindow.DOScale(1, 0.5f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            PlayerDialogueReferances.Instance.OnOpenSymbolPasscode.SetActive(true);
            PlayerDialogueReferances.Instance.OnOpenSymbolPasscode.GetComponent<DialogueManager>();
        });
    }

    public void ClosePassCodeWindow()
    {

        PassCodeWindow.DOScale(0, 0.5f).SetEase(Ease.InSine).OnComplete(() =>
        {

            PlayerStateMachineComponent.Instance.UnpausePlayer();
            PassCodeWindow.gameObject.SetActive(false);


        });

    }

    public void ShowInvalidEntry()
    {
        errorText.SetActive(true);
        successText.SetActive(false);
    }

    public void ShowvalidEntry()
    {
        errorText.SetActive(false);
        successText.SetActive(true);
    }

}

