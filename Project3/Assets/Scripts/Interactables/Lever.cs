using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    [Header("Sprite Info")]
    [SerializeField] Sprite leverOff, leverOn;
    [SerializeField] SpriteRenderer leverRndr;

    private Sprite player;
    bool on = false;

    /// <summary>
    /// Set up the lever
    /// </summary>
    void Start()
    {
        // Get reference to player
        player = GameObject.FindObjectOfType<Player>().GetComponent<Sprite>();

        // Make the sprite match the starting state
        if (on) leverRndr.sprite = leverOn;
        else leverRndr.sprite = leverOff;
    }

    public override void Interact()
    {
        // Test code
        print("Interacting with - " + this.name);

        // Flip the lever if the player is colliding with it
        //if (IsColliding())
        //    FlipLever();
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

    private bool IsColliding()
    {
        float pMinX, pMaxX, pMinY, pMaxY;
        float lMinX, lMaxX, lMinY, lMaxY;

        // Get player bounds
        pMinX = player.bounds.min.x;
        pMaxX = player.bounds.max.x;
        pMinY = player.bounds.min.y;
        pMaxY = player.bounds.max.y;

        // GYt the lever bounds
        lMinX = GetComponent<Sprite>().bounds.min.x;
        lMaxX = GetComponent<Sprite>().bounds.max.x;
        lMinY = GetComponent<Sprite>().bounds.min.y;
        lMaxY = GetComponent<Sprite>().bounds.max.y;

        // CYeck for collision
        if ((lMaxX > pMinX) && (pMaxX > lMinX) && (lMaxY > pMinY) && (pMaxY > lMinY))
            return true;
        else
            return false;
    }
}
