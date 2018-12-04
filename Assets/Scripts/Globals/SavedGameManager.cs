/*
 * Class that handles saved games.
 */
using UnityEngine;

public static class SavedGameManager {
    public static bool SavedGameExists() {
        return GameContext.SavedGameMode != Mode.NONE && GameContext.SavedGameLevel > 0;
    }

    public static void ClearSavedGameData() {
        GameContext.SavedGameMode = Mode.NONE;
        GameContext.SavedGameLevel = -1;
        Debug.Log(GameContext.SavedGameLevel);
    }

    public static void UpdateSavedGameData() {
        GameContext.SavedGameMode = GameContext.GameMode;
        GameContext.SavedGameLevel = GameContext.CurrentLevel;
    }
}
