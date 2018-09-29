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
        switch (currentLevel) {
            case 1:
                currentLevelCoroutine = Levels.LevelOne();
                break;
            case 2:
                currentLevelCoroutine = Levels.LevelTwo();
                break;
            case 3:
                currentLevelCoroutine = Levels.LevelThree();
                break;
            case 4:
                currentLevelCoroutine = Levels.LevelFour();
                break;
            default:
                return;
        }
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
