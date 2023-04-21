using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPuzzle : MonoBehaviour
{
    [Header("Spinners")]
    [SerializeField] List<Spinner> spinners;

    [Header("Room Objects")]
    [SerializeField] Door door;

    // Start is called before the first frame update
    void Start()
    {
        // Randomize directions of spinners
        do
        {
            foreach (Spinner s in spinners)
            {
                s.RandomizeDirection();
            }
        }
        while (IsSolved());
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSolved())
        {
            door.Unlock();
            foreach(Spinner s in spinners)
            {
                s.active = false;
                s.spriteRndr.color = Color.gray;
            }
        }
    }

    private bool IsSolved()
    {
        int numCorrect = 0;

        for (int i = 0; i < spinners.Count; i++)
        {
            if (spinners[i].IsCorrect())
            {
                numCorrect++;
            }
        }

        return numCorrect == spinners.Count;
    }
}
