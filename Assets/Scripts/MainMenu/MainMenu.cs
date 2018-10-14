/*
 * Class that handles MainMenu actions.
 */
using UnityEngine;

public class MainMenu : MonoBehaviour {
    /*
     * Initialize globals and start the game. Loads the MainGame scene.
     */
    private void ChooseMode(string mode) {
        GameContext.GameMode = mode;
        GameContext.CurrentLevel = 1;
        GameContext.PreviousPageContext = "MainMenu";
        GameContext.LevelSelection = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }

    public void ChooseClassicMode() {
        ChooseMode("classic");
    }

    public void ChooseCustomMode() {
        ChooseMode("custom");
    }

    /*
     * Load the LevelSelector scene.
     */
    public void LevelSelector() {
        GameContext.PreviousPageContext = "MainMenu";
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelector");
    }

    /*
     * Load the Options scene.
     */
    public void Options() {
        GameContext.PreviousPageContext = "MainMenu";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Options");
    }
}
