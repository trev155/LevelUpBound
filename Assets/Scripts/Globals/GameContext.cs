﻿/*
 * This class holds global data that can be used across scenes.
 */
using UnityEngine;
using System;
using System.Collections.Generic;

public static class GameContext {
    // Game data
    public static Mode GameMode { get; set; }
    public static int CurrentLevel { get; set; }

    // Used for the Back button
    public static string PreviousPageContext { get; set; }

    // Audio controls
    public static bool AudioEnabled { get; set; }
    public static float CurrentVolume { get; set; }

    // Main Menu game selection
    public static Mode MainMenuGameMode { get; set; }

    // For the level selection page
    public static bool LevelSelection { get; set; }
    public static int LevelSelectionPage { get; set; }
    public static bool StopMovement { get; set; }

    // Theme Selection
    public static string Theme { get; set; }

    // Player Control Scheme
    public static ControlMode ControlScheme { get; set; }

    // Modal Windows
    public static bool ModalActive { get; set; }

    // Completed levels dictionary. Mapping of game mode -> list of integers representing completed level numbers
    private static Dictionary<Mode, List<int>> CompletedLevels = new Dictionary<Mode, List<int>>();

    /*
     * Static Constructor runs when the game loads.
     */
    static GameContext() {
        // Set default globals. Some of these can be overriden by Memory class.
        GameMode = Mode.CLASSIC;
        CurrentLevel = 1;
        PreviousPageContext = "MainMenu";
        AudioEnabled = true;
        CurrentVolume = 0.6f;
        MainMenuGameMode = Mode.EASY;
        LevelSelection = false;
        LevelSelectionPage = 1;
        Theme = "Normal";
        ControlScheme = ControlMode.ARROW;
        ModalActive = false;

        Memory.LoadData();
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
