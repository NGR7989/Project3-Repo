using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offseter : Interactable
{
    [Header("Offseter Details")]
    [SerializeField] float disToInfluence;
    [SerializeField] float radius;
    [SerializeField] LayerMask layer;

    [SerializeField] SpriteRenderer renderer;

    [Header("State Details")]
    [SerializeField] States currentState;

    // Colors should not be the hard values but something a 
    // little tinted 
    [SerializeField] Color red;
    [SerializeField] Color green;
    [SerializeField] Color yellow;
    [SerializeField] float delayOnNeighbor;

    private Player player;
    private bool isChanging;

    public bool IsCorrect { get { return !isChanging ? currentState == States.green : false; } }

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        isChanging = false;

        SetColor(currentState);
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
        Collider2D col = Physics2D.OverlapCircle(this.transform.position + dir * disToInfluence, radius, layer);

        if(col != null)
        {
            // Something to detect 
            Offseter offseter = col.GetComponent<Offseter>();
            
            if(offseter != null)
            {
                // Make sure it has the component 
                //offseter.Interact();
                offseter.ChangeStateOnDelay(delayOnNeighbor);
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
            currentState = States.red;
            
        }
        else
        {
            currentState = currentState + 1;
        }

        // Updates the visual 
        SetColor(currentState);
    }

    private void SetColor(States state)
    {
        Color color = Color.white;

        switch (state)
        {
            case States.red:
                color = red;
                break;
            case States.green:
                color = green;
                break;
            case States.yellow:
                color = yellow;
                break;
        }

        renderer.color = color;
    }

    public void ChangeStateOnDelay(float delay)
    {
        StartCoroutine(ChangeStateOnDelayCo(delay));
    }

    private IEnumerator ChangeStateOnDelayCo(float delay)
    {
        isChanging = true;
        yield return new WaitForSeconds(delay);
        TryInteractWithNeighbor();
        ChangeState();

        isChanging = false;
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
