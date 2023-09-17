using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "Symbols", menuName = "JTE/ symbols")]
public class SymbolScriptable : ScriptableObject
{
    [SerializeField] List<SymbolContainer> symbols = new List<SymbolContainer>();

    public SymbolContainer GetSymbol(int ID)
    {
        var newList = symbols.Where(e => e.symbolID == ID).ToList();
        if(newList.Count > 0)
            return newList[0];
        return null;
    }

}


[System.Serializable]
public class SymbolContainer
{
    public Sprite symbolSprite;
    public int symbolID;
}