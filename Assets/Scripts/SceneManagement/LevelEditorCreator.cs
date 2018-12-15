/*
 * Handles the LevelEditor page.
 */
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelEditorCreator : MonoBehaviour {
    public Text levelText;

    private const int GRID_DIMENSION = 5;
    private List<List<bool>> tileActivationStates;

    private void Awake() {
        AspectRatioManager.AdjustScreenElements();
        ThemeManager.SetTheme();
        
    }

    private void Start() {
        InitializeTileActivationStates();
        levelText.text = "";
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
            for (int col = 1; col < GRID_DIMENSION; col++) {
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
    public void ToggleGameTile() {
        // get game object of tile pressed
        GameObject tilePressed = EventSystem.current.currentSelectedGameObject;
        Debug.Log(tilePressed);

        // toggle the tile
        int row = int.Parse(tilePressed.name.Substring(0, 1));
        int col = int.Parse(tilePressed.name.Substring(2, 1));
        tileActivationStates[row][col] = !tileActivationStates[row][col];

        // change colour
        Color32 newColor;
        if (tileActivationStates[row][col]) {
            newColor = new Color32(140, 130, 201, 255);
        } else {
            newColor = new Color32(172, 209, 255, 255);
        }
        Image tileImage = tilePressed.GetComponent<Image>();
        tileImage.color = newColor;
    }

    public void SubmitLevelSection() {
        // Get all selected buttons
    }

    public void AddDelay() {
        // keyboard input

        // add to level text

    }

}