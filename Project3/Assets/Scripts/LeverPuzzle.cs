using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    [Header("Lever Layout")]
    [SerializeField] List<Lever> levers;
    [SerializeField] int rows, columns;

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
        
    }
}
