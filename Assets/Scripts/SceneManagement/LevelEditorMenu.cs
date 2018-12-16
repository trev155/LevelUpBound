/*
 * Handles the Level Editor Menu.
 */
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class LevelEditorMenu : MonoBehaviour {
    private int currentSelectedLevel;
    

    private void Awake() {
        AspectRatioManager.AdjustScreenElements();
        ThemeManager.SetTheme();

        currentSelectedLevel = 1;
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
        int levelSelected = GetLevelSlotSelected();
        currentSelectedLevel = levelSelected;
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
        GameContext.LevelEditorSlotSelection = currentSelectedLevel;
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetSceneNameString(SceneName.LEVEL_EDITOR_CREATOR));
    }

    public void PlayButton() {
        AudioManager.Instance.PlaySound(AudioManager.BUTTON_DING);
        // go to game scene
    }
}