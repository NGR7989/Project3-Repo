using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("The pause between each looped movement")]
    [SerializeField] float pauseForHoldDownTime;
    [SerializeField] float distancePerMove;
    [Header("Collision")]
    [SerializeField] float checkOffset;
    [SerializeField] float checkRadius;
    [Header("Interaction")]
    [SerializeField] KeyCode interactionKey;
    [SerializeField] LayerMask interactionLayer;

    private Vector3 currentDir = Vector3.zero;
    private Vector3 interactionDir = Vector3.down; // For interaction 

    public bool CanMove { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        CanMove = true;

        // Begin movement coroutines 
        StartCoroutine(MoveDir(Vector3.up, KeyCode.W));
        StartCoroutine(MoveDir(Vector3.down, KeyCode.S));
        StartCoroutine(MoveDir(Vector3.right, KeyCode.D));
        StartCoroutine(MoveDir(Vector3.left, KeyCode.A));
    }

    private void Update()
    {
        Interaction();
    }

    #region Movement  
    /// <summary>
    /// The coroutine the overrides a direction to be moved
    /// and continues to move the player after a pause 
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    private IEnumerator MoveDir(Vector3 dir, KeyCode key)
    {
        float holdDownTime = 0;

        while(true)
        {
            if(!CanMove)
            {
                yield return null;
            }

            if(Input.GetKeyDown(key))
            {
                currentDir = dir;
                interactionDir = dir;

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

    /// <summary>
    /// Used to visualize an anticipated collision 
    /// </summary>
    /// <param name="offset"></param>
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
    #endregion

    /// <summary>
    /// Runs the logic of whether the player is to 
    /// interact with an object in the direction
    /// they last moved 
    /// </summary>
    private void Interaction()
    {
        if (!CanMove)
        {
            return;
        }

        if(Input.GetKeyDown(interactionKey))
        {
            // Try for collision which is interactable 
            if(IsColliding(interactionDir * checkOffset))
            {
                // Try to get interactable 
                Interactable interactable = GetInteractable(interactionDir * checkOffset);

                if(interactable != null)
                {
                    // Activates interactable if possible 
                    interactable.Interact();
                }
            }
        }
    }

    /// <summary>
    /// Checks whether if in a certain direction the player
    /// is colliding with an object 
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    private bool IsColliding(Vector2 offset)
    {
        return Physics2D.OverlapCircle((Vector2)this.transform.position + offset, checkRadius) != null;
    }

    /// <summary>
    /// Get an interactable if possible towards the 
    /// last moved direction 
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    private Interactable GetInteractable(Vector2 offset)
    {
        Collider2D collider = Physics2D.OverlapCircle((Vector2)this.transform.position + offset, checkRadius, interactionLayer);
        return collider == null ? null : collider.GetComponent<Interactable>();
    }

    private void OnDrawGizmos()
    {
        DrawCollision(Vector3.up);
        DrawCollision(Vector3.down);
        DrawCollision(Vector3.right);
        DrawCollision(Vector3.left);
    }

  
}
