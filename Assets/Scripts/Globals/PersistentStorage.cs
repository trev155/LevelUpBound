/*
 * This class handles persistent storage.
 * Typically, we'll want to save global variables, like the ones from GameContext.cs, into persistent storage.
 * But we don't need to save all of them. For example, we don't need to save the current game mode.
 * We want to save the following:
 * - Theme selection
 * - Control Scheme
 * - Volume level
 * - Audio toggle
 * - Completed levels
 * 
 * Another consideration is that we don't want to load data if no PlayerPrefs exist in the first place.
 * For example, the first time the game loads, PlayerPrefs is not defined. Keep another KV pair to keep
 * track of this, "SaveEnabled".
 */
using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public static class PersistentStorage {
    public static readonly string SAVE_ENABLED = "SaveEnabled";
    private static readonly string THEME = "Theme";
    private static readonly string SCHEME = "Scheme";
    private static readonly string AUDIO_TOGGLE = "AudioToggle";
    private static readonly string BGM_VOLUME = "BGMVolume";
    private static readonly string EFFECTS_VOLUME = "EffectsVolume";
    private static readonly string COMPLETED_LEVELS = "CompletedLevels";
    private static readonly string SAVED_GAME_MODE = "SavedGameMode";
    private static readonly string SAVED_GAME_LEVEL = "SavedGameLevel";

    /*
     * Save data to PlayerPrefs. 
     */
    public static void SaveData() {
        Debug.Log("Saving Data to Memory");

        PlayerPrefs.SetString(SAVE_ENABLED, "true");
        PlayerPrefs.SetString(THEME, GameContext.CurrentTheme.ToString());
        PlayerPrefs.SetString(SCHEME, GameContext.ControlScheme.ToString());
        PlayerPrefs.SetFloat(BGM_VOLUME, GameContext.CurrentMusicVolume);
        PlayerPrefs.SetFloat(EFFECTS_VOLUME, GameContext.CurrentEffectsVolume);
        PlayerPrefs.SetInt(AUDIO_TOGGLE, GameContext.AudioEnabled ? 1 : 0);

        string completedLevelsJson = JsonConvert.SerializeObject(GameContext.CompletedLevels);
        PlayerPrefs.SetString(COMPLETED_LEVELS, completedLevelsJson);
        // Debug.Log("Completed Levels Data:");
        // Debug.Log(completedLevelsJson);

        PlayerPrefs.SetString(SAVED_GAME_MODE, GameContext.SavedGameMode.ToString());
        PlayerPrefs.SetInt(SAVED_GAME_LEVEL, GameContext.SavedGameLevel);

        PlayerPrefs.Save();
    }

    /*
     * Load data from PlayerPrefs.
     */
    public static void LoadData() {
        Debug.Log("Loading Data from Memory...");

        if (PlayerPrefs.GetString(SAVE_ENABLED).Length == 0) {
            return;
        }

        string theme = PlayerPrefs.GetString(THEME);
        if (theme.Length > 0) {
            if (theme.Equals("Normal", StringComparison.InvariantCultureIgnoreCase)) {
                GameContext.CurrentTheme = Theme.NORMAL;
            } else if (theme.Equals("Light", StringComparison.InvariantCultureIgnoreCase)) {
                GameContext.CurrentTheme = Theme.LIGHT;
            } else if (theme.Equals("Dark", StringComparison.InvariantCultureIgnoreCase)) {
                GameContext.CurrentTheme = Theme.DARK;
            } else if (theme.Equals("Vibrant", StringComparison.InvariantCultureIgnoreCase)) {
                GameContext.CurrentTheme = Theme.VIBRANT;
            }
        }

        string scheme = PlayerPrefs.GetString(SCHEME);
        if (scheme.Equals("Arrow", StringComparison.InvariantCultureIgnoreCase)) {
            GameContext.ControlScheme = ControlMode.ARROW;
        } else if (scheme.Equals("Click", StringComparison.InvariantCultureIgnoreCase)) {
            GameContext.ControlScheme = ControlMode.CLICK;
        } else {
            GameContext.ControlScheme = ControlMode.ARROW;
        }

        int audioToggle = PlayerPrefs.GetInt(AUDIO_TOGGLE);
        GameContext.AudioEnabled = (audioToggle == 1);

        float bgmVolume = PlayerPrefs.GetFloat(BGM_VOLUME);
        GameContext.CurrentMusicVolume = bgmVolume;
        AudioManager.Instance.AdjustBackgroundMusicVolume();

        float effectsVolume = PlayerPrefs.GetFloat(EFFECTS_VOLUME);
        GameContext.CurrentEffectsVolume = effectsVolume;
        AudioManager.Instance.AdjustEffectsVolume();

        string completedLevelsJson = PlayerPrefs.GetString(COMPLETED_LEVELS);
        // Debug.Log("Completed Levels Data:");
        // Debug.Log(completedLevelsJson);
        if (completedLevelsJson.Length > 0) {
            GameContext.CompletedLevels = JsonConvert.DeserializeObject<Dictionary<Mode, HashSet<int>>>(completedLevelsJson);
        }

        string savedGameMode = PlayerPrefs.GetString(SAVED_GAME_MODE);
        if (savedGameMode.Length > 0) {
            if (savedGameMode.Equals("Easy", StringComparison.InvariantCultureIgnoreCase)) {
                GameContext.SavedGameMode = Mode.EASY;
            } else if (savedGameMode.Equals("Classic", StringComparison.InvariantCultureIgnoreCase)) {
                GameContext.SavedGameMode = Mode.CLASSIC;
            } else if (savedGameMode.Equals("Advanced", StringComparison.InvariantCultureIgnoreCase)) {
                GameContext.SavedGameMode = Mode.ADVANCED;
            } else if (savedGameMode.Equals("Challenge", StringComparison.InvariantCultureIgnoreCase)) {
                GameContext.SavedGameMode = Mode.CHALLENGE;
            }
        }
        int savedGameLevel = PlayerPrefs.GetInt(SAVED_GAME_LEVEL);
        GameContext.SavedGameLevel = savedGameLevel;
    }

    /*
     * Delete all completed level data.
     */
    public static void ResetProgress() {
        Debug.Log("Resetting Progress - Completed Levels, Saved Game Data");
        PlayerPrefs.DeleteKey(COMPLETED_LEVELS);

        PlayerPrefs.DeleteKey(SAVED_GAME_MODE);
        PlayerPrefs.DeleteKey(SAVED_GAME_LEVEL);
        SavedGameManager.ClearSavedGameData();
    }

    //-------------------
    // Custom Level Data
    //-------------------
    public static bool IsLevelSlotOccupied(int levelSlot) {
        return PlayerPrefs.GetString(levelSlot + "_CUSTOM").Length > 0;
    }

    public static void SaveCustomLevel(int levelSlot, string levelString, string externalsString) {
        PlayerPrefs.SetString(levelSlot + "_CUSTOM", levelString);
        PlayerPrefs.SetString(levelSlot + "_EXTERNALS", externalsString);
    }

    public static void DeleteCustomLevel(int levelSlot) {
        PlayerPrefs.DeleteKey(levelSlot + "_CUSTOM");
        PlayerPrefs.DeleteKey(levelSlot + "_EXTERNALS");
    }

    public static string LoadCustomLevelExplosions(int levelSlot) {
        string levelData = PlayerPrefs.GetString(levelSlot + "_CUSTOM");
        string formattedData = FormatCustomLevelData(levelData);
        return formattedData;
    }
    
    /*
     * Expected input:
     * T0,0 AR
     * T1,2 AR
     * T4,3 AR
     * W500
     * T1,2 AR
     * T1,4 AR
     * W500
     * etc.
     *
     * Expected output:
     * 0,0
     * 1,2
     * 4,3
     * W500
     * 1,2
     * 1,4
     * W500
     * etc.
     */
    private static string FormatCustomLevelData(string data) {
        string[] lines = data.Split('\n');
        string formattedData = "";
        foreach (string line in lines) {
            if (line.Contains("AR")) {
                formattedData += line.Substring(1, 3);
            } else {
                formattedData += line;
            }
            formattedData += "\n";
        }
        return formattedData;
    }

    public static string LoadCustomLevelExternals(int levelSlot) {
        return PlayerPrefs.GetString(levelSlot + "_EXTERNALS");
    }
}
