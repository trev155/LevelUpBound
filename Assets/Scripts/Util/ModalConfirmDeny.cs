/*
 * Modal that gets displayed when you want to present the user with a Yes or No option.
 * Callers should specify OnClick handlers for the Yes/No dynamically, with .onClick.AddListener().
 */
public class ModalConfirmDeny : Modal {
    private static readonly string EXIT_APP_TEXT = "Are you sure you want to exit?";
    private static readonly string RESET_PROGRESS_TEXT = "Are you sure you want to reset your progress?";

    public void InitializeExitModal() {
        SetMainText(EXIT_APP_TEXT);
    }

    public void InitializeResetProgressModal() {
        SetMainText(RESET_PROGRESS_TEXT);
    }
}