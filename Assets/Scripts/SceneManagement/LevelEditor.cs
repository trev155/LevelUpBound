/*
 * Handles the LevelEditor page.
 */
using UnityEngine;

public class LevelEditor : MonoBehaviour {
    private void Awake() {
        AspectRatioManager.AdjustScreenElements();
        ThemeManager.SetTheme();
    }

    /*
    * Back button handler
    */
    public void BackButton() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        SceneManager.LoadPreviousContextPage();
    }
}