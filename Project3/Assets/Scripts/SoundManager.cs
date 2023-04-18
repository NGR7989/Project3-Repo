using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] GameObject soundRef;

    private Dictionary<string, GameObject> sounds;

    private void Start()
    {
        sounds = new Dictionary<string, GameObject>();
    }

    /// <summary>
    /// Attempt to add a sound source to the game. If the 
    /// key is already in use the previous sound will be
    /// overriden 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="audio"></param>
    public void TryPlaySound(string key, AudioClip audio)
    {
        if (sounds.ContainsKey(key))
        {
            if(sounds[key] != null)
            {
                // Sound must be replace 
                Destroy(sounds[key]);
                sounds[key] = Instantiate(soundRef);
                sounds[key].GetComponent<AudioSource>().clip = audio;
            }
            else
            {
                // Sound can simply be added 
                sounds[key] = Instantiate(soundRef);
                sounds[key].GetComponent<AudioSource>().clip = audio;
            }
        }
        else
        {
            // Sound can be added to dictionary 
            sounds.Add(key, Instantiate(soundRef));
            sounds[key].GetComponent<AudioSource>().clip = audio;
        }
    }
}
