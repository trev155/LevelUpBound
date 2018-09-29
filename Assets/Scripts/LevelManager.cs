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
    private int currentLevel = 12;

    public void Start() {
        PlayLevel();
    }

    // Play the current level.
    private void PlayLevel() {
        switch (currentLevel) {
            case 1:
                currentLevelCoroutine = Levels.Level1();
                break;
            case 2:
                currentLevelCoroutine = Levels.Level2();
                break;
            case 3:
                currentLevelCoroutine = Levels.Level3();
                break;
            case 4:
                currentLevelCoroutine = Levels.Level4();
                break;
            case 5:
                currentLevelCoroutine = Levels.Level5();
                break;
            case 6:
                currentLevelCoroutine = Levels.Level6();
                break;
            case 7:
                currentLevelCoroutine = Levels.Level7();
                break;
            case 8:
                currentLevelCoroutine = Levels.Level8();
                break;
            case 9:
                currentLevelCoroutine = Levels.Level9();
                break;
            case 10:
                currentLevelCoroutine = Levels.Level10();
                break;
            case 11:
                currentLevelCoroutine = Levels.Level11();
                break;
            case 12:
                currentLevelCoroutine = Levels.Level12();
                break;
            case 13:
                currentLevelCoroutine = Levels.Level13();
                break;
            case 14:
                currentLevelCoroutine = Levels.Level14();
                break;
            case 15:
                currentLevelCoroutine = Levels.Level15();
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
