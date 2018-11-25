/*
 * A page where the user can set various options, such as volume and theme.
 */
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {
    // Reference to objects
    public Text audioText;
    public Image audioEnabledImage;
    public Image audioDisabledImage;
    public Slider volumeSlider;
    public Image controlSchemeArrowImage;
    public Image controlSchemeClickImage;

    // Modal for progress reset
    public Transform modalContainer;
    public ModalConfirmDeny modalConfirmDeny;
    private ModalConfirmDeny resetProgressModal;

    /*
     * Initialization
     */
    private void Awake() {
        AspectRatioManager.AdjustScreen();
        InitializeAudioUI();
        SetInitialVolume();
        volumeSlider.onValueChanged.AddListener(delegate { VolumeSliderChanged(); });
        ThemeManager.SetTheme();
        SetControlSchemeImage();
    }

    /*
     * Back button handler
     */
    public void BackButton() {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.LoadPreviousPage();
    }

    /*
     * When the scene is initialized, decide whether to show the AudioEnabled or the AudioDisabled image.
     */
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

    /*
     * Toggle audio on / off.
     */
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

        Memory.SaveData();
    }

    /*
     * Set the initial value of the volume slider based on the game context.
     */
    private void SetInitialVolume() {
        volumeSlider.value = GameContext.CurrentVolume;
    }

    /*
     * Function should be called whenever the volume slider changes.
     * Change the volume global variable to the slider value.
     * Since audio is played on different scenes, we have to set volume levels
     * of all audio sources on each scene during scene loads. That means
     * there is minimal work to do here.
     * Also, the only place where volume can be set right now is on this page.
     */
    public void VolumeSliderChanged() {
        if (GameContext.ModalActive) {
            return;
        }

        GameContext.CurrentVolume = volumeSlider.value;
        AudioManager.Instance.AdjustVolumeLevels();
        Memory.SaveData();
    }

    // Theme Button handlers
    private void SetTheme(Theme theme) {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.GameTheme = theme;
        ThemeManager.SetTheme();
        Memory.SaveData();
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

    // Control Scheme Buttons
    public void SetArrowControls() {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.ControlScheme = ControlMode.ARROW;
        SetControlSchemeImage();
        Memory.SaveData();

    }

    public void SetClickControls() {
        if (GameContext.ModalActive) {
            return;
        }

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.ControlScheme = ControlMode.CLICK;
        SetControlSchemeImage();
        Memory.SaveData();
    }

    /*
     * Set the Image sprite for the control scheme.
     */
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

    /*
     * Reset Data Button
     */
    public void ResetProgress() {
        if (GameContext.ModalActive) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        resetProgressModal = Instantiate(modalConfirmDeny, modalContainer);
        resetProgressModal.SetModalTextResetProgress();

        // Set handlers for the Yes / No buttons
        Button yesButton = GameObject.Find("YesButton").GetComponent<Button>();
        yesButton.onClick.AddListener(AllowResetProgress);
        Button noButton = GameObject.Find("NoButton").GetComponent<Button>();
        noButton.onClick.AddListener(DenyResetProgress);
    }

    private void AllowResetProgress() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        Memory.ResetProgress();
        GameContext.InitializeLevelsCompleted();
        resetProgressModal.Close();
    }

    private void DenyResetProgress() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        resetProgressModal.Close();
    }
}
