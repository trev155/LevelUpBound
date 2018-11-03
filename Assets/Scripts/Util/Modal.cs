﻿/*
 * Class that handles modal windows.
 */
using UnityEngine;
using UnityEngine.UI;

public class Modal : MonoBehaviour {
    public Text modalText;

    private void Awake() {
        GameContext.ModalActive = true;
    }

    /*
     * Main function to close the current modal window
     */
    public void CloseModalWindow() {
        gameObject.SetActive(false);
        GameContext.ModalActive = false;
    }

    public void CloseModalWindowAndGoBack() {
        CloseModalWindow();
        GameContext.LoadPreviousPage();
    }

    public void SetModalTextVictory() {
        modalText.text = "Congratulations! You have beaten all of the levels for the " + GameMode.GetName(GameContext.GameMode) + " game mode!";
    }
}