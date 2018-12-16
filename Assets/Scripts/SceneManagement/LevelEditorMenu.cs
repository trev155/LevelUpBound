/*
 * Handles the Level Editor Menu.
 */
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class LevelEditorMenu : MonoBehaviour {
    public Text outputText;
    public Text levelSlotOccupiedText;
    public Transform modalContainer;
    public ModalConfirmDeny modalConfirmDeny;

    private const string LEVEL_DOES_NOT_EXIST_TEXT = "Level does not exist.";
    private int currentSelectedLevel;
    private ModalConfirmDeny modalOverwriteWarning;
    private ModalConfirmDeny modalDeleteWarning;

    private void Awake() {
        AspectRatioManager.AdjustScreenElements();
        ThemeManager.SetTheme();

        currentSelectedLevel = 1;
        GameContext.LevelEditorSlotSelection = currentSelectedLevel;
        outputText.text = "";
        SetLevelSlotOccupiedText(currentSelectedLevel);
    }

    public void BackButton() {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        SceneManager.LoadPreviousContextPage();
    }

    //----------------------
    // Level Slot Selection
    //----------------------
    public void LevelSlotSelection() {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        int levelSelected = GetLevelSlotSelected();
        currentSelectedLevel = levelSelected;
        GameContext.LevelEditorSlotSelection = currentSelectedLevel;
        SetLevelSlotSelectedText(levelSelected);
        SetLevelSlotOccupiedText(currentSelectedLevel);
    }

    private int GetLevelSlotSelected() {
        string buttonSelected = EventSystem.current.currentSelectedGameObject.name;
        return int.Parse(buttonSelected.Substring(buttonSelected.Length - 1, 1));
    }

    private void SetLevelSlotSelectedText(int level) {
        Text selectedLevelSlotText = GameObject.Find("SelectedLevelSlotText").GetComponent<Text>();
        selectedLevelSlotText.text = level.ToString();
    }

    private void SetLevelSlotOccupiedText(int levelSlot) {
        if (PersistentStorage.IsLevelSlotOccupied(levelSlot)) {
            levelSlotOccupiedText.text = "(Level Slot currently holds a level)";
        } else {
            levelSlotOccupiedText.text = "";
        }
    }

    //-----------------
    // Button Handlers
    //-----------------
    public void CreateButton() {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlaySound(AudioManager.BUTTON_DING);
        if (CreationWouldBeAnOverwrite()) {
            modalOverwriteWarning = Instantiate(modalConfirmDeny, modalContainer);
            modalOverwriteWarning.InitilaizeLevelCreatorOverwriteModal();

            Button yesButton = GameObject.Find("YesButton").GetComponent<Button>();
            yesButton.onClick.AddListener(ConfirmWarning);
            Button noButton = GameObject.Find("NoButton").GetComponent<Button>();
            noButton.onClick.AddListener(DenyWarning);
        } else {
            GameContext.PreviousPageContext = SceneName.LEVEL_EDITOR_MENU;
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetSceneNameString(SceneName.LEVEL_EDITOR_CREATOR));
        }
    }

    private bool CreationWouldBeAnOverwrite() {
        return PersistentStorage.IsLevelSlotOccupied(currentSelectedLevel);
    }

    private void ConfirmWarning() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        modalOverwriteWarning.Close();

        GameContext.PreviousPageContext = SceneName.LEVEL_EDITOR_MENU;
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetSceneNameString(SceneName.LEVEL_EDITOR_CREATOR));
    }

    private void DenyWarning() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        modalOverwriteWarning.Close();
    }

    public void PlayButton() {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlaySound(AudioManager.BUTTON_DING);

        if (!PersistentStorage.IsLevelSlotOccupied(currentSelectedLevel)) {
            outputText.text = LEVEL_DOES_NOT_EXIST_TEXT;
            return;
        }
       
        GameContext.GameMode = Mode.CUSTOM;
        GameContext.PreviousPageContext = SceneName.LEVEL_EDITOR_MENU;
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetSceneNameString(SceneName.MAIN_GAME));
    }

    public void EditButton() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        // TODO
        // check if slot even exists. if not, output message
    }
    
    public void DeleteButton() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        if (PersistentStorage.IsLevelSlotOccupied(currentSelectedLevel)) {
            modalDeleteWarning = Instantiate(modalConfirmDeny, modalContainer);
            modalDeleteWarning.InitializeLevelEditorDeletionModal();

            Button yesButton = GameObject.Find("YesButton").GetComponent<Button>();
            yesButton.onClick.AddListener(ConfirmDelete);
            Button noButton = GameObject.Find("NoButton").GetComponent<Button>();
            noButton.onClick.AddListener(DenyDelete);
        } else {
            outputText.text = LEVEL_DOES_NOT_EXIST_TEXT;
        }
    }

    private void ConfirmDelete() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        modalDeleteWarning.Close();

        PersistentStorage.DeleteCustomLevel(currentSelectedLevel);
        SetLevelSlotOccupiedText(currentSelectedLevel);
    }

    private void DenyDelete() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        modalDeleteWarning.Close();
    }
}
