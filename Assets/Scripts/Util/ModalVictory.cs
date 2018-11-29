/*
 * Modal that gets displayed when you reach the end of a game mode.
 */
public class ModalVictory : Modal {
    private static readonly string VICTORY_TEXT = "Congratulations! You have beaten all of the levels for the " + GameMode.GetName(GameContext.GameMode) + " game mode!";

    public void InitializeVictoryModal() {
        SetMainText(VICTORY_TEXT);
    }
}
