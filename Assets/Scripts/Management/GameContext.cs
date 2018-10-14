/*
 * This class holds global data that can be used across scenes.
 */
using UnityEngine;
using System;

public static class GameContext {
    // Global Variables
    public static string GameMode { get; set; }
    public static int CurrentLevel { get; set; }
    public static string PreviousPageContext { get; set; }
    public static bool AudioEnabled { get; set; }
    
    /*
     * Static Constructor = Default Values
     */
    static GameContext() {
        GameMode = "classic";
        CurrentLevel = 7;
        PreviousPageContext = "MainMenu";
        AudioEnabled = true;
    }

    private static readonly string[] validPreviousPages = {"MainMenu", "LevelSelector"};

    /* 
     * Load the page pointed by the PreviousPageContext.
     * If no page exists, load a default page.
     */
    public static void LoadPreviousPage() {
        Debug.Log(PreviousPageContext);
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
