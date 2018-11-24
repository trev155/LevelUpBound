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

    // Game Mode Selection
    public Text gameModeSelectedText;
    public Image leftArrow;
    public Image rightArrow;
    public Text gameModeDescription;

    /*
     * Initialization
     */
    private void Awake() {
        AspectRatioManager.AdjustScreen();
        ThemeManager.SetTheme();
    }

    /*
     * Initialization, after Awake()
     */
    private void Start() {
        // Game Mode Selector
        SetGameModeSelectedModeText();
        BlurArrows();
        SetGameModeDescriptionText();
    }

    /*
     * Play Button.
     * Initialize globals and start the game. Loads the MainGame scene.
     */
    public void Play() {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.GameMode = GameContext.MainMenuGameMode;
        GameContext.CurrentLevel = 1;
        GameContext.PreviousPageContext = "MainMenu";
        GameContext.LevelSelection = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }
   
    public void ScrollLeft() {
        if (GameContext.MainMenuGameMode == Mode.TUTORIAL) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        if (GameContext.MainMenuGameMode == Mode.CHALLENGE) {
            GameContext.MainMenuGameMode = Mode.ADVANCED;
        } else if (GameContext.MainMenuGameMode == Mode.ADVANCED) {
            GameContext.MainMenuGameMode = Mode.CLASSIC;
        } else if (GameContext.MainMenuGameMode == Mode.CLASSIC) {
            GameContext.MainMenuGameMode = Mode.EASY;
        } else if (GameContext.MainMenuGameMode == Mode.EASY) {
            GameContext.MainMenuGameMode = Mode.TUTORIAL;
        }

        SetGameModeSelectedModeText();
        BlurArrows();
        SetGameModeDescriptionText();
    }

    public void ScrollRight() {
        if (GameContext.MainMenuGameMode == Mode.CHALLENGE) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        if (GameContext.MainMenuGameMode == Mode.TUTORIAL) {
            GameContext.MainMenuGameMode = Mode.EASY;
        } else if (GameContext.MainMenuGameMode == Mode.EASY) {
            GameContext.MainMenuGameMode = Mode.CLASSIC;
        } else if (GameContext.MainMenuGameMode == Mode.CLASSIC) {
            GameContext.MainMenuGameMode = Mode.ADVANCED;
        } else if (GameContext.MainMenuGameMode == Mode.ADVANCED) {
            GameContext.MainMenuGameMode = Mode.CHALLENGE;
        } 

        SetGameModeSelectedModeText();
        BlurArrows();
        SetGameModeDescriptionText();
    }

    private void SetGameModeSelectedModeText() {
        gameModeSelectedText.text = GameMode.GetName(GameContext.MainMenuGameMode);
    }

    private void BlurArrows() {
        Utils.UndoGrayoutImage(leftArrow);
        Utils.UndoGrayoutImage(rightArrow);
        if (GameContext.MainMenuGameMode == Mode.TUTORIAL) {
            Utils.GrayoutImage(leftArrow);
        }
        if (GameContext.MainMenuGameMode == Mode.CHALLENGE) {
            Utils.GrayoutImage(rightArrow);
        }
    }

    private void SetGameModeDescriptionText() {
        gameModeDescription.text = GameMode.GetModeDescription(GameContext.MainMenuGameMode);
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
