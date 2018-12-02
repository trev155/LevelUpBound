/*
 * Class that handles MainMenu actions. 
 * The MainMenu is the main "landing page" for the application.
 */
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public Transform modalContainer;
    public ModalConfirmDeny modalConfirmDeny;
    private ModalConfirmDeny exitModal;

    public Text gameModeSelectedText;
    public Image leftArrow;
    public Image rightArrow;
    public Text gameModeDescription;

    private void Awake() {
        PersistentStorage.LoadData();   // Load saved data here, as it is the main landing page for the app

        AspectRatioManager.AdjustScreenElements();
        ThemeManager.SetTheme();
    }

    private void Start() {
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

    public void PlayButtonHandler() {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.GameMode = GameContext.MainMenuGameMode;
        GameContext.CurrentLevel = 1;
        GameContext.PreviousPageContext = SceneName.MAIN_MENU;
        GameContext.PlayedFromLevelSelector = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetSceneNameString(SceneName.MAIN_GAME));
    }
   
    public void ScrollLeft() {
        if (GameContext.ModalActive) {
            return;
        }
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
        if (GameContext.ModalActive) {
            return;
        }
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

    // Button handlers for the different scenes
    public void LevelSelector() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.PreviousPageContext = SceneName.MAIN_MENU;
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetSceneNameString(SceneName.LEVEL_SELECTOR));
    }

    public void LevelEditor() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.PreviousPageContext = SceneName.MAIN_MENU;
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetSceneNameString(SceneName.LEVEL_EDITOR));
    }

    public void Instructions() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.PreviousPageContext = SceneName.MAIN_MENU;
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetSceneNameString(SceneName.INSTRUCTIONS));
    }

    public void Options() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.PreviousPageContext = SceneName.MAIN_MENU;
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetSceneNameString(SceneName.OPTIONS));
    }
    
    public void About() {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.PreviousPageContext = SceneName.MAIN_MENU;
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetSceneNameString(SceneName.ABOUT));
    }

    // Exit App Button Handling
    public void ExitAppButton() {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        exitModal = Instantiate(modalConfirmDeny, modalContainer);
        exitModal.InitializeExitModal();

        // Set handlers for the Yes / No buttons
        Button yesButton = GameObject.Find("YesButton").GetComponent<Button>();
        yesButton.onClick.AddListener(ConfirmExit);
        Button noButton = GameObject.Find("NoButton").GetComponent<Button>();
        noButton.onClick.AddListener(DenyExit);
    }

    public void ConfirmExit() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        exitModal.Close();
        ExitApp();
    }

    public void DenyExit() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        exitModal.Close();
    }

    private void ExitApp() {
        Debug.Log("Exiting the Application");
        Application.Quit();
    }
}
