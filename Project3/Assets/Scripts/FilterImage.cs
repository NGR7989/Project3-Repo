using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FilterImage : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] float appearSpeed;
    [SerializeField] AnimationCurve appearCurve;
    [SerializeField] float disappearSpeed;
    [SerializeField] AnimationCurve disappearCurve;
    [Header("Audio")]
    [SerializeField] AudioSource source;
    [SerializeField] float volumeMax;
    [SerializeField] float volumeMin;
    private bool active;

    public bool Active { get { return active; } }

    private void Start()
    {
        TryDisappear();
    }

    /// <summary>
    /// Try to make image appear if not already animating 
    /// </summary>
    public void TryAppear()
    {
        if(active)
        {
            return;
        }

        StartCoroutine(Appear());
        active = true;
    }

    /// <summary>
    /// Try to make image disappear if not already animating 
    /// </summary>
    public void TryDisappear()
    {
        if (active)
        {
            return;
        }

        StartCoroutine(Disappear());
        active = true;
    }

    /// <summary>
    /// Makes the image visible 
    /// </summary>
    /// <returns></returns>
    private IEnumerator Appear()
    {
        float lerp = 0; 

        while(lerp <= 1)
        {
            image.color = Color.Lerp(Color.clear, Color.white, appearCurve.Evaluate(lerp));

            source.volume = Mathf.Lerp(volumeMin, volumeMax, lerp);

            lerp += Time.deltaTime * appearSpeed;
            yield return null;
        }

        active = false;
    }

    /// <summary>
    /// Makes the image turn clear 
    /// </summary>
    /// <returns></returns>
    private IEnumerator Disappear()
    {
        float lerp = 0;

        while (lerp <= 1)
        {
            image.color = Color.Lerp(Color.white, Color.clear, disappearCurve.Evaluate(lerp));

            source.volume = Mathf.Lerp(volumeMax, volumeMin, lerp);

            lerp += Time.deltaTime * disappearSpeed;
            yield return null;
        }

        active = false;
    }
}
