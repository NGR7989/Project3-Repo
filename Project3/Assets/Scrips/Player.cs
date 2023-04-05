using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float timeUntilHold;
    [SerializeField] float pauseForHoldDownTime;
    [SerializeField] int distancePerMove;
    [Header("Checking")]
    [SerializeField] float checkOffset;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveCo());
    }

    private IEnumerator MoveCo()
    {
        float heldVertical = 0;
        float heldHorizontal = 0;

        while(true)
        {
            // Only allow for one input at a time 
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");

            if (Mathf.Abs(vertical) > 0)
            {
                // Vertical movement 

                // Check if close enough under arbitrary value 
                if(Mathf.Abs(heldVertical - vertical) < 0.01f)
                {
                    // Same value as before so treat as held down 

                    float pauseBeforeContinue = 0;

                    // Loop until held is done long enough 
                    while(pauseBeforeContinue < timeUntilHold)
                    {
                        // Check if pulled up during this time 
                        // Or if another key is pressed 

                        yield return null; 
                    }

                    float counter = 0;

                    // Continues movement loop 
                    while(true)
                    {
                        // Check if pulled up during this time 
                        // Or if another key is pressed 

                        if (counter >= pauseForHoldDownTime)
                        {
                            // Move player 
                        }
                        else
                        {
                            counter += Time.deltaTime;
                        }

                        yield return null;
                    }
                    
                }
                else
                {
                    // This is when the button is just pressed 

                    // New Value so treat like just pressed down 
                    heldVertical = vertical;

                    // Move player 
                    
                }
            }
            else if(Mathf.Abs(horizontal) > 0)
            {
                
            }


            // Logic for on key up 


            yield return null;
        }
    }
}
