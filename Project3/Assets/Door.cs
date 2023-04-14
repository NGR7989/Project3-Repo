using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : TextPopup
{
    [Header("Door")]
    [SerializeField] Color unlockedColor;
    [SerializeField] Color lockedColor;
    [SerializeField] SpriteRenderer renderer;

    private bool unlocked = false;

    public override void Interact()
    {
        if(unlocked)
        {
            // Load next room 
        }
        else
        {
            // Runs popup text 
            base.Interact();
        }
    }

    /// <summary>
    /// Call this function to unlock the door 
    /// </summary>
    public void Unlock()
    {
        unlocked = true;

        // Play the audio to unlock
    }

    // Update is called once per frame
    void Update()
    {
        if(unlocked)
        {
            renderer.color = unlockedColor; 
        }
        else
        {
            renderer.color = lockedColor;
        }    
    }
}
