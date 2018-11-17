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

public static class Memory {
    public static readonly string SAVE_ENABLED = "SaveEnabled";
    private static readonly string THEME = "Theme";
    private static readonly string SCHEME = "Scheme";
    private static readonly string VOLUME = "Volume";
    private static readonly string AUDIO_TOGGLE = "AudioToggle";
    private static readonly string COMPLETED_LEVELS = "CompletedLevels";

    /*
     * Save data to PlayerPrefs. 
     */
    public static void SaveData() {
        Debug.Log("Saving Data to Memory");

        PlayerPrefs.SetString(SAVE_ENABLED, "true");
        PlayerPrefs.SetString(THEME, GameContext.Theme);
        PlayerPrefs.SetString(SCHEME, GameContext.ControlScheme.ToString());
        PlayerPrefs.SetFloat(VOLUME, GameContext.CurrentVolume);
        PlayerPrefs.SetInt(AUDIO_TOGGLE, GameContext.AudioEnabled ? 1 : 0);

        string completedLevelsJson = JsonConvert.SerializeObject(GameContext.CompletedLevels);
        PlayerPrefs.SetString(COMPLETED_LEVELS, completedLevelsJson);
        Debug.Log(completedLevelsJson);

        PlayerPrefs.Save();
    }

    /*
     * Load data from PlayerPrefs.
     */
    public static void LoadData() {
        Debug.Log("Loading Data from Memory");

        string theme = PlayerPrefs.GetString(THEME);
        if (theme.Length > 0) {
            GameContext.Theme = theme;
        }

        string scheme = PlayerPrefs.GetString(SCHEME);
        if (scheme.Equals("Arrow", StringComparison.InvariantCultureIgnoreCase)) {
            GameContext.ControlScheme = ControlMode.ARROW;
        } else if (scheme.Equals("Click", StringComparison.InvariantCultureIgnoreCase)) {
            GameContext.ControlScheme = ControlMode.CLICK;
        } else {
            GameContext.ControlScheme = ControlMode.ARROW;
        }

        float volume = PlayerPrefs.GetFloat(VOLUME);
        GameContext.CurrentVolume = volume;

        int audioToggle = PlayerPrefs.GetInt(AUDIO_TOGGLE);
        GameContext.AudioEnabled = (audioToggle == 1);

        string completedLevelsJson = PlayerPrefs.GetString(COMPLETED_LEVELS);
        Debug.Log(completedLevelsJson);
        if (completedLevelsJson.Length > 0) {
            GameContext.CompletedLevels = JsonConvert.DeserializeObject<Dictionary<Mode, HashSet<int>>>(completedLevelsJson);
        }
    }

    /*
     * Delete all completed level data.
     */
    public static void ResetProgress() {
        Debug.Log("Deleting all completed level data.");
        PlayerPrefs.DeleteKey(COMPLETED_LEVELS);
    }
}
