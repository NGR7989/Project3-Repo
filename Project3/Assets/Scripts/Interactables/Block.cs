using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Interactable
{
    [SerializeField] int distancePerMove;
    [SerializeField] float checkRadius;

    private Player player;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    public override void Interact()
    {
        // When interacted with this object is attempted to be moved
        Vector3 newPos = player.InteractionDir * distancePerMove;

        if(!IsColliding(newPos))
        {
            // Moves forward if possible 
            this.transform.position += newPos;
        }
    }

    /// <summary>
    /// Checks whether if in a certain direction the block
    /// is colliding with an object 
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    private bool IsColliding(Vector2 offset)
    {
        return Physics2D.OverlapCircle((Vector2)this.transform.position + offset, checkRadius) != null;
    }
}
