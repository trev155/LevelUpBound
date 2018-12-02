/*
 * Class that handles modal windows.
 */
using UnityEngine;
using UnityEngine.UI;

public abstract class Modal : MonoBehaviour {
    public Text modalMainText;

    private void Awake() {
        GameContext.ModalActive = true;
    }

    public void SetMainText(string text) {
        modalMainText.text = text;
    }

    public void SetMainTextAlignment(TextAnchor textAnchor) {
        modalMainText.alignment = textAnchor;
    }

    public void SetMainTextFontSize(int size) {
        modalMainText.fontSize = size;
    }

    public void Close() {
        gameObject.SetActive(false);
        GameContext.ModalActive = false;
    }

    public void CloseAndGoBack() {
        Close();
        SceneManager.LoadPreviousContextPage();
    }
}