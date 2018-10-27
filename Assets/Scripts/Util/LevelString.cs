/*
 * Utility functions for constructing level filepaths / strings.
 */
public static class LevelString {
    /*
    * Filepath: eg) /Assets/Scripts/Levels/008
    */
    public static string GetFilepathString(int level, string prefix) {
        string filepathString = prefix + "/" + GetLevelString(level);
        return filepathString;
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
