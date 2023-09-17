using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolRead : MonoBehaviour
{
    [SerializeField] SymbolScriptable symbolContainer;
    [SerializeField] int symbolID;

    public void OnInteract()
    {
        var symbol = symbolContainer.GetSymbol(symbolID);
        if (symbol == null)
        {
            Debug.Log("Null");
            return;
        }

        UIManager.Instance.ShowSymbolInteractUI(symbol.symbolSprite);

    }
}
