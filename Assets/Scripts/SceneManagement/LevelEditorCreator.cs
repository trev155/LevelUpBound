/*
 * Handles the Level editor creation page.
 */
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;


public class LevelEditorCreator : MonoBehaviour {
    public Text levelExplosionsText;
    public Text levelExternalsText;
    public Text outputText;
    public Text delayText;
    public ModalPages modalPages;
    public Transform modalContainer;

    private ModalPages levelEditorInstructionsModal;

    private const string SAVE_FAILED_REQUIRED_ELEMENTS_TEXT = "Save failed!\nYour level must have at least one explosion and one delay element!";
    private const string SAVE_SUCCESS_TEXT = "Save Successful!";

    private const int GRID_DIMENSION = 5;
    private List<List<bool>> tileActivationStates;
    private Color32 activatedTileColor = new Color32(140, 130, 201, 255);
    private Color32 deactivatedTileColor = new Color32(172, 209, 255, 255);
    private int currentDelayTimeSetting = 500;

    private void Awake() {
        AspectRatioManager.AdjustScreenElements();
        ThemeManager.SetTheme();
    }

    private void Start() {
        InitializeTileActivationStates();
        levelExplosionsText.text = "";
        levelExternalsText.text = "";
        outputText.text = "";

        if (PersistentStorage.IsLevelSlotOccupied(GameContext.LevelEditorSlotSelection)) {
            levelExplosionsText.text = PersistentStorage.LoadCustomLevelExplosions(GameContext.LevelEditorSlotSelection);
            levelExternalsText.text = PersistentStorage.LoadCustomLevelExternals(GameContext.LevelEditorSlotSelection);
        }
    }

    private void InitializeTileActivationStates() {
        tileActivationStates = new List<List<bool>> {
            new List<bool>(),
            new List<bool>(),
            new List<bool>(),
            new List<bool>(),
            new List<bool>()
        };
        foreach (List<bool> row in tileActivationStates) {
            for (int col = 0; col < GRID_DIMENSION; col++) {
                row.Add(false);
            }
        }
    }

    //--------------------
    // Level Construction
    //--------------------
    public void GameTilePressed() {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        GameObject tilePressed = EventSystem.current.currentSelectedGameObject;
        int row = int.Parse(tilePressed.name.Substring(0, 1));
        int col = int.Parse(tilePressed.name.Substring(2, 1));

        ToggleGameTile(row, col);
        ToggleTileColour(tilePressed, row, col);
    }

    private void ToggleGameTile(int row, int col) {
        tileActivationStates[row][col] = !tileActivationStates[row][col];
    }

    private void ToggleTileColour(GameObject tile, int row, int col) {
        Color32 newColor;
        if (tileActivationStates[row][col]) {
            newColor = activatedTileColor;
        } else {
            newColor = deactivatedTileColor;
        }
        Image tileImage = tile.GetComponent<Image>();
        tileImage.color = newColor;
    }

    public void AddExplosionSelection() {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        string levelTextToAdd = "";
        for (int row = 0; row < GRID_DIMENSION; row++) {
            for (int col = 0; col < GRID_DIMENSION; col++) {
                if (tileActivationStates[row][col]) {
                    GameObject tileToAdd = GameObject.Find(row.ToString() + "," + col.ToString());
                    tileActivationStates[row][col] = false;
                    ToggleTileColour(tileToAdd, row, col);
                    levelTextToAdd += tileToAdd.name + "\n";
                }
            }
        }
        levelExplosionsText.text += levelTextToAdd;
    }

    public void AddWallsSelection() {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        string externalsTextToAdd = "";
        for (int row = 0; row < GRID_DIMENSION; row++) {
            for (int col = 0; col < GRID_DIMENSION; col++) {
                if (tileActivationStates[row][col]) {
                    GameObject tileToAdd = GameObject.Find(row.ToString() + "," + col.ToString());
                    tileActivationStates[row][col] = false;
                    ToggleTileColour(tileToAdd, row, col);
                    externalsTextToAdd += "W" + tileToAdd.name + "\n";
                }
            }
        }
        levelExternalsText.text += externalsTextToAdd;
    }

