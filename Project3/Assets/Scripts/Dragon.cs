using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : TextPopup
{
    [Header("Dragon")]
    [SerializeField] KeyCode continueKey;
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] float phaseOutSpeed;
    [SerializeField] GameObject door;
    [SerializeField] SoundManager soundManager;

    private bool activeDialogue = false;

    public override void Interact()
    {
        if(!activeDialogue)
        {
            base.Interact();
            soundManager.ChangeMusicTrack(1);
            StartCoroutine(ContinueAfterDialogue());
        }
    }

    /// <summary>
    /// Waits untill the dialogue ends to continue 
    /// </summary>
    /// <returns></returns>
    private IEnumerator ContinueAfterDialogue()
    {
        // Counts how many times pressed up
        // This is not the best way to do this but 
        // time constraints 
        int counter = 0;

        while(counter < text.Count)
        {
            if (Input.GetKeyUp(continueKey))
            {
                counter++;
            }

            yield return null; 
        }

        StartCoroutine(PhaseOut());

        while (counter <= text.Count)
        {
            if (Input.GetKeyUp(continueKey))
            {
                counter++;
            }

            yield return null;
        }

        // End
        Destroy(this.gameObject);
        door.SetActive(true);
    }

    /// <summary>
    /// Phases the dragon's color out 
    /// </summary>
    /// <returns></returns>
    private IEnumerator PhaseOut()
    {
        float lerp = 0;

        Color startColor = renderer.color;
        while(lerp <= 1)
        {
            renderer.color = Color.Lerp(startColor, Color.clear, lerp);

            lerp += Time.deltaTime * phaseOutSpeed;
            yield return null;
        }
    }
}
