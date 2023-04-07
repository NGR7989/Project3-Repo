using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Time until looped movement can be considered")]
    [SerializeField] float timeUntilHold;
    [Tooltip("The pause between each looped movement")]
    [SerializeField] float pauseForHoldDownTime;
    [SerializeField] int distancePerMove;
    [Header("Collision")]
    [SerializeField] float checkOffset;
    [SerializeField] float checkRadius;

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
                if((heldVertical > 0 & vertical > 0) || (heldVertical < 0 & vertical < 0))
                {
                    // Same value as before so treat as held down 
                    bool canBeginLoop = true;
                    float pauseBeforeContinue = 0;

                    // Loop until held is done long enough 
                    while(pauseBeforeContinue < timeUntilHold)
                    {
                        // Check if pulled up during this time 
                        // Or if another key is pressed 
                        float loopedValue = (int)Input.GetAxis("Vertical");
                        if (Mathf.Abs(heldVertical - loopedValue) > 0.01f)
                        {
                            // Key up
                            canBeginLoop = false;
                        }

                        pauseBeforeContinue += Time.deltaTime;
                        yield return null; 
                    }

                    float counter = 0;

                    // Continues movement loop 
                    while(canBeginLoop)
                    {
                        // Check if pulled up during this time 
                        // Or if another key is pressed 

                        float loopedValue = (int)Input.GetAxis("Vertical");
                        if (Mathf.Abs(heldVertical - loopedValue) > 0.0001f)
                        {
                            // Key up
                            canBeginLoop = false;
                        }

                        if (counter >= pauseForHoldDownTime)
                        {
                            // Move player 
                            Vector3 newPos = Vector3.up * distancePerMove * loopedValue;
                            if (!IsColliding(newPos))
                            {
                                this.transform.position += newPos;
                            }
                            counter = 0;
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
                    if(vertical > 0)
                    {
                        if(!IsColliding(Vector3.up))
                        {
                            this.transform.position += Vector3.up * distancePerMove;
                        }
                    }
                    else
                    {
                        if(!IsColliding(Vector3.down))
                        {
                            this.transform.position -= Vector3.up * distancePerMove;
                        }
                    }
                }
            }
            else if(Mathf.Abs(horizontal) > 0)
            {
                // Horizontal movement 

                // Check if close enough under arbitrary value 
                if ((heldHorizontal > 0 & horizontal > 0) || (heldHorizontal < 0 & horizontal < 0))
                {
                    // Same value as before so treat as held down 
                    bool canBeginLoop = true;
                    float pauseBeforeContinue = 0;

                    // Loop until held is done long enough 
                    while (pauseBeforeContinue < timeUntilHold)
                    {
                        // Check if pulled up during this time 
                        // Or if another key is pressed 
                        float loopedValue = (int)Input.GetAxis("Horizontal");
                        if (Mathf.Abs(heldHorizontal - loopedValue) > 0.01f)
                        {
                            // Key up
                            canBeginLoop = false;
                        }

                        pauseBeforeContinue += Time.deltaTime;
                        yield return null;
                    }

                    float counter = 0;

                    // Continues movement loop 
                    while (canBeginLoop)
                    {
                        // Check if pulled up during this time 
                        // Or if another key is pressed 

                        float loopedValue = (int)Input.GetAxis("Horizontal");
                        if (Mathf.Abs(heldHorizontal - loopedValue) > 0.0001f)
                        {
                            // Key up
                            canBeginLoop = false;
                        }

                        if (counter >= pauseForHoldDownTime)
                        {
                            // Move player 
                            Vector3 newPos = Vector3.right * distancePerMove * loopedValue;
                            if (!IsColliding(newPos))
                            {
                                this.transform.position += newPos;
                            }
                            counter = 0;
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
                    if (horizontal > 0)
                    {
                        if (!IsColliding(Vector3.right))
                        {
                            this.transform.position += Vector3.right * distancePerMove;
                        }
                    }
                    else
                    {
                        if (!IsColliding(Vector3.left))
                        {
                            this.transform.position -= Vector3.right * distancePerMove;
                        }
                    }
                }
            }

            heldVertical = vertical;
            heldHorizontal = horizontal;

            yield return null;
        }
    }

    private bool IsColliding(Vector2 offset)
    {
        return Physics2D.OverlapCircle((Vector2)this.transform.position + offset, checkRadius)!= null;
    }

    private void OnDrawGizmos()
    {
        DrawCollision(Vector3.up);
        DrawCollision(Vector3.down);
        DrawCollision(Vector3.right);
        DrawCollision(Vector3.left);
    }

    private void DrawCollision(Vector3 offset)
    {
        if (IsColliding(offset))
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.white;
        }

        Gizmos.DrawWireSphere(this.transform.position + offset * checkOffset, checkRadius);
    }
}
