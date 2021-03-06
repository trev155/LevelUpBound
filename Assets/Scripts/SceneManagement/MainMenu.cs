﻿/*
 * Class that handles MainMenu actions. 
 */
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public Transform modalContainer;
    public ModalConfirmDeny modalConfirmDeny;
    public ModalInfo modalInfo;
    private ModalConfirmDeny exitModal;
    private ModalConfirmDeny saveOverwriteModal;
    private ModalInfo saveNotFoundModal;

    public Text gameModeSelectedText;
    public Image leftArrow;
    public Image rightArrow;
    public Text gameModeDescription;

    private void Awake() {
        // for local testing. this is already being called in the Intro scene, but that's fine because intro scene doesn't change game context at all
        PersistentStorage.LoadData();

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
    
    //--------------
    // Play Button
    //--------------
    public void PlayButtonHandler() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        if (SavedGameManager.SavedGameExists() && GameContext.MainMenuGameMode != Mode.TUTORIAL) {
            OpenSavedGameOverwriteModal();
        } else {
            Play();
        }
    }

    private void OpenSavedGameOverwriteModal() {
        saveOverwriteModal = Instantiate(modalConfirmDeny, modalContainer);
        saveOverwriteModal.InitializePlaySaveOverwriteModal(GameContext.SavedGameMode, GameContext.SavedGameLevel);

        Button yesButton = GameObject.Find("YesButton").GetComponent<Button>();
        yesButton.onClick.AddListener(ConfirmPlayModal);
        Button noButton = GameObject.Find("NoButton").GetComponent<Button>();
        noButton.onClick.AddListener(ClosePlayModal);
    }

    private void Play() {
        GameContext.GameMode = GameContext.MainMenuGameMode;
        GameContext.CurrentLevel = 1;
        GameContext.PreviousPageContext = SceneName.MAIN_MENU;
        GameContext.PlayedFromLevelSelector = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetSceneNameString(SceneName.MAIN_GAME));
    }

    private void ConfirmPlayModal() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        saveOverwriteModal.Close();
        Play();
    }

    private void ClosePlayModal() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        saveOverwriteModal.Close();
    }

    //--------------
    // Load Button
    //--------------
    public void LoadButtonHandler() {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        if (SavedGameManager.SavedGameExists()) {
            GameContext.GameMode = GameContext.SavedGameMode;
            GameContext.CurrentLevel = GameContext.SavedGameLevel;
            GameContext.PreviousPageContext = SceneName.MAIN_MENU;
            GameContext.PlayedFromLevelSelector = false;
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetSceneNameString(SceneName.MAIN_GAME));
        } else {
            OpenLoadGameSaveNotFoundModal();
        }
    }

    private void OpenLoadGameSaveNotFoundModal() {
        saveNotFoundModal = Instantiate(modalInfo, modalContainer);
        saveNotFoundModal.InitializeLoadModal();
    }

    //------------------
    // Scroll Game Mode
    //------------------
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

    //------------------------------------------
    // Button handlers for the different scenes
    //------------------------------------------
    public void LevelSelector() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.PreviousPageContext = SceneName.MAIN_MENU;
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetSceneNameString(SceneName.LEVEL_SELECTOR));
    }

    public void LevelEditorMenu() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.PreviousPageContext = SceneName.MAIN_MENU;
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetSceneNameString(SceneName.LEVEL_EDITOR_MENU));
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

    //-------------
    // Exit Button
    //-------------
    public void ExitAppButton() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        OpenExitModal();
    }

    private void OpenExitModal() {
        exitModal = Instantiate(modalConfirmDeny, modalContainer);
        exitModal.InitializeExitModal();
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
