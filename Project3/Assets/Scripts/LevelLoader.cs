using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] List<GameObject> levels;
    [SerializeField] GameObject player;
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

        // Deactivate the current level
        levels[currentLvl].SetActive(false);

        // Activate the specified level
        currentLvl = lvlNum;
        levels[currentLvl].SetActive(true);

        // Reset player position
        player.transform.position.Set(0, 0, 0);
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

        // Deactiveate the current level
        levels[currentLvl].SetActive(false);

        // Activate the next level
        currentLvl++;
        levels[currentLvl].SetActive(true);

        // Reset player position
        player.transform.position.Set(0, 0, 0);
    }
}
