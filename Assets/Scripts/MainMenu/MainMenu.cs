/*
 * Class that handles MainMenu actions.
 */
using UnityEngine;

public class MainMenu : MonoBehaviour {
    /*
     * Initialization
     */
    private void Awake() {
        Theme.SetTheme();
    }

    /*
     * Initialize globals and start the game. Loads the MainGame scene.
     */
    private void ChooseMode(Mode mode) {
        GameContext.GameMode = mode;
        GameContext.CurrentLevel = 1;
        GameContext.PreviousPageContext = "MainMenu";
        GameContext.LevelSelection = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }

    public void ChooseClassicMode() {
        ChooseMode(Mode.CLASSIC);
    }

    public void ChooseAdvancedMode() {
        ChooseMode(Mode.ADVANCED);
    }

    public void ChooseEasyMode() {
        ChooseMode(Mode.EASY);
    }

    public void ChooseTutorialMode() {
        ChooseMode(Mode.TUTORIAL);
    }

    public void ChooseChallengeMode() {
        ChooseMode(Mode.CHALLENGE);
    }

    /*
     * Load the LevelSelector scene.
     */
    public void LevelSelector() {
        GameContext.PreviousPageContext = "MainMenu";
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelector");
    }

    /*
     * Load the Level Editor scene.
     */
    public void LevelEditor() {
        GameContext.PreviousPageContext = "MainMenu";
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelEditor");
    }

    /*
    * Load the Instructions page.
    */
    public void Instructions() {
        GameContext.PreviousPageContext = "MainMenu";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Instructions");
    }

    /*
     * Load the Options scene.
     */
    public void Options() {
        GameContext.PreviousPageContext = "MainMenu";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Options");
    }
    
    /*
     * Load the About page.
     */
    public void About() {
        GameContext.PreviousPageContext = "MainMenu";
        UnityEngine.SceneManagement.SceneManager.LoadScene("About");
    }

    /*
     * Exit the application.
     */
    public void ExitApp() {
        Debug.Log("Exiting the Application");
        Application.Quit();
    }
}
