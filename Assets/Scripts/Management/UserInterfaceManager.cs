/*
 * Class that manages the UI on the Game Page.
 * Doesn't handle the arrow control buttons.
 */

using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour {
    public Text currentLevelLabel;
    public Text currentGameModeLabel;

    public Image audioEnabledImage;
    public Image audioDisabledImage;

    public void BackButtonPressed() {
        GameContext.LoadPreviousPage();
    }

    public void UpdateLevelText() {
        currentLevelLabel.text = "Current Level: " + GameContext.CurrentLevel;
    }

    public void UpdateGameModeText() {
        currentGameModeLabel.text = "Game Mode: " + GameContext.GameMode;
    }

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
