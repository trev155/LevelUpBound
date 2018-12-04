/*
 * This class holds global data that can be used across scenes.
 */
using System.Collections.Generic;

public static class GameContext {
    // Game data
    public static Mode GameMode { get; set; }
    public static int CurrentLevel { get; set; }

    // Keep track of what page the back button should refer to
    public static SceneName PreviousPageContext { get; set; }

    // Audio controls
    public static bool AudioEnabled { get; set; }
    public static float CurrentMusicVolume { get; set; }
    public static float CurrentEffectsVolume { get; set; }

    // Main Menu game selection
    public static Mode MainMenuGameMode { get; set; }

    // For the level selection page
    public static bool PlayedFromLevelSelector { get; set; }
    public static int LevelSelectionPageNum { get; set; }

    // Theme Selection
    public static Theme CurrentTheme { get; set; }

    // Player Control Scheme
    public static ControlMode ControlScheme { get; set; }

    // Modal Windows
    public static bool ModalActive { get; set; }

    // Completed levels dictionary. Mapping of game mode -> list of integers representing completed level numbers
    public static Dictionary<Mode, HashSet<int>> CompletedLevels;

    // Keep track of saved games
    public static Mode SavedGameMode { get; set; }
    public static int SavedGameLevel { get; set; }

    /*
     * Static Constructor runs when the game loads. These represent the default globals.
     */
    static GameContext() {
        GameMode = Mode.CLASSIC;
        CurrentLevel = 1;
        PreviousPageContext = SceneName.MAIN_MENU;
        AudioEnabled = true;
        CurrentMusicVolume = 0.6f;
        CurrentEffectsVolume = 0.6f;
        MainMenuGameMode = Mode.EASY;
        PlayedFromLevelSelector = false;
        LevelSelectionPageNum = 1;
        CurrentTheme = Theme.NORMAL;
        ControlScheme = ControlMode.ARROW;
        ModalActive = false;
        InitializeCompletedLevels();
        SavedGameMode = Mode.NONE;
        SavedGameLevel = 0;
        
        // PlayerPrefs.DeleteAll();     // For testing
    }

    public static void InitializeCompletedLevels() {
        CompletedLevels = new Dictionary<Mode, HashSet<int>> {
            { Mode.EASY, new HashSet<int>() },
            { Mode.CLASSIC, new HashSet<int>() },
            { Mode.ADVANCED, new HashSet<int>() },
            { Mode.CHALLENGE, new HashSet<int>() }
        };
    }
}
