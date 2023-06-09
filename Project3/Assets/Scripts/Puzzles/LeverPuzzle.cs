using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    [Header("Lever Layout")]
    [SerializeField] List<Lever> levers;
    [SerializeField] int rows, columns;

    [Header("Room Objects")]
    [SerializeField] Door door;

    // Start is called before the first frame update
    void Start()
    {
        // Array Example setup with 3 rows and 4 columns
        // {
        //    {levers[0], levers[1], levers[2],  levers[3]}
        //      [0,0]       [0,1]       [0,2]       [0,3]
        //
        //    {levers[4], levers[5], levers[6],  levers[7]}
        //      [1,0]       [1,1]       [1,2]       [1,3]
        //
        //    {levers[8], levers[9], levers[10], levers[11]}
        //      [2,0]       [2,1]       [2,2]       [2,3]
        // }
        // Coordinate decoder (y * columns) + x

        // Check to see if there are enought levers for the grid size
        if (rows * columns != levers.Count)
        {
            throw new System.Exception("Error: Grid size does not match number of levers - "
                + "\nA " + rows + "x" + columns + " doesn't fit"
                + "\n" + levers.Count + " Levers");
        }


        /*
         * Test out starting with all the levers off to decrease the challenge of the puzzle.
         * If that doesnt work just uncomment the loop below
         */
        //// Make sure levers don't start solved
        //do
        //{
        //    // Randomize lever starting points
        //    for (int i = 0; i < levers.Count; i++)
        //    {
        //        if (Random.Range(0, 2) == 1)
        //        {
        //            levers[i].FlipLever();
        //            //print("Lever On");
        //        }
        //        //else print("Lever Off");
        //    }
        //} while (IsSolved());
    }

    // Update is called once per frame
    void Update()
    {
        // Check for lever flips and flip adjacent levers
        for (int i = 0; i < levers.Count; i++)
        {
            if (levers[i].wasFlipped)
            {
                // Recet lever flip
                levers[i].wasFlipped = false;

                FlipAdjacentLevers(i);
            }
        }

        // Open door if solved
        //print(IsSolved());
        if (IsSolved()) 
        {
            door.Unlock();
            //this.gameObject.SetActive(false);
            foreach (Lever l in levers) {
                l.active = false;
                l.leverRndr.color = Color.gray;
            }
        }
    }

    void FlipAdjacentLevers(int index)
    {
        // Get adjacent lever indecies
        int tLever, rLever, bLever, lLever;

        tLever = index - columns;
        rLever = index + 1;
        bLever = index + columns;
        lLever = index - 1;

        // Check to see if there is a top lever
        if (tLever >= 0 && tLever < levers.Count)
        {
            levers[tLever].FlipLever();
        }
        // Check to see if there is a right lever
        if ((rLever >= 0 && rLever < levers.Count) && index % columns != columns - 1)
        {
            levers[rLever].FlipLever();
        }
        // Check to see if there is a bottom lever
        if (bLever >= 0 && bLever < levers.Count)
        {
            levers[bLever].FlipLever();
        }
        // Check to see if there is a left lever
        if ((lLever >= 0 && lLever < levers.Count) && index % columns != 0)
        {
            levers[lLever].FlipLever();
        }
    }

    public bool IsSolved()
    {
        int numCorrect = 0;

        foreach (Lever l in levers)
        {
            if (l.IsCorrect()) numCorrect++;
        }

        return numCorrect == levers.Count;
    }
}
