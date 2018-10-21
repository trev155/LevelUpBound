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

    /*
     * Initialization
     */
    private void Awake() {
        InitializeAudioUI();
        SetInitialVolume();
        volumeSlider.onValueChanged.AddListener(delegate { VolumeSliderChanged(); });
        Theme.SetTheme();
    }

    /*
     * Back button handler
     */
    public void BackButton() {
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
        GameContext.Theme = "Normal";
        Theme.SetTheme();
    }

    public void SetLightTheme() {
        GameContext.Theme = "Light";
        Theme.SetTheme();
    }

    public void SetDarkTheme() {
        GameContext.Theme = "Dark";
        Theme.SetTheme();
    }

    public void SetVibrantTheme() {
        GameContext.Theme = "Vibrant";
        Theme.SetTheme();
    }

}
