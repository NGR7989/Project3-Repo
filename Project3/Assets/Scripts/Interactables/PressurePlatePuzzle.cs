using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlatePuzzle : MonoBehaviour
{
    [SerializeField] List<PressurePlate> plates;
    [SerializeField] Door door;

    /*[Header("Audio")]
    [SerializeField] AudioClip pressedClip;*/

    private void Update()
    {
        bool allActive = true;
        for (int i = 0; i < plates.Count; i++)
        {
            if (plates[i].Active == false)
            {
                allActive = false;
                break;
            }
        }

        if(allActive)
        {
            door.Unlock();
        }
    }
}
