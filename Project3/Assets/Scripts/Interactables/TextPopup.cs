using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopup : Interactable
{
    [SerializeField] protected List<string> text;

    private DialogueManager dm;

    private void Start()
    {
        dm = GameObject.FindObjectOfType<DialogueManager>();
    }

    public override void Interact()
    {
        dm.TryRun(text);
    }
}
