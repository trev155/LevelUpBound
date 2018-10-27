/*
 * Everything to do with the "About" page.
 */
using UnityEngine;

public class About : MonoBehaviour {
    private void Awake() {
        AspectRatioManager.AdjustScreen();
    }

    public void BackButton() {
        GameContext.LoadPreviousPage();
    }
}
