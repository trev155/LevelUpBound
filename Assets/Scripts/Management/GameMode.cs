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
    CUSTOM,
    CHALLENGE
}

public static class GameMode {
    private const string LEVEL_CLASSIC_PREFIX = "Data/Levels/Classic";
    private const string LEVEL_CUSTOM_PREFIX = "Data/Levels/Custom";
    private const string EXTERNALS_CLASSIC_PREFIX = "Data/ExternalObjects/Classic";
    private const string EXTERNALS_CUSTOM_PREFIX = "Data/ExternalObjects/Custom";
    private const string LEVELS_WITH_EXTERNALS_CLASSIC = "Data/ExternalObjects/classicExternalsList";
    private const string LEVELS_WITH_EXTERNALS_CUSTOM = "Data/ExternalObjects/customExternalsList";
    private const string LEVEL_OBJECT_TYPE = "level";
    private const string EXTERNAL_OBJECT_TYPE = "external";

    private static Dictionary<Mode, string> ModeNames = new Dictionary<Mode, string>();

    static GameMode() {
        // String names for game modes
        ModeNames.Add(Mode.TUTORIAL, "Tutorial");
        ModeNames.Add(Mode.EASY, "Easy");
        ModeNames.Add(Mode.CLASSIC, "Classic");
        ModeNames.Add(Mode.CUSTOM, "Custom");
        ModeNames.Add(Mode.CHALLENGE, "Challenge");

        // Locations of the level files

        // Locations of the externals files

        // Locations of the external lists

    }

    public static string GetName(Mode mode) {
        try {
            return ModeNames[mode];
        } catch (KeyNotFoundException e) {
            Debug.Log(e);
            return null;
        }
    }

    public static string GetExternalsFilepath(Mode mode) {

    }

}
