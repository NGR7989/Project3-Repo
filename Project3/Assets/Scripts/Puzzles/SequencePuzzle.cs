using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencePuzzle : MonoBehaviour
{
    [Header("Lever Layout")]
    [SerializeField] List<Lever> levers;
    [SerializeField] float feedbackTime;
    float timeSinceFlip = 0;
    bool flippedLever;

    [Header("Room Objects")]
    [SerializeField] Door door;

    // Start is called before the first frame update
    void Start()
    {
        // Randomize sequence or reset (Stretch goal)
    }

    // Update is called once per frame
    void Update()
    {
        // Add a delay between fliping a lever and reseting
        // to give visual feedback to player
        
        // Check for a flipped lever
        foreach (Lever l in levers)
        {
            if (l.wasFlipped)
            {
                flippedLever = true;
                l.wasFlipped = false;
            }
        }
        
        // increment timer while a lever has just been flipped
        if (flippedLever)
        {
            timeSinceFlip -= Time.deltaTime;
        }


        // Check if enough time has passed since lever flip to check levers
        if (timeSinceFlip <= 0)
        {
            // Check for out of sequence lever flip
            for (int i = 1; i < levers.Count; i++)
            {
                // See if the lever before this was flipped
                if (levers[i].IsCorrect() && !levers[i - 1].IsCorrect())
                {
                    ResetLevers();
                }
            }

            // Reset timeSinceFlip
            timeSinceFlip = feedbackTime;
            flippedLever = false;
        }
        

        // Unlock the door once all levers are turned on
        //print(IsSolved());
        if (IsSolved())
        {
            door.Unlock();
            //this.gameObject.SetActive(false);
            foreach (Lever l in levers)
            {
                l.active = false;
                l.leverRndr.color = Color.gray;
            }
        }
    }

    /// <summary>
    /// Turn all levers in the array off
    /// </summary>
    void ResetLevers()
    {
        foreach (Lever l in levers)
        {
            // Check if the lever is filped on
            if (l.IsCorrect())
            {
                // Flip it off if it is on
                l.FlipLever();
            }
        }
    }

    /// <summary>
    /// Check if all the levers are correctly flipped
    /// </summary>
    /// <returns>true if puzzle is solved : else false</returns>
    bool IsSolved()
    {
        int numCorrect = 0;

        foreach (Lever l in levers)
        {
            if (l.IsCorrect()) numCorrect++;
        }

        return numCorrect == levers.Count;
    }
}
