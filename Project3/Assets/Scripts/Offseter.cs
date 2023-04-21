using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offseter : Interactable
{
    [Header("Offseter Details")]
    [SerializeField] float disToInfluence;
    [SerializeField] float radius;
    [SerializeField] LayerMask layer;

    [Header("State Details")]
    [SerializeField] States currentState;

    // Colors should not be the hard values but something a 
    // little tinted 
    [SerializeField] Color red;
    [SerializeField] Color green;
    [SerializeField] Color yellow;

    private Player player;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }


    public override void Interact()
    {
        // Try to influence the neighbors 
        TryInteractWithNeighbor();

        // Change the current state 
        ChangeState();
    }

    /// <summary>
    /// Influences the offseter in the same direction that 
    /// the player is facing 
    /// </summary>
    private void TryInteractWithNeighbor()
    {
        // Get the direction you want to influence 
        Vector3 dir = player.InteractionDir;

        // Check if there is another offseter there 
        Collider2D col = Physics2D.OverlapCircle(dir * disToInfluence, radius);

        if(col != null)
        {
            // Something to detect 
            Offseter offseter = col.GetComponent<Offseter>();

            if(offseter != null)
            {
                // Make sure it has the component 
                offseter.Interact();
            }
        }
    }

    /// <summary>
    /// Changes the current colorstate
    /// </summary>
    private void ChangeState()
    {
        if (currentState == States.yellow)
        {
            // Wrap back around 
            print(States.red);
        }
        else
        {
            print(currentState + 1);
        }
    }

    private enum States
    {
        red,
        green,
        yellow
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position + Vector3.up * disToInfluence, radius);
        Gizmos.DrawWireSphere(this.transform.position + Vector3.right * disToInfluence, radius);
        Gizmos.DrawWireSphere(this.transform.position + Vector3.left * disToInfluence, radius);
        Gizmos.DrawWireSphere(this.transform.position + Vector3.down * disToInfluence, radius);
    }
}
