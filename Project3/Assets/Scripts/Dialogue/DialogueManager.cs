using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject textBox;

    private bool isRunning; 

    public void TryRun(List<string> text)
    {
        if(isRunning)
        {
            return;
        }
    }
}
