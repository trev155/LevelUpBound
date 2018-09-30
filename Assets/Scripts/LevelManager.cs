/*
 * This class handles level management. This includes things like,
 * - starting levels
 * - stopping levels
 * - keeping track of the current level and current level data
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
    // Constants
    private const string LEVEL_CLASSIC_PREFIX = "./Assets/Scripts/Levels/Classic";
    private const string LEVEL_CUSTOM_PREFIX = "./Assets/Scripts/Levels/Custom";
    private const string EXTERNALS_CLASSIC_PREFIX = "./Assets/Scripts/ExternalObjects/Classic";
    private const string EXTERNALS_CUSTOM_PREFIX = "./Assets/Scripts/ExternalObjects/Custom";
    private const string LEVEL_OBJECT_TYPE = "level";
    private const string EXTERNAL_OBJECT_TYPE = "external";

    // Collection of all Levels
    public LevelCollection Levels;

    // Keep track of the current level
    private IEnumerator currentLevelCoroutine;
    private int currentLevel = 10;

    // Keep track of the game mode selected
    private string gameMode;

    // Utility functions
    Util util = new Util();

    // Levels that require external objects
    HashSet<int> levelsWithExternals = new HashSet<int>();


    /*
     * Initialization.
     */
    private void Awake() {
        // TODO probably should read these levels from file, and they will be different for the 2 game modes
        int[] levelsWithExternalsArr = { 7, 10, 11, 13 };
        foreach (int i in levelsWithExternalsArr) {
            levelsWithExternals.Add(i);
        }
    }

    /*
     * Startup.
     */
    void Start() {
        gameMode = "classic";
        // gameMode = "custom";

        PlayLevel();
    }

    /*
     * Play the current level.
     */
    private void PlayLevel() {
        // File locations to read from depends on the game mode
        string levelPrefix = "";
        string externalsPrefix = "";
        if (gameMode.Equals("classic")) {
            levelPrefix = LEVEL_CLASSIC_PREFIX;
            externalsPrefix = EXTERNALS_CLASSIC_PREFIX;
        } else if (gameMode.Equals("custom")) {
            levelPrefix = LEVEL_CUSTOM_PREFIX;
            externalsPrefix = EXTERNALS_CUSTOM_PREFIX;
        }

        // Load the current level data
        string levelFilepath = util.GetFilepathString(currentLevel, gameMode, levelPrefix, LEVEL_OBJECT_TYPE);
        currentLevelCoroutine = Levels.LoadLevelFromFilepath(levelFilepath);

        // Check if there are any other things we need to initialize for this level
        string externalsFilepath = util.GetFilepathString(currentLevel, gameMode, externalsPrefix, EXTERNAL_OBJECT_TYPE);
        if (levelsWithExternals.Contains(currentLevel)) {
            Levels.LoadExternalObjects(externalsFilepath);
        }

        // TODO - handle filenotfound exceptions

        // Start the current level
        Levels.StartCoroutine(currentLevelCoroutine);

        Debug.Log("Current Level: " + currentLevel);
    }

    /*
     * Stop the current level. 
     */
    private void StopLevel() {
        Levels.StopCoroutine(currentLevelCoroutine);
    }

    /*
     * Level completed. Stop the current level, and start the next one.
     */
    public void AdvanceLevel() {
        StopLevel();
        Levels.RemoveExternalObjects();
        currentLevel += 1;
        StartCoroutine(PauseBetweenLevels());
        PlayLevel();
    }

    /*
     * Wait for some time.
     */
    private IEnumerator PauseBetweenLevels() {
        yield return new WaitForSeconds(2.0f);
    }

}
