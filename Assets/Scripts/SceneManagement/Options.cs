/*
 * A page where the user can set various options, such as volume and theme.
 */
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {
    public Text audioText;
    public Image audioEnabledImage;
    public Image audioDisabledImage;
    public Slider backgroundMusicVolumeSlider;
    public Slider effectsVolumeSlider;
    public Image controlSchemeArrowImage;
    public Image controlSchemeClickImage;

    public Transform modalContainer;
    public ModalConfirmDeny modalConfirmDeny;
    private ModalConfirmDeny resetProgressModal;
    private ModalConfirmDeny resetOptionsModal;

    private void Awake() {
        AspectRatioManager.AdjustScreenElements();
        InitializeAudioUI();
        SetInitialVolume();
        backgroundMusicVolumeSlider.onValueChanged.AddListener(delegate { BackgroundMusicVolumeSliderChanged(); });
        effectsVolumeSlider.onValueChanged.AddListener(delegate { EffectsVolumeSliderChanged(); });
        ThemeManager.SetTheme();
        SetControlSchemeImage();
    }

    public void BackButton() {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        SceneManager.LoadPreviousContextPage();
    }

    //-------
    // Audio
    //-------
    private void InitializeAudioUI() {
        if (GameContext.AudioEnabled) {
            audioText.text = "ON";
            audioEnabledImage.gameObject.SetActive(true);
            audioDisabledImage.gameObject.SetActive(false);
        } else if (!GameContext.AudioEnabled) {
            audioText.text = "OFF";
            audioEnabledImage.gameObject.SetActive(false);
            audioDisabledImage.gameObject.SetActive(true);
        }
    }

    public void ToggleAudio() {
        if (GameContext.ModalActive) {
            return;
        }

        if (GameContext.AudioEnabled) {
            audioText.text = "OFF";
            audioEnabledImage.gameObject.SetActive(false);
            audioDisabledImage.gameObject.SetActive(true);
            AudioManager.Instance.StopBackgroundMusic();
        } else {
            audioText.text = "ON";
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

    private void SetInitialVolume() {
        backgroundMusicVolumeSlider.value = GameContext.CurrentMusicVolume;
        effectsVolumeSlider.value = GameContext.CurrentEffectsVolume;
    }

    public void BackgroundMusicVolumeSliderChanged() {
        if (GameContext.ModalActive) {
            return;
        }

        GameContext.CurrentMusicVolume = backgroundMusicVolumeSlider.value;
        AudioManager.Instance.AdjustBackgroundMusicVolume();
        PersistentStorage.SaveData();
    }

    public void EffectsVolumeSliderChanged() {
        if (GameContext.ModalActive) {
            return;
        }
        GameContext.CurrentEffectsVolume = effectsVolumeSlider.value;
        AudioManager.Instance.AdjustEffectsVolume();
        PersistentStorage.SaveData();
    }

    //--------
    // Themes
    //--------
    private void SetTheme(Theme theme) {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.CurrentTheme = theme;
        ThemeManager.SetTheme();
        PersistentStorage.SaveData();
    }

    public void SetNormalTheme() {
        SetTheme(Theme.NORMAL);
    }

    public void SetLightTheme() {
        SetTheme(Theme.LIGHT);
    }

    public void SetDarkTheme() {
        SetTheme(Theme.DARK);
    }

    public void SetVibrantTheme() {
        SetTheme(Theme.VIBRANT);
    }

    //------------------------
    // Control Scheme Buttons
    //------------------------
    public void SetArrowControls() {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.ControlScheme = ControlMode.ARROW;
        SetControlSchemeImage();
        PersistentStorage.SaveData();

    }

    public void SetClickControls() {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.ControlScheme = ControlMode.CLICK;
        SetControlSchemeImage();
        PersistentStorage.SaveData();
    }

    private void SetControlSchemeImage() {
        if (GameContext.ControlScheme == ControlMode.ARROW) {
            controlSchemeArrowImage.gameObject.SetActive(true);
            controlSchemeClickImage.gameObject.SetActive(false);
        }
        if (GameContext.ControlScheme == ControlMode.CLICK) {
            controlSchemeArrowImage.gameObject.SetActive(false);
            controlSchemeClickImage.gameObject.SetActive(true);
        }
    }

    //---------------------------------
    // Reset Level Completion Progress
    //---------------------------------
    public void ResetProgressButtonHandler() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        resetProgressModal = Instantiate(modalConfirmDeny, modalContainer);
        resetProgressModal.InitializeResetProgressModal();

        // Set handlers for the Yes / No buttons
        Button yesButton = GameObject.Find("YesButton").GetComponent<Button>();
        yesButton.onClick.AddListener(AllowResetProgress);
        Button noButton = GameObject.Find("NoButton").GetComponent<Button>();
        noButton.onClick.AddListener(DenyResetProgress);
    }

    private void AllowResetProgress() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        PersistentStorage.ResetProgress();
        GameContext.InitializeCompletedLevels();
        resetProgressModal.Close();
    }

    private void DenyResetProgress() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        resetProgressModal.Close();
    }

    //--------------------------
    // Reset to default options
    //--------------------------
    public void ResetDefaultOptionsButtonHandler() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        resetOptionsModal = Instantiate(modalConfirmDeny, modalContainer);
        resetOptionsModal.InitializeResetOptionsModal();

        // Set handlers for the Yes / No button
        Button yesButton = GameObject.Find("YesButton").GetComponent<Button>();
        yesButton.onClick.AddListener(AllowResetOptions);
        Button noButton = GameObject.Find("NoButton").GetComponent<Button>();
        noButton.onClick.AddListener(DenyResetOptions);
    }

    private void AllowResetOptions() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.SetDefaultGameContextValues();
        PersistentStorage.SaveData();
        SceneManager.LoadPreviousContextPage();
        if (!AudioManager.Instance.IsBackgroundMusicPlaying()) {
            AudioManager.Instance.PlayBackgroundMusic();
        }
        resetOptionsModal.Close();
    }

    private void DenyResetOptions() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        resetOptionsModal.Close();
    }
}
