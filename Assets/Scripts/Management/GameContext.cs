﻿/*
 * This class holds global data that can be used across scenes.
 */
using UnityEngine;
using System;

public static class GameContext {
    // Game data
    public static Mode GameMode { get; set; }
    public static int CurrentLevel { get; set; }

    // Used for the Back button
    public static string PreviousPageContext { get; set; }

    // Audio controls
    public static bool AudioEnabled { get; set; }
    public static float CurrentVolume { get; set; }

    // For the level selection page
    public static bool LevelSelection { get; set; }
    public static int LevelSelectionPage { get; set; }
    public static bool StopMovement { get; set; }

    // Theme Selection
    public static string Theme { get; set; }
    
    /*
     * Static Constructor = Default Values
     */
    static GameContext() {
        GameMode = Mode.EASY;
        CurrentLevel = 1;
        PreviousPageContext = "MainMenu";
        AudioEnabled = true;
        CurrentVolume = 0.6f;
        LevelSelection = false;
        LevelSelectionPage = 1;
        Theme = "Normal";
    }

    private static readonly string[] validPreviousPages = {"MainMenu", "LevelSelector"};

    /* 
     * Load the page pointed by the PreviousPageContext.
     * If no page exists, load a default page.
     */
    public static void LoadPreviousPage() {
        if (PreviousPageContext == null || PreviousPageContext == "") {
            Debug.Log("Previous Page Context not set. Loading Default Page");
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        } else if (Array.IndexOf(validPreviousPages, PreviousPageContext) == -1) {
            Debug.Log("Previous Page Context is invalid. Loading Default Page");
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        } else {
            UnityEngine.SceneManagement.SceneManager.LoadScene(PreviousPageContext);
        }
    }
}
