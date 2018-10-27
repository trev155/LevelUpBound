/*
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
    CHALLENGE
}

public static class GameMode {
    private static Dictionary<Mode, string> ModeNames = new Dictionary<Mode, string>();
    private static Dictionary<Mode, string> LevelPrefixes = new Dictionary<Mode, string>();
    private static Dictionary<Mode, string> ExternalPrefixes = new Dictionary<Mode, string>();
    private static Dictionary<Mode, string> ExternalListPaths = new Dictionary<Mode, string>();
    private static Dictionary<Mode, int> LevelCounts = new Dictionary<Mode, int>();

    static GameMode() {
        // String names for game modes
        ModeNames.Add(Mode.TUTORIAL, "Tutorial");
        ModeNames.Add(Mode.EASY, "Easy");
        ModeNames.Add(Mode.CLASSIC, "Classic");
        ModeNames.Add(Mode.ADVANCED, "Advanced");
        ModeNames.Add(Mode.CHALLENGE, "Challenge");

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
}