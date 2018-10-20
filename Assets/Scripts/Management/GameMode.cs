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
    CUSTOM,
    CHALLENGE
}

public static class GameMode {
    private static Dictionary<Mode, string> ModeNames = new Dictionary<Mode, string>();
    private static Dictionary<Mode, string> LevelPrefixes = new Dictionary<Mode, string>();
    private static Dictionary<Mode, string> ExternalPrefixes = new Dictionary<Mode, string>();
    private static Dictionary<Mode, string> ExternalListPaths = new Dictionary<Mode, string>();

    static GameMode() {
        // String names for game modes
        ModeNames.Add(Mode.TUTORIAL, "Tutorial");
        ModeNames.Add(Mode.EASY, "Easy");
        ModeNames.Add(Mode.CLASSIC, "Classic");
        ModeNames.Add(Mode.CUSTOM, "Custom");
        ModeNames.Add(Mode.CHALLENGE, "Challenge");

        // Locations of the level files
        LevelPrefixes.Add(Mode.TUTORIAL, "Data/Levels/Tutorial");
        LevelPrefixes.Add(Mode.EASY, "Data/Levels/Easy");
        LevelPrefixes.Add(Mode.CLASSIC, "Data/Levels/Classic");
        LevelPrefixes.Add(Mode.CUSTOM, "Data/Levels/Custom");
        LevelPrefixes.Add(Mode.CHALLENGE, "Data/Levels/Challenge");

        // Locations of the externals files
        ExternalPrefixes.Add(Mode.TUTORIAL, "Data/ExternalObjects/Tutorial");
        ExternalPrefixes.Add(Mode.EASY, "Data/ExternalObjects/Easy");
        ExternalPrefixes.Add(Mode.CLASSIC, "Data/ExternalObjects/Classic");
        ExternalPrefixes.Add(Mode.CUSTOM, "Data/ExternalObjects/Custom");
        ExternalPrefixes.Add(Mode.CHALLENGE, "Data/ExternalObjects/Challenge");

        // Locations of the external lists
        ExternalListPaths.Add(Mode.TUTORIAL, "Data/ExternalObjects/classicExternalsList");
        ExternalListPaths.Add(Mode.EASY, "Data/ExternalObjects/easyExternalsList");
        ExternalListPaths.Add(Mode.CLASSIC, "Data/ExternalObjects/classicExternalsList");
        ExternalListPaths.Add(Mode.CUSTOM, "Data/ExternalObjects/customExternalsList");
        ExternalListPaths.Add(Mode.CHALLENGE, "Data/ExternalObjects/challengeExternalsList");
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

}