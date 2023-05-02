using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    private bool loading = false;

    public void MainMenu(float time)
    {
        if (loading)
        {
            return;
        }
        loading = true;
        StartCoroutine(PlayAfterTime(time));
    }

    public void QuitGame()
    {
        print("Quitting Game");
        Application.Quit();
    }

    private IEnumerator PlayAfterTime (float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("TestMenu");
    }
}
