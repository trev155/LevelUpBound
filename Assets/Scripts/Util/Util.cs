﻿using UnityEngine;

public static class Util {
    /*
     * Filepath: eg) /Assets/Scripts/Levels/LS008
     */
    public static string GetFilepathString(int level, string gameMode, string prefix, string objectType) {
        string filepathString = prefix + "/" + GetFilenameString(level, gameMode, objectType);
        return filepathString;
    }

    /*
     * Filename: eg) LS037, LC009, OS010, OC023
     */
    public static string GetFilenameString(int level, string gameMode, string objectType) {
        string filePrefix = "";
        if (objectType.Equals("level")) {
            filePrefix += "L";
        } else if (objectType.Equals("external")) {
            filePrefix += "O";
        } else {
            Debug.Log("Unknown game mode. This should not happen.");
        }

        if (gameMode.Equals("classic")) {
            filePrefix += "S";
        } else if (gameMode.Equals("custom")) {
            filePrefix += "C";
        } else {
            Debug.Log("Unknown game mode. This should not happen.");
        }
        string filenameString = filePrefix + GetLevelString(level);
        return filenameString;
    }

    /*
     * Level strings: eg) 003, 012, 132
     */
    public static string GetLevelString(int level) {
        string levelString = "";
        if (level < 100) {
            levelString += "0";
            if (level < 10) {
                levelString += "0";
            }
        }
        levelString += level;
        return levelString;
    }
}
