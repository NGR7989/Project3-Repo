using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopup : Interactable
{
    [SerializeField] string text;

    public override void Interact()
    {
        print(text);
    }
}
