﻿/*
 * Handles MainGame scene activities.
 */
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour {
    public Text currentLevelLabel;
    public Text currentGameModeLabel;
    public Image audioEnabledImage;
    public Image audioDisabledImage;

    private void Awake() {
        AspectRatioManager.AdjustScreenElements();
        InitializeAudioUI();
        ThemeManager.SetTheme();
    }

    public void BackButtonPressed() {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        SceneManager.LoadPreviousContextPage();
        if (GameContext.PreviousPageContext == SceneName.LEVEL_SELECTOR) {
            GameContext.PreviousPageContext = SceneName.MAIN_MENU;
        }
    }

    public void UpdateLevelText() {
        string levelText;
        if (GameContext.GameMode == Mode.CUSTOM) {
            levelText = "Cus" + GameContext.LevelEditorSlotSelection;
        } else {
            levelText = GameContext.CurrentLevel + "";
        }

        currentLevelLabel.text = "Current Level: " + levelText;
    }

    public void UpdateGameModeText() {
        currentGameModeLabel.text = "Game Mode: " + GameMode.GetName(GameContext.GameMode);
    }

    private void InitializeAudioUI() {
        if (GameContext.AudioEnabled) {
            audioEnabledImage.gameObject.SetActive(true);
            audioDisabledImage.gameObject.SetActive(false);
        } else if (!GameContext.AudioEnabled) {
            audioEnabledImage.gameObject.SetActive(false);
            audioDisabledImage.gameObject.SetActive(true);
        }
    }

    public void ToggleAudio() {
        if (GameContext.ModalActive) {
            return;
        }

        if (GameContext.AudioEnabled) {
            audioEnabledImage.gameObject.SetActive(false);
            audioDisabledImage.gameObject.SetActive(true);
            AudioManager.Instance.StopBackgroundMusic();
        } else {
            audioEnabledImage.gameObject.SetActive(true);
            audioDisabledImage.gameObject.SetActive(false);
        }
        GameContext.AudioEnabled = !GameContext.AudioEnabled;
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        // if we just turned audio on, start playing BGM
        if (GameContext.AudioEnabled) {
            AudioManager.Instance.PlayBackgroundMusic();
        }

        PersistentStorage.SaveData();
    }
}
