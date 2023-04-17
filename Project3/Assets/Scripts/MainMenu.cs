using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool loading = false;

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void PlayGame(float time)
    {
        if(loading)
        {
            return;
        }
        loading = true;
        StartCoroutine(PlayAfterTime(time));
    }

    private IEnumerator PlayAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        print("Quitting Game");
        Application.Quit();
    }
}
