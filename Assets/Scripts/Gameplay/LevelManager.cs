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
    public MainGame mainGame;

    // Keep track of the current level's coroutine so that we can stop it when we need to
    private IEnumerator currentLevelCoroutine;
    // Levels that require external objects
    HashSet<int> levelsWithExternals = new HashSet<int>();

    // Tutorial mode requires extra support
    public ModalPrelevelInfo modalPrelevelInfo;
    public Transform modalContainer;

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
        if (GameContext.GameMode == Mode.TUTORIAL) {
            PlayTutorialLevel();
            return;
        }

        PlayLevel();
    }

    /*
     * Play the current level.
     */
    public void PlayLevel() {
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
        mainGame.UpdateLevelText();
        mainGame.UpdateGameModeText();

        Debug.Log("Current Level: " + GameContext.CurrentLevel);
    }

    /*
     * Wrapper for playing a tutorial level.
     */
    public void PlayTutorialLevel() {
        TutorialModeHandler();
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

        if (GameContext.GameMode == Mode.TUTORIAL) {
            PlayTutorialLevel();
            return;
        }
        PlayLevel();
    }

    /*
     * Wait for some time.
     */
    private IEnumerator PauseBetweenLevels() {
        yield return new WaitForSeconds(2.0f);
    }

    /*
     * Tutorial Mode handler
     */
    private void TutorialModeHandler() {
        ModalPrelevelInfo modal = Instantiate(modalPrelevelInfo, modalContainer);
        List<string> messages = new List<string>();
        Dictionary<int, string> imageLevelToPaths = new Dictionary<int, string>();
        if (GameContext.CurrentLevel == 1) {
            messages.Add("Welcome to Level Up Bound! This game is a top-down arcade style game where you dodge obstacles.");
            messages.Add("Your objective is to reach the goal circle at the top of the screen.");
            messages.Add("Make your way to the top by tapping on the control arrows, and don't get caught in the explosions!");
            imageLevelToPaths.Add(2, "TutorialImages/tut1-2");
            imageLevelToPaths.Add(3, "TutorialImages/tut1-3");
        }
        if (GameContext.CurrentLevel == 2) {
            messages.Add("Great work! Different levels have different explosion patterns. Try to analyze what the pattern is before you start moving!");
        }
        if (GameContext.CurrentLevel == 3) {
            messages.Add("Some levels will not only have explosions, but other elements as well.");
            messages.Add("For example, the playing field may contain walls.");
            imageLevelToPaths.Add(2, "TutorialImages/tut3-2");
        }
        if (GameContext.CurrentLevel == 4) {
            messages.Add("Key levels are also quite common. In these levels, you have to fetch the key to unlock the sealed wall.");
            imageLevelToPaths.Add(1,"TutorialImages/tut4-1");
        }
        if (GameContext.CurrentLevel == 5) {
            messages.Add("Nice! Now use your skills to complete the final level of the Tutorial.");
        }
        modal.Initialize(messages, imageLevelToPaths);
        return;
    }
}
