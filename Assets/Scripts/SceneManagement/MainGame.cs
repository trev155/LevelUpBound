/*
 * Class that manages general MainGame things, such as user interface.
 */
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour {
    // Fields
    public Text currentLevelLabel;
    public Text currentGameModeLabel;
    public Image audioEnabledImage;
    public Image audioDisabledImage;

    /*
     * Initialization
     */
    private void Awake() {
        AspectRatioManager.AdjustScreen();
        InitializeAudioUI();
        Theme.SetTheme();
    }

    /*
     * Back button handler. If we came from the LevelSelector, the back button should go back to the MainMenu.
     */
    public void BackButtonPressed() {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
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
        if (GameContext.ModalActive) {
            return;
        }

        if (GameContext.AudioEnabled) {
            audioEnabledImage.gameObject.SetActive(false);
            audioDisabledImage.gameObject.SetActive(true);
            AudioManager.Instance.StopBackgroundMusic();
        } else {
            audioEnabledImage.gameObject.SetActive(true);
            audioDisabledImage.gameObject.SetActive(false);
        }
        GameContext.AudioEnabled = !GameContext.AudioEnabled;
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        // if we just turned audio on, start playing BGM
        if (GameContext.AudioEnabled) {
            AudioManager.Instance.PlayBackgroundMusic();
        }

        Memory.SaveData();
    }
}
