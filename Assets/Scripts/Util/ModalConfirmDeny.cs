﻿/*
 * Modal that gets displayed when you want to present the user with a Yes or No option.
 * Callers should specify OnClick handlers for the Yes/No dynamically, with .onClick.AddListener().
 */
public class ModalConfirmDeny : Modal {
    private static readonly string EXIT_APP_TEXT = "Are you sure you want to exit?";
    private static readonly string RESET_PROGRESS_TEXT = "Are you sure you want to reset your progress?";
    private static readonly string RESET_OPTIONS_TEXT = "Are you sure you want to revert back to the default options?";
    private static readonly string SAVE_OVERWRITE_TEXT = "There is already a saved game. Are you sure you want to overwrite it?";

    public void InitializeExitModal() {
        SetMainText(EXIT_APP_TEXT);
    }

    public void InitializeResetProgressModal() {
        SetMainText(RESET_PROGRESS_TEXT);
    }

    public void InitializeResetOptionsModal() {
        SetMainText(RESET_OPTIONS_TEXT);
    }

    public void InitializePlaySaveOverwriteModal(Mode mode, int level) {
        SetMainText(SAVE_OVERWRITE_TEXT + " (" + GameMode.GetName(mode) + ": " + level.ToString() + ")");
    }
}