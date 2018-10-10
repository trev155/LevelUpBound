using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour {
    public Text currentLevelLabel;
    public Text currentGameModeLabel;

    public void BackButtonPressed() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void UpdateLevelText() {
        currentLevelLabel.text = "Current Level: " + GameContext.CurrentLevel;
    }

    public void UpdateGameModeText() {
        currentGameModeLabel.text = "Game Mode: " + GameContext.GameMode;
    }
}
