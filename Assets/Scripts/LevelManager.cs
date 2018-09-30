/*
 * This class handles level management. This includes things like,
 * - starting levels
 * - stopping levels
 * - keeping track of the current level and current level data
 */

using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
    // Collection of all Levels
    public LevelCollection Levels;

    private IEnumerator currentLevelCoroutine;
    private int currentLevel = 1;

    public void Start() {
        PlayLevel();
    }

    // Play the current level.
    private void PlayLevel() {
        string filename = "LS";
        if (currentLevel < 100) {
            filename += "0";
            if (currentLevel < 10) {
                filename += "0";
            }
        }
        filename += currentLevel;
        
        // TODO handle file not found error 

        currentLevelCoroutine = Levels.LoadLevelFromFile(filename);
        Debug.Log(currentLevelCoroutine);
        Levels.StartCoroutine(currentLevelCoroutine);
    }

    // Stop the current level. 
    private void StopLevel() {
        Levels.StopCoroutine(currentLevelCoroutine);
    }

    // Level completed. Stop the current level, and start the next one.
    public void AdvanceLevel() {
        StopLevel();
        currentLevel += 1;
        StartCoroutine(PauseBetweenLevels());
        PlayLevel();
    }

    private IEnumerator PauseBetweenLevels() {
        yield return new WaitForSeconds(1.0f);
    }

}
