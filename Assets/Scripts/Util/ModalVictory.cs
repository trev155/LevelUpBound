/*
 * Modal that gets displayed when you reach the end of a game mode.
 */
public class ModalVictory : Modal {
    public void SetModalTextVictory() {
        modalMainText.text = "Congratulations! You have beaten all of the levels for the " + GameMode.GetName(GameContext.GameMode) + " game mode!";
    }
}
