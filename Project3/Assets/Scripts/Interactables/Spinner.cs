using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : Interactable
{
    enum Direction {
        north = 0,
        south = 180,
        east = -90,
        west = 90
    }

    [Header("Spinner Info")]
    [SerializeField] Direction correctDirection;
    [SerializeField] SpriteRenderer spriteRndr;

    [Header("Audio")]
    [SerializeField] AudioClip turnSpinner;

    Direction direction = Direction.north;
    public bool active = true;

    private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        // Make object match starting state
        UpdateVisual();

        soundManager = GameObject.FindObjectOfType<SoundManager>();
    }

    public override void Interact() 
    {
        print("Spinning " + transform.name);
        Spin();

        if (active)
        {
            //Spin();
            //soundManager.TryPlaySound("lever", turnSpinner, 0.9f, 1.1f);
        }
    }

    public bool IsCorrect()
    {
        return direction == correctDirection;
    }

    private void UpdateVisual() {
        transform.eulerAngles += Vector3.forward * (float)direction;
    }

    private void Spin()
    {
        switch (direction)
        {
            case Direction.north:
                direction = Direction.east;
                break;
            case Direction.south:
                direction = Direction.west;
                break;
            case Direction.east:
                direction = Direction.south;
                break;
            case Direction.west:
                direction = Direction.north;
                break;
        }

        UpdateVisual();
    }
}
