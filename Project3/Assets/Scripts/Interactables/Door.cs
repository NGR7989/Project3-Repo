using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : TextPopup
{
    [Header("Door")]
    [SerializeField] Color unlockedColor;
    [SerializeField] Color lockedColor;
    [SerializeField] SpriteRenderer renderer;

    [Header("Audio")]
    [SerializeField] AudioClip lockedClip;
    [SerializeField] AudioClip unlockedClip;


    private LevelLoader levelLoader;
    private SoundManager soundManager;

    [SerializeField] bool unlocked = false;

    private void Awake()
    {
        levelLoader = GameObject.FindObjectOfType<LevelLoader>();
        soundManager = GameObject.FindObjectOfType<SoundManager>();
    }

    public override void Interact()
    {
        if(unlocked)
        {
            // Load next room 
            levelLoader.LoadNextLevel();
        }
        else
        {
            // Runs popup text 
            base.Interact();
            soundManager.TryPlaySound("door", lockedClip, 1, 1);
        }
    }

    /// <summary>
    /// Call this function to unlock the door 
    /// </summary>
    public void Unlock()
    {
        unlocked = true;
        soundManager.TryPlaySound("door", unlockedClip, 1, 1);

        // Play the audio to unlock
    }

    // Update is called once per frame
    void Update()
    {
        if(unlocked)
        {
            renderer.color = unlockedColor; 
        }
        else
        {
            renderer.color = lockedColor;
        }    
    }
}
