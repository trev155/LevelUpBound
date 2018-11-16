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
 */
using System;
using UnityEngine;


public static class Memory {
    /*
     * Save data to PlayerPrefs. 
     */
    public static void SaveData() {
        Debug.Log("Saving Data to Memory");

        PlayerPrefs.SetString("Theme", GameContext.Theme);
        PlayerPrefs.SetString("Scheme", GameContext.ControlScheme.ToString());
        PlayerPrefs.SetFloat("Volume", GameContext.CurrentVolume);
        PlayerPrefs.SetInt("audioToggle", GameContext.AudioEnabled ? 1 : 0);

        PlayerPrefs.Save();
    }

    /*
     * Load data from PlayerPrefs.
     */
    public static void LoadData() {
        Debug.Log("Loading Data from Memory");

        string theme = PlayerPrefs.GetString("Theme");
        if (theme.Length != 0) {
            GameContext.Theme = theme;
        }

        string scheme = PlayerPrefs.GetString("Scheme");
        if (scheme.Equals("Arrow", StringComparison.InvariantCultureIgnoreCase)) {
            GameContext.ControlScheme = ControlMode.ARROW;
        }
        if (scheme.Equals("Click", StringComparison.InvariantCultureIgnoreCase)) {
            GameContext.ControlScheme = ControlMode.CLICK;
        }

        float volume = PlayerPrefs.GetFloat("Volume");
        GameContext.CurrentVolume = volume;

        int audioToggle = PlayerPrefs.GetInt("audioToggle");
        GameContext.AudioEnabled = (audioToggle == 1);
    }
}
