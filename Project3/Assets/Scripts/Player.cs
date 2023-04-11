using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("The pause between each looped movement")]
    [SerializeField] float pauseForHoldDownTime;
    [SerializeField] int distancePerMove;
    [Header("Collision")]
    [SerializeField] float checkOffset;
    [SerializeField] float checkRadius;

    private Vector3 currentDir = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveDir(Vector3.up, KeyCode.W));
        StartCoroutine(MoveDir(Vector3.down, KeyCode.S));
        StartCoroutine(MoveDir(Vector3.right, KeyCode.D));
        StartCoroutine(MoveDir(Vector3.left, KeyCode.A));
    }

    private IEnumerator MoveDir(Vector3 dir, KeyCode key)
    {
        float holdDownTime = 0;

        while(true)
        {
            if(Input.GetKeyDown(key))
            {
                currentDir = dir;

                // Check before moving 
                if (!IsColliding(dir * distancePerMove))
                {
                    this.transform.position += dir * distancePerMove;
                }
            }

            if(Input.GetKey(key) && currentDir.Equals(dir))
            {
                // Repeat while the same direction 
                holdDownTime += Time.deltaTime;

                if(holdDownTime >= pauseForHoldDownTime)
                {
                    holdDownTime = 0;

                    // Check before moving 
                    if(!IsColliding(dir * distancePerMove))
                    {
                        this.transform.position += dir * distancePerMove;
                    }
                }
            }
            else if(Input.GetKeyUp(key))
            {
                holdDownTime = 0;

                if(currentDir.Equals(dir))
                {
                    // Resets if no other key is pressed 
                    currentDir = Vector3.zero;
                }
            }
            else
            {
                holdDownTime = 0;
            }

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
