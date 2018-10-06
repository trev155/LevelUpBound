/*
 * This class handles level management. This includes things like,
 * - starting levels
 * - stopping levels
 * - keeping track of the current level and current level data
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LevelManager : MonoBehaviour {
    // Constants
    private const string LEVEL_CLASSIC_PREFIX = "./Assets/Data/Levels/Classic";
    private const string LEVEL_CUSTOM_PREFIX = "./Assets/Data/Levels/Custom";
    private const string EXTERNALS_CLASSIC_PREFIX = "./Assets/Data/ExternalObjects/Classic";
    private const string EXTERNALS_CUSTOM_PREFIX = "./Assets/Data/ExternalObjects/Custom";
    private const string LEVELS_WITH_EXTERNALS_CLASSIC = "./Assets/Data/ExternalObjects/classicExternalsList.txt";
    private const string LEVELS_WITH_EXTERNALS_CUSTOM = "./Assets/Data/ExternalObjects/customExternalsList.txt";
    private const string LEVEL_OBJECT_TYPE = "level";
    private const string EXTERNAL_OBJECT_TYPE = "external";

    // Collection of all Levels
    public LevelConstructor levelConstructor;

    // Keep track of the current level
    private IEnumerator currentLevelCoroutine;
    private int currentLevel = 46;

    // Keep track of the game mode selected
    private string gameMode = GameContext.GameMode;

    // Utility functions
    Util util = new Util();

    // Levels that require external objects
    HashSet<int> levelsWithExternals = new HashSet<int>();

    /*
     * Initialization.
     */
    private void Awake() {
        // default game mode
        if (gameMode == null) {
            gameMode = "custom";
        }

        // load data regarding which levels have external objects
        string datafile = "";
        if (gameMode == "classic") {
            datafile = LEVELS_WITH_EXTERNALS_CLASSIC;
        } else if (gameMode == "custom") {
            datafile = LEVELS_WITH_EXTERNALS_CUSTOM;
        }
        StreamReader inputStream = new StreamReader(datafile);
        while (!inputStream.EndOfStream) {
            string line = inputStream.ReadLine();
            levelsWithExternals.Add(int.Parse(line));
        }
    }


    /*
     * Startup.
     */
    void Start() {
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
        currentLevelCoroutine = levelConstructor.LoadLevelFromFilepath(levelFilepath);
        if (currentLevelCoroutine == null) {
            return;
        }

        // Check if there are any other things we need to initialize for this level
        string externalsFilepath = util.GetFilepathString(currentLevel, gameMode, externalsPrefix, EXTERNAL_OBJECT_TYPE);
        if (levelsWithExternals.Contains(currentLevel)) {
            levelConstructor.LoadExternalObjects(externalsFilepath);
        }

        // TODO - handle filenotfound exceptions

        // Start the current level
        levelConstructor.StartCoroutine(currentLevelCoroutine);

        Debug.Log("Current Level: " + currentLevel);
    }

    /*
     * Stop the current level. 
     */
    private void StopLevel() {
        levelConstructor.StopCoroutine(currentLevelCoroutine);
    }

    /*
     * Level completed. Stop the current level, and start the next one.
     */
    public void AdvanceLevel() {
        StopLevel();
        levelConstructor.RemoveExternalObjects();
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
