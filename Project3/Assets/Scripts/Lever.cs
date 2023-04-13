using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [Header("Sprite Info")]
    [SerializeField] Sprite leverOff, leverOn;
    [SerializeField] SpriteRenderer leverRndr;

    bool on = false;

    /// <summary>
    /// Set up the lever
    /// </summary>
    void Start()
    {
        // Make the sprite match the starting state
        if (on) leverRndr.sprite = leverOn;
        else leverRndr.sprite = leverOff;
    }

    public void FlipLever()
    {
        // Check if the lever is on or not
        if (on)
        {
            // Flip the lever off
            on = false;
            leverRndr.sprite = leverOff;
        }
        else
        {
            // Flip the lever on
            on = true;
            leverRndr.sprite = leverOn;
        }
    }

    public bool IsCorrect()
    {
        return on;
    }
}
