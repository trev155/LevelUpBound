using UnityEngine;

public class MainMenu : MonoBehaviour {
    private void ChooseMode(string mode) {
        GameContext.GameMode = mode;
        GameContext.CurrentLevel = 1;
        GameContext.PreviousPageContext = "MainMenu";
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }

    public void ChooseClassicMode() {
        ChooseMode("classic");
    }

    public void ChooseCustomMode() {
        ChooseMode("custom");
    }

    public void LevelSelector() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelector");
    }
}
