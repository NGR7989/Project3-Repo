using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] List<GameObject> levels;
    [SerializeField] GameObject player;
    [SerializeField] FilterImage filterImage;
    [SerializeField] float staticTime;
    int currentLvl;

    /// <summary>
    /// Setup all the levels and activate the first one
    /// </summary>
    void Start()
    {
        // Make sure all levels are inactive to start
        foreach (GameObject level in levels)
        {
            level.SetActive(false);
        }

        // Activate the first level
        currentLvl = 0;
        levels[currentLvl].SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            LoadLevel(currentLvl);
        }
    }

    /// <summary>
    /// Loads a specified level
    /// </summary>
    /// <param name="lvlNum">level to load (0 indexed)</param>
    public void LoadLevel(int lvlNum)
    {
        // Check for valid level number
        if (levels.Count <= lvlNum || lvlNum < 0)
        {
            // Print error message and return
            print("LoadLevel Error: lvlNum " + lvlNum + " is outside the index of levels[]");
            return;
        }
        // Check for duplicate load
        else if (currentLvl == lvlNum)
        {
            // Print error message and return
            print("LoadLevel Error: lvlNum " + lvlNum + " is already loaded");
            return;
        }

        StartCoroutine(LoadAfterPause(lvlNum));
        //LoadLevelUnsafe(currentLvl);
    }

    /// <summary>
    /// Loads the next level in the levels list
    /// </summary>
    public void LoadNextLevel()
    {
        // Check if there is a next level to load
        if (levels.Count <= currentLvl + 1)
        {
            // Print error message and return
            print("LoadNextLevel Error: level " + (currentLvl + 1) + " is outside the index of levels[]");
            return;
        }

        StartCoroutine(LoadAfterPause(currentLvl + 1));
        //LoadLevelUnsafe(currentLvl + 1);
    }

    private IEnumerator LoadAfterPause(int levelToLoad)
    {
        // Make static appear 
        filterImage.TryAppear();

        // Loop until rendered 
        while (true)
        {
            if(!filterImage.Active)
            {
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(staticTime);
        LoadLevelUnsafe(levelToLoad);
        filterImage.TryDisappear();

        /*// Loop until gone 
        while (true)
        {
            if (!filterImage.Active)
            {
                break;
            }
            yield return null;
        }*/
    }

    private void LoadLevelUnsafe(int levelToLoad)
    {
        // Deactiveate the current level
        levels[currentLvl].SetActive(false);

        // Activate the next level
        levels[levelToLoad].SetActive(true);
        currentLvl = levelToLoad;

        // Reset player position
        player.transform.position = new Vector3(0, 0, 0);
        print("Moved Player to " + player.transform.position);
    }
}
