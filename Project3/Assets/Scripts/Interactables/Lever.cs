using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    [Header("Sprite Info")] 
    [SerializeField] Sprite leverOff, leverOn;
    [SerializeField] public SpriteRenderer leverRndr;

    [Header("Audio")]
    [SerializeField] AudioClip activateLever;

    bool on = false;
    public bool wasFlipped;
    public bool active = true;

    private SoundManager soundManager;
    private Vector4 offColor = new Vector4(0.7f, 0.8f, 1f, 1f);
    private Vector4 onColor = new Vector4(0.5f, 1f, .6f, 1f);

    /// <summary>
    /// Set up the lever
    /// </summary>
    void Start()
    {
        // Make the sprite match the starting state
        if (on)
        {
            leverRndr.sprite = leverOn;
            leverRndr.color = onColor;
        }
        else
        {
            leverRndr.sprite = leverOff;
            leverRndr.color = offColor;
        }

        soundManager = GameObject.FindObjectOfType<SoundManager>();
    }

    public override void Interact()
    {
        if (active) {
            // Flip the lever and show that it was flipped
            FlipLever();
            wasFlipped = true;
            soundManager.TryPlaySound("lever", activateLever, 0.9f, 1.1f);
        }
    }

    public void FlipLever()
    {
        // Check if the lever is on or not
        if (on)
        {
            // Flip the lever off
            on = false;
            leverRndr.sprite = leverOff;
            leverRndr.color = offColor;
        }
        else
        {
            // Flip the lever on
            on = true;
            leverRndr.sprite = leverOn;
            leverRndr.color = onColor;
        }
    }

    public bool IsCorrect()
    {
        return on;
    }
}
