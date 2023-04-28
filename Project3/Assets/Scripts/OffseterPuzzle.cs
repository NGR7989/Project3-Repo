using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffseterPuzzle : MonoBehaviour
{

    [SerializeField] List<Offseter> items;
    [SerializeField] Door door;
    [SerializeField] Transform player;
    bool isComplete = false;


    // Update is called once per frame
    void Update()
    {
        if(!isComplete)
        {
            bool allGood = true;
            for (int i = 0; i < items.Count; i++)
            {
                if(!items[i].IsCorrect)
                {
                    allGood = false;
                    break;
                }
            }

            if (allGood)
            {
                isComplete = true;
                door.Unlock();
            }
        }
    }
}
