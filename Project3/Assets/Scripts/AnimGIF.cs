using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimGIF : MonoBehaviour
{

    [SerializeField] private Sprite[] frames;
    [SerializeField] private float fps = 10.0f;
    private Image imageRenderer;

    //private Material mat;

    void Start()
    {
        imageRenderer = GetComponent<Image>();
    }

    void Update()
    {
        int index = (int)(Time.time * fps);
        index = index % frames.Length;
        //mat.mainTexture = frames[index].texture; // usar en planeObjects
        imageRenderer.sprite = frames[index];
    }
}