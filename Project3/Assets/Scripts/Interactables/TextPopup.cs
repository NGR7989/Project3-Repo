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
        print(dm);
    }

    public override void Interact()
    {
        dm.TryRun(text);
        print("trrying to run");
    }
}
