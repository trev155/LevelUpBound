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
    private const string LEVEL_CLASSIC_PREFIX = "Data/Levels/Classic";
    private const string LEVEL_CUSTOM_PREFIX = "Data/Levels/Custom";
    private const string EXTERNALS_CLASSIC_PREFIX = "Data/ExternalObjects/Classic";
    private const string EXTERNALS_CUSTOM_PREFIX = "Data/ExternalObjects/Custom";
    private const string LEVELS_WITH_EXTERNALS_CLASSIC = "Data/ExternalObjects/classicExternalsList";
    private const string LEVELS_WITH_EXTERNALS_CUSTOM = "Data/ExternalObjects/customExternalsList";
    private const string LEVEL_OBJECT_TYPE = "level";
    private const string EXTERNAL_OBJECT_TYPE = "external";

    // Level construction manager object
    public LevelConstructor levelConstructor;
    // UI Management
    public UserInterfaceManager UImanager;

    // Keep track of the current level's coroutine so that we can stop it when we need to
    private IEnumerator currentLevelCoroutine;
    // Utility functions
    Util util = new Util();
    // Levels that require external objects
    HashSet<int> levelsWithExternals = new HashSet<int>();

    /*
     * Initialization.
     */
    private void Awake() {
        // default game mode
        if (GameContext.GameMode == null) {
            GameContext.GameMode = "classic";
        }

        // default level
        if (GameContext.CurrentLevel < 1) {
            GameContext.CurrentLevel = 1;
        }
        

        // load data regarding which levels have external objects
        string datafile = "";
        if (GameContext.GameMode == "classic") {
            datafile = LEVELS_WITH_EXTERNALS_CLASSIC;
        } else if (GameContext.GameMode == "custom") {
            datafile = LEVELS_WITH_EXTERNALS_CUSTOM;
        }

        TextAsset externalLevelsTextAsset = Resources.Load<TextAsset>(datafile);
        string[] externalLevelsLines = externalLevelsTextAsset.text.Split('\n');
        foreach (string line in externalLevelsLines) {
            levelsWithExternals.Add(int.Parse(line.Trim()));
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
        if (GameContext.GameMode.Equals("classic")) {
            levelPrefix = LEVEL_CLASSIC_PREFIX;
            externalsPrefix = EXTERNALS_CLASSIC_PREFIX;
        } else if (GameContext.GameMode.Equals("custom")) {
            levelPrefix = LEVEL_CUSTOM_PREFIX;
            externalsPrefix = EXTERNALS_CUSTOM_PREFIX;
        }

        // Load the current level data 
        string levelFilepath = util.GetFilepathString(GameContext.CurrentLevel, GameContext.GameMode, levelPrefix, LEVEL_OBJECT_TYPE);
        currentLevelCoroutine = levelConstructor.LoadLevelFromFilepath(levelFilepath);
        if (currentLevelCoroutine == null) {
            return;
        }

        // Check if there are any other things we need to initialize for this level
        string externalsFilepath = util.GetFilepathString(GameContext.CurrentLevel, GameContext.GameMode, externalsPrefix, EXTERNAL_OBJECT_TYPE);
        if (levelsWithExternals.Contains(GameContext.CurrentLevel)) {
            levelConstructor.LoadExternalObjects(externalsFilepath);
        }
        
        // Start the current level
        levelConstructor.StartCoroutine(currentLevelCoroutine);

        // Update the UI
        UImanager.UpdateLevelText();
        UImanager.UpdateGameModeText();
        Debug.Log("Current Level: " + GameContext.CurrentLevel);
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
        GameContext.CurrentLevel += 1;
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
