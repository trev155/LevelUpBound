/*
 * Class that handles MainMenu actions.
 */
using UnityEngine;

public class MainMenu : MonoBehaviour {
    /*
     * Initialization
     */
    private void Awake() {
        AspectRatioManager.AdjustScreen();
        Theme.SetTheme();

        // For the LevelSelector, want to set default page to Easy
        GameContext.GameMode = Mode.EASY;
        GameContext.LevelSelectionPage = 1;
    }

    /*
     * Initialize globals and start the game. Loads the MainGame scene.
     */
    private void ChooseMode(Mode mode) {
        AudioSingleton.Instance.PlaySound(AudioSingleton.BUTTON_DING);
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
        AudioSingleton.Instance.PlaySound(AudioSingleton.BUTTON_DING);
        GameContext.PreviousPageContext = "MainMenu";
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelector");
    }

    /*
     * Load the Level Editor scene.
     */
    public void LevelEditor() {
        AudioSingleton.Instance.PlaySound(AudioSingleton.BUTTON_DING);
        GameContext.PreviousPageContext = "MainMenu";
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelEditor");
    }

    /*
    * Load the Instructions page.
    */
    public void Instructions() {
        AudioSingleton.Instance.PlaySound(AudioSingleton.BUTTON_DING);
        GameContext.PreviousPageContext = "MainMenu";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Instructions");
    }

    /*
     * Load the Options scene.
     */
    public void Options() {
        AudioSingleton.Instance.PlaySound(AudioSingleton.BUTTON_DING);
        GameContext.PreviousPageContext = "MainMenu";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Options");
    }
    
    /*
     * Load the About page.
     */
    public void About() {
        AudioSingleton.Instance.PlaySound(AudioSingleton.BUTTON_DING);
        GameContext.PreviousPageContext = "MainMenu";
        UnityEngine.SceneManagement.SceneManager.LoadScene("About");
    }

    /*
     * Exit the application.
     */
    public void ExitApp() {
        AudioSingleton.Instance.PlaySound(AudioSingleton.BUTTON_DING);
        Debug.Log("Exiting the Application");
        Application.Quit();
    }
}
