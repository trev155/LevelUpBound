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
    private const string LEVEL_OBJECT_TYPE = "level";
    private const string EXTERNAL_OBJECT_TYPE = "external";

    // Level construction manager object
    public LevelConstructor levelConstructor;
    // UI Management
    public UserInterfaceManager UImanager;

    // Keep track of the current level's coroutine so that we can stop it when we need to
    private IEnumerator currentLevelCoroutine;
    // Levels that require external objects
    HashSet<int> levelsWithExternals = new HashSet<int>();

    /*
     * Initialization.
     */
    private void Awake() {
        // load data regarding which levels have external objects
        string datafile = GameMode.GetExternalListPath(GameContext.GameMode);
        TextAsset externalLevelsTextAsset = Resources.Load<TextAsset>(datafile);
        string externalLevelsText = externalLevelsTextAsset.text;
        string[] externalLevelsLines = externalLevelsText.Split('\n');
        foreach (string line in externalLevelsLines) {
            if (line.Trim() != "") {
                levelsWithExternals.Add(int.Parse(line.Trim()));
            }
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
        string levelPrefix = GameMode.GetLevelPrefix(GameContext.GameMode);
        string externalPrefix = GameMode.GetExternalPrefix(GameContext.GameMode);

        // Load the current level data 
        string levelFilepath = LevelString.GetFilepathString(GameContext.CurrentLevel, levelPrefix);
        currentLevelCoroutine = levelConstructor.LoadLevelFromFilepath(levelFilepath);
        if (currentLevelCoroutine == null) {
            return;
        }

        // Check if there are any other things we need to initialize for this level
        string externalsFilepath = LevelString.GetFilepathString(GameContext.CurrentLevel, externalPrefix);
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
    public void StopLevel() {
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
