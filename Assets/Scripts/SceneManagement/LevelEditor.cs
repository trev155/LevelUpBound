/*
 * Handles the Level Editor feature. 
 * Likely won't get to this for a long time though.
 */
using UnityEngine;

public class LevelEditor : MonoBehaviour {
    /*
     * Initialization
     */
    private void Awake() {
        AspectRatioManager.AdjustScreen();
        ThemeManager.SetTheme();
    }

    /*
    * Back button handler
    */
    public void BackButton() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.LoadPreviousPage();
    }
}
