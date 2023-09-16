using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform interactUIPrefab;
    [SerializeField] Camera cam;
    [SerializeField] RectTransform canvas;

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
}