    public void AddDelay() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);        
        levelExplosionsText.text += "W" + currentDelayTimeSetting + "\n";
    }

    public void RemoveLastLineExplosions() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        string newLevelText = "";
        string[] levelLines = levelExplosionsText.text.Split('\n');
        for (int levelLineIndex = 0; levelLineIndex < levelLines.Length - 2; levelLineIndex++) {
            string line = levelLines[levelLineIndex];
            newLevelText += line + "\n";
        }

        levelExplosionsText.text = newLevelText;
    }

    public void RemoveLastLineExternals() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        string newLevelText = "";
        string[] levelLines = levelExternalsText.text.Split('\n');
        for (int levelLineIndex = 0; levelLineIndex < levelLines.Length - 2; levelLineIndex++) {
            string line = levelLines[levelLineIndex];
            newLevelText += line + "\n";
        }

        levelExternalsText.text = newLevelText;
    }

    public void DecreaseCurrentDelayValue() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        if (currentDelayTimeSetting > 50) {
            currentDelayTimeSetting -= 50;
        }

        delayText.text = "Add Delay\n(" + currentDelayTimeSetting + " ms)";
    }

    public void IncreaseCurrentDelayValue() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        if (currentDelayTimeSetting < 10000) {
            currentDelayTimeSetting += 50;
        }

        delayText.text = "Add Delay\n(" + currentDelayTimeSetting + " ms)";
    }

    public void SaveLevel() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        List<string> levelLinesToSave = GetFormattedLevelTextList(levelExplosionsText);
        if (levelLinesToSave == null) {
            outputText.text = SAVE_FAILED_REQUIRED_ELEMENTS_TEXT;
            return;
        }
        string levelStringToSave = GenerateLevelExplosionStringToSave(levelLinesToSave);

        PersistentStorage.SaveCustomLevel(GameContext.LevelEditorSlotSelection, levelStringToSave, levelExternalsText.text);

        outputText.text = SAVE_SUCCESS_TEXT;
    }

    private List<string> GetFormattedLevelTextList(Text text) {
        List<string> levelLinesToSave = new List<string>();
        string[] levelTextLines = levelExplosionsText.text.TrimEnd().Split('\n');
        bool hasDelay = false;
        bool hasExplosion = false;
        foreach (string levelTextLine in levelTextLines) {
            if (!levelTextLine.Contains("W")) {
                levelLinesToSave.Add("T" + levelTextLine + " AR");
                hasExplosion = true;
            } else {
                levelLinesToSave.Add(levelTextLine);
                hasDelay = true;
            }
        }
        
        if (!hasDelay || !hasExplosion) {
            Debug.Log("Cannot save - level does not have at least one explosion and one delay.");
            return null;
        }

        return levelLinesToSave;
    }

    private string GenerateLevelExplosionStringToSave(List<string> levelLinesToSave) {
        string levelStringToSave = "";
        foreach (string line in levelLinesToSave) {
            levelStringToSave += line + "\n";
        }
        levelStringToSave = levelStringToSave.TrimEnd();
        return levelStringToSave;
    }

    //-------
    // Other
    //-------
    public void BackButton() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        SceneManager.LoadPreviousContextPage();
        GameContext.PreviousPageContext = SceneName.MAIN_MENU;
    }

    public void InstructionsButton() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
       
        levelEditorInstructionsModal = Instantiate(modalPages, modalContainer);

        List<string> messages = new List<string>();
        Dictionary<int, string> imageLevelToPaths = new Dictionary<int, string>();
        messages.Add("Page One");
        messages.Add("Page Two");
        messages.Add("Page Three");
        imageLevelToPaths.Add(2, "TutorialImages/tut1-2");
        imageLevelToPaths.Add(3, "TutorialImages/tut1-3");
       
        levelEditorInstructionsModal.Initialize(messages, imageLevelToPaths);
        return;
    }
}