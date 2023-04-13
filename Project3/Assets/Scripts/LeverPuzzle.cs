using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    [Header("Lever Layout")]
    [SerializeField] List<Lever> levers;
    [SerializeField] int rows, columns;

    bool solved = false;

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

        // Randomize lever starting points
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                if (Random.Range(0, 2) == 1)
                {
                    levers[(y * columns) + x].FlipLever();
                    print("Lever On");
                }
                else print("Lever Off");
            }
        }

        
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

                // Get adjacent lever indecies
                int tLever, rLever, bLever, lLever;

                tLever = i - columns;
                rLever = i + 1;
                bLever = i + columns;
                lLever = i - 1;

                // Check to see if there is a top lever
                if (tLever >= 0 && tLever < levers.Count)
                {
                    levers[tLever].FlipLever();
                }
                // Check to see if there is a right lever
                if ((rLever >= 0 && rLever < levers.Count) && i % columns != columns - 1)
                {
                    levers[rLever].FlipLever();
                }
                // Check to see if there is a bottom lever
                if (bLever >= 0 && bLever < levers.Count)
                {
                    levers[bLever].FlipLever();
                }
                // Check to see if there is a left lever
                if ((lLever >= 0 && lLever < levers.Count) && i % columns != 0)
                {
                    levers[lLever].FlipLever();
                }
            }
        }

        // Open door if solved
        print(IsSolved());
    }

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
