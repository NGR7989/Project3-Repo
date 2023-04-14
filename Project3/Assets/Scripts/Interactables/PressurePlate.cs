using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : TextPopup
{
    [Header("Rendering")]
    [SerializeField] Color inActiveColor;
    [SerializeField] Color activeColor;

    [SerializeField] SpriteRenderer renderer;

    [Header("Checking")]
    [SerializeField] float radiusCheck;
    [SerializeField] LayerMask layer;

    private bool active;

    public bool Active { get { return active; } }

    // Update is called once per frame
    void Update()
    {
        active = (Physics2D.OverlapCircle((Vector2)this.transform.position, radiusCheck) != null);

        // Update the color 
        if (active)
        {
            renderer.color = activeColor;
        }
        else
        {
            renderer.color = inActiveColor;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, radiusCheck);
    }
}
