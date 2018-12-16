/*
 * Handles the Level editor creation page.
 */
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelEditorCreator : MonoBehaviour {
    public Text levelExplosionsText;
    public Text levelExternalsText;
    public Text outputText;
    public Text delayText;
    

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

    /*
     * Back button handler
     */
    public void BackButton() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        SceneManager.LoadPreviousContextPage();
        GameContext.PreviousPageContext = SceneName.MAIN_MENU;
    }

    //--------------------
    // Level Construction
    //--------------------
    public void GameTilePressed() {
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
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);        
        levelExplosionsText.text += "W" + currentDelayTimeSetting + "\n";
    }

    public void RemoveLastLineExplosions() {
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
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        if (currentDelayTimeSetting > 50) {
            currentDelayTimeSetting -= 50;
        }

        delayText.text = "Add Delay\n(" + currentDelayTimeSetting + " ms)";
    }

    public void IncreaseCurrentDelayValue() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        if (currentDelayTimeSetting < 10000) {
            currentDelayTimeSetting += 50;
        }

        delayText.text = "Add Delay\n(" + currentDelayTimeSetting + " ms)";
    }

    public void SaveLevel() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        // formatting level text
        List<string> levelLinesToSave = new List<string>();
        string[] levelTextLines = levelExplosionsText.text.TrimEnd().Split('\n');
        bool hasDelay = false;
        bool hasExplosion = false;
        foreach (string levelTextLine in levelTextLines) {
            if (!levelTextLine.Contains("W")) {
                levelLinesToSave.Add(levelTextLine + " AR");
                hasExplosion = true;
            } else {
                levelLinesToSave.Add(levelTextLine);
                hasDelay = true;
            }
        }

        // verify there is at least one delay, explosion
        if (!hasDelay || !hasExplosion) {
            SetAndFadeOutputText(SAVE_FAILED_REQUIRED_ELEMENTS_TEXT);
            return;
        }

        // generate final string to save to file
        string levelStringToSave = "";
        foreach (string line in levelLinesToSave) {
            levelStringToSave += line + "\n";
        }
        levelStringToSave = levelStringToSave.TrimEnd();

        // Save to resources file
        Debug.Log(levelStringToSave);

        SetAndFadeOutputText(SAVE_SUCCESS_TEXT);
    }

    //------------------
    // Message Handling
    //------------------
    private void SetAndFadeOutputText(string message) {
        outputText.text = message;
        StartCoroutine(FadeOutText(outputText, 5.0f));
    }

    IEnumerator FadeOutText(Text textObject, float fadeTime) {
        Color textColor = textObject.color;
        Color tmpColor = textObject.color;
        while (tmpColor.a > 0f) {
            tmpColor.a -= Time.deltaTime / fadeTime;
            textObject.color = tmpColor;
            if (tmpColor.a <= 0f) {
                tmpColor.a = 0f;
            }
            yield return null;
        }
        textObject.color = tmpColor;
    }
}