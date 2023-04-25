using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextImagePopup : Interactable
{
    [SerializeField] protected List<string> text;
    [SerializeField] GameObject image;

    private DialogueManager dm;
    private bool showing = false;

    private void Start()
    {
        dm = GameObject.FindObjectOfType<DialogueManager>();
        print(dm);
    }

    private void Update()
    {
        if (showing && !dm.Running)
        {
            image.SetActive(false);
            showing = false;
        }
    }

    public override void Interact()
    {
        if (!showing)
        {
            dm.TryRun(text);
            image.SetActive(true);
            showing = true;
            print("trrying to run");
        }
        
    }
}
