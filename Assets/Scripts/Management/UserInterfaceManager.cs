/*
 * Class that manages the UI on the Game Page.
 * Doesn't handle the arrow control buttons.
 */

using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour {
    public Text currentLevelLabel;
    public Text currentGameModeLabel;

    public void BackButtonPressed() {
        GameContext.LoadPreviousPage();
    }

    public void UpdateLevelText() {
        currentLevelLabel.text = "Current Level: " + GameContext.CurrentLevel;
    }

    public void UpdateGameModeText() {
        currentGameModeLabel.text = "Game Mode: " + GameContext.GameMode;
    }
}
