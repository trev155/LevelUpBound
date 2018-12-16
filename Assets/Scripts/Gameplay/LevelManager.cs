/*
 * Handling levels, such as starting a level, stopping a level, and more.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
    private const string LEVEL_OBJECT_TYPE = "level";
    private const string EXTERNAL_OBJECT_TYPE = "external";

    public LevelConstructor levelConstructor;
    public MainGame mainGame;

    private IEnumerator currentLevelCoroutine;
    HashSet<int> levelsWithExternals = new HashSet<int>();

    public ModalPrelevelInfo modalPrelevelInfo;
    public Transform modalContainer;


    private void Awake() {
        if (GameContext.GameMode != Mode.CUSTOM) {
            LoadLevelsWithExternals();
        }
    }

    void Start() {
        if (GameContext.GameMode == Mode.TUTORIAL) {
            PlayTutorialLevel();
            return;
        }
        PlayLevel();
    }

    public void PlayLevel() {
        if (GameContext.GameMode == Mode.CUSTOM) {
            string customLevelExplosions = PlayerPrefs.GetString(GameContext.LevelEditorSlotSelection + "_CUSTOM");
            string customLevelExternals = PlayerPrefs.GetString(GameContext.LevelEditorSlotSelection + "_EXTERNALS");
            List<string> customLevelExplosionsList = new List<string>();
            foreach (string line in customLevelExplosions.Split('\n')) {
                customLevelExplosionsList.Add(line);
            }
            currentLevelCoroutine = levelConstructor.LoadLevelFromList(customLevelExplosionsList);

            List<string> customLevelExternalsList = new List<string>();
            foreach (string line in customLevelExternals.Split('\n')) {
                customLevelExternalsList.Add(line);
            }
            levelConstructor.ConstructExternalObjects(customLevelExternalsList);
        } else {
            string levelPrefix = GameMode.GetLevelPrefix(GameContext.GameMode);
            string levelFilepath = LevelStringConstructor.GetFilepathString(GameContext.CurrentLevel, levelPrefix);
            currentLevelCoroutine = levelConstructor.LoadLevelFromFilepath(levelFilepath);
        }

        if (currentLevelCoroutine == null) {
            throw new UnityException("Current Level Coroutine was null, cannot play level");
        }

        levelConstructor.StartCoroutine(currentLevelCoroutine);
        UpdateUIElements();
    }

    private void LoadExternalObjects() {
        string externalPrefix = GameMode.GetExternalPrefix(GameContext.GameMode);
        string externalsFilepath = LevelStringConstructor.GetFilepathString(GameContext.CurrentLevel, externalPrefix);
        if (levelsWithExternals.Contains(GameContext.CurrentLevel)) {
            levelConstructor.LoadExternalObjects(externalsFilepath);
        }
    }

    private void UpdateUIElements() {
        mainGame.UpdateLevelText();
        mainGame.UpdateGameModeText();
    }

    public void StopLevel() {
        levelConstructor.StopCoroutine(currentLevelCoroutine);
    }

    public void LevelCompletedHandler() {
        StopLevel();
        levelConstructor.RemoveExternalObjectsFromScene();
        GameContext.CurrentLevel += 1;
        StartCoroutine(InitiateWaitBeforeAdvancingLevel());

        if (GameContext.GameMode == Mode.TUTORIAL) {
            PlayTutorialLevel();
            return;
        }
        PlayLevel();
    }

    private void LoadLevelsWithExternals() {
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

    private IEnumerator InitiateWaitBeforeAdvancingLevel() {
        yield return new WaitForSeconds(2.0f);
    }

    public void PlayTutorialLevel() {
        ModalPrelevelInfo tutorialModal = Instantiate(modalPrelevelInfo, modalContainer);
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
            imageLevelToPaths.Add(1, "TutorialImages/tut4-1");
        }
        if (GameContext.CurrentLevel == 5) {
            messages.Add("Nice! Now use your skills to complete the final level of the Tutorial.");
        }
        tutorialModal.Initialize(messages, imageLevelToPaths);
        return;
    }

    //----------------
    // Static Methods
    //----------------
    public static void RecordLevelCompleted(Mode mode, int level) {
        if (mode == Mode.TUTORIAL || mode == Mode.CUSTOM) {
            return;
        }

        if (GameContext.CompletedLevels[mode].Contains(level)) {
            return;
        } else {
            GameContext.CompletedLevels[mode].Add(level);
        }
    }

    public static bool CompletedAllLevelsInGameMode() {
        return IsLastLevel() && !CameFromLevelSelector();
    }

    public static bool IsLastLevel() {
        return GameContext.CurrentLevel == GameMode.GetNumberOfLevels(GameContext.GameMode);
    }

    public static bool CameFromLevelSelector() {
        return GameContext.PlayedFromLevelSelector;
    }

    public static bool CameFromCustomLevelEditor() {
        return GameContext.GameMode == Mode.CUSTOM;
    }
}
