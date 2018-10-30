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

    /*
     * Initialization
     */
    private void Awake() {
        AspectRatioManager.AdjustScreen();
        InitializeAudioUI();
        SetInitialVolume();
        volumeSlider.onValueChanged.AddListener(delegate { VolumeSliderChanged(); });
        Theme.SetTheme();
        SetControlSchemeImage();
    }

    /*
     * Back button handler
     */
    public void BackButton() {
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
        if (GameContext.AudioEnabled) {
            audioText.text = "OFF";
            audioEnabledImage.gameObject.SetActive(false);
            audioDisabledImage.gameObject.SetActive(true);
        } else {
            audioText.text = "ON";
            audioEnabledImage.gameObject.SetActive(true);
            audioDisabledImage.gameObject.SetActive(false);
        }
        GameContext.AudioEnabled = !GameContext.AudioEnabled;
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
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
        GameContext.CurrentVolume = volumeSlider.value;
    }

    // Theme Button handlers
    public void SetNormalTheme() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.Theme = "Normal";
        Theme.SetTheme();
    }

    public void SetLightTheme() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.Theme = "Light";
        Theme.SetTheme();
    }

    public void SetDarkTheme() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.Theme = "Dark";
        Theme.SetTheme();
    }

    public void SetVibrantTheme() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.Theme = "Vibrant";
        Theme.SetTheme();
    }

    // Control Scheme Buttons
    public void SetArrowControls() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.ControlScheme = ControlMode.ARROW;
        SetControlSchemeImage();
        Debug.Log(GameContext.ControlScheme);
    }

    public void SetClickControls() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.ControlScheme = ControlMode.CLICK;
        SetControlSchemeImage();
        Debug.Log(GameContext.ControlScheme);
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
}
