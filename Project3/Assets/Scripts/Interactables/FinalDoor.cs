using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDoor : Door
{
    [Header("Level Loading")]
    [SerializeField] FilterImage filter;
    [SerializeField] float waitTime;

    bool starting = false;


    public override void Interact()
    {
        if (!starting)
        {
            filter.TryAppear();
            StartCoroutine(Starting());

            starting = true;
        }
    }

    private IEnumerator Starting()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
