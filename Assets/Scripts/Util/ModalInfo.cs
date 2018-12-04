/*
 * Generic modal that is a single page long, displays a single message.
 */
public class ModalInfo : Modal {
    private static readonly string VICTORY_TEXT = "Congratulations! You have beaten all of the levels for the game mode,";
    private static readonly string LOAD_NO_SAVED_GAMES_TEXT = "Unable to load game. No saved game data found.";
    
    public void InitializeVictoryModal() {
        SetMainText(VICTORY_TEXT + " " + GameMode.GetName(GameContext.GameMode));
    }

    public void InitializeLoadModal() {
        SetMainText(LOAD_NO_SAVED_GAMES_TEXT);
    }
}
