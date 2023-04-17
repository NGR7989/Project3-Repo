using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToStart : MonoBehaviour
{
    [SerializeField] FilterImage filter;
    [SerializeField] float timeTillLoadGame;

    bool starting = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !starting)
        {
            filter.TryAppear();
            StartCoroutine(Starting());

            starting = true;
        }    
    }

    private IEnumerator Starting()
    {
        yield return new WaitForSeconds(timeTillLoadGame);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
