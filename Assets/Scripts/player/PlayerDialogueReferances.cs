using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueReferances : MonoBehaviour
{

    public static PlayerDialogueReferances Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    public GameObject OnreadSymbol;
    public GameObject OnOpenSymbolPasscode;


}
