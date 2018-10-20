/*
 * Class that manages the UI on the Game Page (MainGame scene).
 * Doesn't handle the arrow control buttons.
 */

using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour {
    // Fields
    public Text currentLevelLabel;
    public Text currentGameModeLabel;
    public Image audioEnabledImage;
    public Image audioDisabledImage;

    /*
     * Initialization
     */
    private void Awake() {
        InitializeAudioUI();
    }

    /*
     * Back button handler. If we came from the LevelSelector, the back button should go back to the MainMenu.
     */
    public void BackButtonPressed() {
        GameContext.LoadPreviousPage();
        if (GameContext.PreviousPageContext == "LevelSelector") {
            GameContext.PreviousPageContext = "MainMenu";
        }
    }

    /* 
     * Update current level text.
     */
    public void UpdateLevelText() {
        currentLevelLabel.text = "Current Level: " + GameContext.CurrentLevel;
    }

    /*
     * Set game mode text.
     */
    public void UpdateGameModeText() {
        currentGameModeLabel.text = "Game Mode: " + GameMode.GetName(GameContext.GameMode);
    }

    /*
    * When the scene is initialized, decide whether to show the AudioEnabled or the AudioDisabled image.
    */
    private void InitializeAudioUI() {
        if (GameContext.AudioEnabled) {
            audioEnabledImage.gameObject.SetActive(true);
            audioDisabledImage.gameObject.SetActive(false);
        } else if (!GameContext.AudioEnabled) {
            audioEnabledImage.gameObject.SetActive(false);
            audioDisabledImage.gameObject.SetActive(true);
        }
    }

    /*
     * Turn audio on or off.
     */
    public void ToggleAudio() {
        if (GameContext.AudioEnabled) {
            audioEnabledImage.gameObject.SetActive(false);
            audioDisabledImage.gameObject.SetActive(true);
        } else {
            audioEnabledImage.gameObject.SetActive(true);
            audioDisabledImage.gameObject.SetActive(false);
        }

        GameContext.AudioEnabled = !GameContext.AudioEnabled;
    }
}
