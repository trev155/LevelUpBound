/*
 * Class that handles MainMenu actions.
 */
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    // Modals
    public Transform modalContainer;
    public ModalConfirmDeny modalConfirmDeny;
    private ModalConfirmDeny modal;
    
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
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
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
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.PreviousPageContext = "MainMenu";
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelector");
    }

    /*
     * Load the Level Editor scene.
     */
    public void LevelEditor() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.PreviousPageContext = "MainMenu";
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelEditor");
    }

    /*
    * Load the Instructions page.
    */
    public void Instructions() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.PreviousPageContext = "MainMenu";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Instructions");
    }

    /*
     * Load the Options scene.
     */
    public void Options() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.PreviousPageContext = "MainMenu";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Options");
    }
    
    /*
     * Load the About page.
     */
    public void About() {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.PreviousPageContext = "MainMenu";
        UnityEngine.SceneManagement.SceneManager.LoadScene("About");
    }

    // Close Button handlers
    public void ExitButton() {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        modal = Instantiate(modalConfirmDeny, modalContainer);
        modal.SetModalTextCloseApp();

        // Set handlers for the Yes / No buttons
        Button yesButton = GameObject.Find("YesButton").GetComponent<Button>();
        yesButton.onClick.AddListener(ConfirmExit);
        Button noButton = GameObject.Find("NoButton").GetComponent<Button>();
        noButton.onClick.AddListener(DenyExit);
    }

    public void ConfirmExit() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        modal.CloseModalWindow();
        ExitApp();
    }

    public void DenyExit() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        modal.CloseModalWindow();
    }

    /*
     * Exit the application.
     */
    public void ExitApp() {
        Debug.Log("Exiting the Application");
        Application.Quit();
    }
}
