/*
 * Handles the Level Editor Menu.
 */
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class LevelEditorMenu : MonoBehaviour {
    public Text outputText;

    private const string LEVEL_DOES_NOT_EXIST_TEXT = "Level does not exist.";
    private int currentSelectedLevel;

    private void Awake() {
        AspectRatioManager.AdjustScreenElements();
        ThemeManager.SetTheme();

        currentSelectedLevel = 1;
        GameContext.LevelEditorSlotSelection = currentSelectedLevel;
        outputText.text = "";
    }

    /*
    * Back button handler
    */
    public void BackButton() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        SceneManager.LoadPreviousContextPage();
    }


    //----------------------
    // Level Slot Selection
    //----------------------
    public void LevelSlotSelection() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        int levelSelected = GetLevelSlotSelected();
        currentSelectedLevel = levelSelected;
        GameContext.LevelEditorSlotSelection = currentSelectedLevel;
        SetLevelSlotSelectedText(levelSelected);       
    }

    private int GetLevelSlotSelected() {
        string buttonSelected = EventSystem.current.currentSelectedGameObject.name;
        return int.Parse(buttonSelected.Substring(buttonSelected.Length - 1, 1));
    }

    private void SetLevelSlotSelectedText(int level) {
        Text selectedLevelSlotText = GameObject.Find("SelectedLevelSlotText").GetComponent<Text>();
        selectedLevelSlotText.text = level.ToString();
    }


    //-----------------
    // Button Handlers
    //-----------------
    public void CreateButton() {
        AudioManager.Instance.PlaySound(AudioManager.BUTTON_DING);
        GameContext.PreviousPageContext = SceneName.LEVEL_EDITOR_MENU;
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetSceneNameString(SceneName.LEVEL_EDITOR_CREATOR));
    }

    public void PlayButton() {
        AudioManager.Instance.PlaySound(AudioManager.BUTTON_DING);

        // Check if level even exists
        if (PlayerPrefs.GetString(GameContext.LevelEditorSlotSelection + "_CUSTOM").Length <= 0) {
            outputText.text = LEVEL_DOES_NOT_EXIST_TEXT;
            return;
        }
       
        GameContext.GameMode = Mode.CUSTOM;
        GameContext.PreviousPageContext = SceneName.LEVEL_EDITOR_MENU;
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetSceneNameString(SceneName.MAIN_GAME));
    }
}