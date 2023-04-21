using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : Interactable
{
    [Header("Spinner Info")]
    [SerializeField] Direction correctDirection;
    [SerializeField] public SpriteRenderer spriteRndr;

    [Header("Audio")]
    [SerializeField] AudioClip turnSpinner;

    Direction direction = Direction.North;
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
        if (active)
        {
            Spin();
            soundManager.TryPlaySound("lever", turnSpinner, 0.9f, 1.1f);
        }
    }

    public bool IsCorrect()
    {
        return direction == correctDirection;
    }

    public void RandomizeDirection()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                direction = Direction.North; 
                break;
            case 1:
                direction = Direction.East;
                break;
            case 2:
                direction = Direction.South;
                break;
            default:
                direction = Direction.West;
                break;
        }
    }

    private void UpdateVisual() {
        transform.eulerAngles = Vector3.forward * (float)direction;
    }

    private void Spin()
    {
        switch (direction)
        {
            case Direction.North:
                direction = Direction.East;
                print("Pointing Right: " + (float)Direction.East);
                break;
            case Direction.South:
                direction = Direction.West;
                print("Pointing Left: " + (float)Direction.West);
                break;
            case Direction.East:
                direction = Direction.South;
                print("Pointing Down: " + (float)Direction.South);
                break;
            case Direction.West:
                direction = Direction.North;
                print("Pointing Up: " + (float)Direction.North);
                break;
        }

        UpdateVisual();
    }
}
