using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject textBox;
    [SerializeField] Player player;
    [SerializeField] KeyCode nextKey;

    private bool isRunning = false;

    public bool Running { get { return isRunning; } }


    /// <summary>
    /// Attempt to run dialogue to the screen. If 
    /// there is currently being dialogue written to the 
    /// screen then it will not run
    /// </summary>
    /// <param name="text"></param>
    public void TryRun(List<string> text)
    {
        if(isRunning)
        {
            return;
        }
        StartCoroutine(RunText(text));
    }

    private IEnumerator RunText(List<string> text)
    {
        isRunning = true;
        player.CanMove = false;

        int index = 0;
        TextMeshProUGUI mesh = textBox.GetComponentInChildren<TextMeshProUGUI>();
        textBox.SetActive(true);
        mesh.text = text[index];

        // If not included it will go into the main loop with key down 
        while (true)
        {
            // Loops until next key 
            if (Input.GetKeyUp(nextKey))
            {
                // Only continues once key is back up 
                break;
            }

            yield return null;
        }

        do
        {
            mesh.text = text[index];

            while (true)
            {
                // Loops until next key 
                if(Input.GetKeyDown(nextKey))
                {
                    index++;
                    print(index);
                    break;
                }

                yield return null;
            }

            while (true)
            {
                // Loops until next key 
                if (Input.GetKeyUp(nextKey))
                {
                    // Only continues once key is back up 
                    break;
                }

                yield return null;
            }

        } while (index < text.Count);

        textBox.SetActive(false);
        isRunning = false;
        player.CanMove = true;
    }
}
