using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTile : MonoBehaviour
{
    [SerializeField] float slideDis;
    [SerializeField] float slideSpeed;
    [SerializeField] float radiusCheck = 0.1f;

    private Player player;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.Equals(this.transform.position))
        {
            Vector3 checkPos = player.transform.position + player.InteractionDir * slideDis;
            if(Physics2D.OverlapCircle(checkPos, radiusCheck) == null)
            {
                // Check if spot is not occupied 

                if (player.CanMove)
                {
                    // Slide player in opposite direction 
                    player.CanMove = false;
                    StartCoroutine(SlidePlayer());
                }
            }
        }
    }

    private IEnumerator SlidePlayer()
    {
        float lerp = 0;
        Vector3 start = player.transform.position;
        Vector3 target = player.transform.position + player.InteractionDir * slideDis;

        while(lerp <= 1)
        {
            player.transform.position = Vector3.Lerp(start, target, lerp);

            lerp += Time.deltaTime * slideSpeed;
            yield return null;
        }

        player.transform.position = target;
        player.CanMove = true;
    }

}
