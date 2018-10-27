/*
 * Everything to do with the Instructions page.
 */
using UnityEngine;

public class Instructions : MonoBehaviour {
    private void Awake() {
        AspectRatioManager.AdjustScreen();
        Theme.SetTheme();
    }

    public void BackButton() {
        GameContext.LoadPreviousPage();
    }
}
