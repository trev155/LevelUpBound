﻿/*
 * This class handles strings representing game modes. The convenience of this is so
 * we don't have to write strings to represent game modes, we can just use an Enum.
 * This also helps us organize filepath name mappings.
 */
using UnityEngine;
using System.Collections.Generic;

public enum Mode {
    TUTORIAL,
    EASY,
    CLASSIC,
    ADVANCED,
    CHALLENGE,
    CUSTOM,
    NONE
}

public static class GameMode {
    private static Dictionary<Mode, string> ModeNames = new Dictionary<Mode, string>();
    private static Dictionary<Mode, string> LevelPrefixes = new Dictionary<Mode, string>();
    private static Dictionary<Mode, string> ExternalPrefixes = new Dictionary<Mode, string>();
    private static Dictionary<Mode, string> ExternalListPaths = new Dictionary<Mode, string>();
    private static Dictionary<Mode, int> LevelCounts = new Dictionary<Mode, int>();
    private static Dictionary<Mode, string> Descriptions = new Dictionary<Mode, string>();

    static GameMode() {
        // String names for game modes
        ModeNames.Add(Mode.TUTORIAL, "Tutorial");
        ModeNames.Add(Mode.EASY, "Easy");
        ModeNames.Add(Mode.CLASSIC, "Classic");
        ModeNames.Add(Mode.ADVANCED, "Advanced");
        ModeNames.Add(Mode.CHALLENGE, "Challenge");
        ModeNames.Add(Mode.CUSTOM, "Custom");

        // Locations of the level files
        LevelPrefixes.Add(Mode.TUTORIAL, "Data/Levels/Tutorial");
        LevelPrefixes.Add(Mode.EASY, "Data/Levels/Easy");
        LevelPrefixes.Add(Mode.CLASSIC, "Data/Levels/Classic");
        LevelPrefixes.Add(Mode.ADVANCED, "Data/Levels/Advanced");
        LevelPrefixes.Add(Mode.CHALLENGE, "Data/Levels/Challenge");

        // Locations of the externals files
        ExternalPrefixes.Add(Mode.TUTORIAL, "Data/ExternalObjects/Tutorial");
        ExternalPrefixes.Add(Mode.EASY, "Data/ExternalObjects/Easy");
        ExternalPrefixes.Add(Mode.CLASSIC, "Data/ExternalObjects/Classic");
        ExternalPrefixes.Add(Mode.ADVANCED, "Data/ExternalObjects/Advanced");
        ExternalPrefixes.Add(Mode.CHALLENGE, "Data/ExternalObjects/Challenge");

        // Locations of the external lists
        ExternalListPaths.Add(Mode.TUTORIAL, "Data/ExternalObjects/tutorialExternalsList");
        ExternalListPaths.Add(Mode.EASY, "Data/ExternalObjects/easyExternalsList");
        ExternalListPaths.Add(Mode.CLASSIC, "Data/ExternalObjects/classicExternalsList");
        ExternalListPaths.Add(Mode.ADVANCED, "Data/ExternalObjects/advancedExternalsList");
        ExternalListPaths.Add(Mode.CHALLENGE, "Data/ExternalObjects/challengeExternalsList");

        // Number of levels per game mode
        LevelCounts.Add(Mode.TUTORIAL, 5);
        LevelCounts.Add(Mode.EASY, 50);
        LevelCounts.Add(Mode.CLASSIC, 100);
        LevelCounts.Add(Mode.ADVANCED, 100);
        LevelCounts.Add(Mode.CHALLENGE, 10);

        // Game mode descriptions
        Descriptions.Add(Mode.TUTORIAL, "If you're new, start here!");
        Descriptions.Add(Mode.EASY, "These levels will allow you to get used to the game and test your basic bounding skills");
        Descriptions.Add(Mode.CLASSIC, "All 100 levels from the original Level Up Bound");
        Descriptions.Add(Mode.ADVANCED, "A set of more difficult levels that will test your skills");
        Descriptions.Add(Mode.CHALLENGE, "A set of very difficult levels. If you can beat these, you are a pro!");
    }

    public static string GetName(Mode mode) {
        try {
            return ModeNames[mode];
        } catch (KeyNotFoundException e) {
            Debug.Log(e);
            return null;
        }
    }

    public static string GetLevelPrefix(Mode mode) {
        try {
            return LevelPrefixes[mode];
        } catch (KeyNotFoundException e) {
            Debug.Log(e);
            return null;
        }
    }

    public static string GetExternalPrefix(Mode mode) {
        try {
            return ExternalPrefixes[mode];
        } catch (KeyNotFoundException e) {
            Debug.Log(e);
            return null;
        }
    }

    public static string GetExternalListPath(Mode mode) {
        try {
            return ExternalListPaths[mode];
        } catch (KeyNotFoundException e) {
            Debug.Log(e);
            return null;
        }
    }

    public static int GetNumberOfLevels(Mode mode) {
        try {
            return LevelCounts[mode];
        } catch (KeyNotFoundException e) {
            Debug.Log(e);
            return -1;
        }
    }

    public static string GetModeDescription(Mode mode) {
        try {
            return Descriptions[mode];
        } catch (KeyNotFoundException e) {
            Debug.Log(e);
            return null;
        }
    }
}