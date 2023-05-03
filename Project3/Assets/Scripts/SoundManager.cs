using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] List<AudioClip> musicTracks;
    [SerializeField] float musicVolume;
    [SerializeField] float musicFadeOutTime;

    [Header("Sounds FX")]
    [SerializeField] GameObject soundRef;
    [SerializeField] int soundListMaxSize;

    private Dictionary<string, LinkedList<GameObject>> sounds;
    private AudioSource musicSource;
    private bool musicChanging = false;

    private void Start()
    {
        sounds = new Dictionary<string, LinkedList<GameObject>>();

        sounds["MUSIC"] = new LinkedList<GameObject>();
        musicSource = CreateSource("MUSIC", 1, musicVolume, musicTracks[0]);
        musicSource.loop = true;
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

    private AudioSource CreateSource(string key, float pitch, AudioClip audio)
    {
        // 0.5 is the default unity audiosource volume 
        return CreateSource(key, pitch, 0.5f, audio);
    }

    private AudioSource CreateSource(string key, float pitch, float volume, AudioClip audio)
    {
        sounds[key].AddLast(Instantiate(soundRef));

        AudioSource audioSource = sounds[key].Last.Value.GetComponent<AudioSource>();
        audioSource.clip = audio;
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.Play();

        return audioSource;
    }

    /// <summary>
    /// Changes the music track from one audio clip to the next 
    /// </summary>
    /// <param name="index"></param>
    public void ChangeMusicTrack(int index)
    {
        if(!musicChanging)
        {
            musicChanging = true;
            StartCoroutine(ChangeMusicTrackCo(index));
        }
    }

    private IEnumerator ChangeMusicTrackCo(int index)
    {
        yield return FadeSource(musicSource, musicFadeOutTime);
        musicSource.clip = musicTracks[index];
    }

    /// <summary>
    /// This coroutine fades out a source's volume to 0
    /// </summary>
    /// <param name="source"></param>
    /// <param name="fadeTime"></param>
    /// <returns></returns>
    private IEnumerator FadeSource(AudioSource source, float fadeTime)
    {
        float time = 0;

        float startVolume = source.volume;
        while(time <= fadeTime)
        {
            source.volume = Mathf.Lerp(startVolume, 0, time / fadeTime);

            time += Time.deltaTime;
            yield return null;
        }
    }
}