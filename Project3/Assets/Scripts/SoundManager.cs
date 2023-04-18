using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] GameObject soundRef;
    [SerializeField] int soundListMaxSize;

    private Dictionary<string, LinkedList<GameObject>> sounds;

    private void Start()
    {
        sounds = new Dictionary<string, LinkedList<GameObject>>();
    }

    /// <summary>
    /// Attempt to add a sound source to the game. If the 
    /// key is already in use the previous sound will be
    /// overriden 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="audio"></param>
    public void TryPlaySound(string key, AudioClip audio, float pitchMin, float pitchMax)
    {
        if (sounds.ContainsKey(key))
        {
            if(sounds[key] != null)
            {
                // Sound must be replace 
                if(sounds[key].Count >= soundListMaxSize)
                {
                    Destroy(sounds[key].First.Value);
                    sounds[key].RemoveFirst();
                }

                CreateSource(key, Random.Range(pitchMin, pitchMax), audio);
            }
            else
            {
                // Sound can simply be added 
                CreateSource(key, Random.Range(pitchMin, pitchMax), audio);
            }
        }
        else
        {
            // Sound can be added to dictionary 
            sounds[key] = new LinkedList<GameObject>();
            CreateSource(key, Random.Range(pitchMin, pitchMax), audio);
        }
    }

    private void CreateSource(string key, float pitch, AudioClip audio)
    {
        sounds[key].AddLast(Instantiate(soundRef));

        AudioSource audioSource = sounds[key].Last.Value.GetComponent<AudioSource>();
        audioSource.clip = audio;
        audioSource.pitch = pitch;
        audioSource.Play();
    }
}
