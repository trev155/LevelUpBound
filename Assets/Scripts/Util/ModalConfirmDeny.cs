/*
 * Modal that gets displayed when you want to present the user with a Yes or No option.
 * Callers should specify OnClick handlers for the Yes/No dynamically, with .onClick.AddListener().
 */
public class ModalConfirmDeny : Modal {
    public void SetModalTextCloseApp() {
        modalMainText.text = "Are you sure you want to exit?";
    }
}