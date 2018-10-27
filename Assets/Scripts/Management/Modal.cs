﻿/*
 * Class that handles modal windows.
 */
using UnityEngine;
using UnityEngine.UI;

public class Modal : MonoBehaviour {
    public Text modalText;

    public void CloseModalWindow() {
        gameObject.SetActive(false);
    }

    public void SetModalText() {
        modalText.text = "Congratulations! You have beaten all of the levels for the " + GameMode.GetName(GameContext.GameMode) + " game mode!";
    }
}