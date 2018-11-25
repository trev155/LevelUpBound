/*
 * Class that handles modal windows.
 * This is a parent class. Every modal will have basic functions and fields defined in this class.
 */
using UnityEngine;
using UnityEngine.UI;

public abstract class Modal : MonoBehaviour {
    public Text modalMainText;

    private void Awake() {
        GameContext.ModalActive = true;
    }

    /*
     * Main function to close the current modal window
     */
    public void Close() {
        gameObject.SetActive(false);
        GameContext.ModalActive = false;
    }

    public void CloseAndGoBack() {
        Close();
        GameContext.LoadPreviousPage();
    }
}