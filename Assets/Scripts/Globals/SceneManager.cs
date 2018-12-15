/*
 * Scene names and string mappings.
 */
using UnityEngine;
using System;

public enum SceneName {
    ABOUT,
    INSTRUCTIONS,
    INTRO,
    LEVEL_EDITOR_MENU,
    LEVEL_EDITOR_CREATOR,
    LEVEL_SELECTOR,
    MAIN_GAME,
    MAIN_MENU,
    OPTIONS
}

public static class SceneManager {
    public static string GetSceneNameString(SceneName name) {
        switch (name) {
            case SceneName.ABOUT:
                return "About";
            case SceneName.INSTRUCTIONS:
                return "Instructions";
            case SceneName.INTRO:
                return "Intro";
            case SceneName.LEVEL_EDITOR_MENU:
                return "LevelEditorMenu";
            case SceneName.LEVEL_EDITOR_CREATOR:
                return "LevelEditorCreator";
            case SceneName.LEVEL_SELECTOR:
                return "LevelSelector";
            case SceneName.MAIN_GAME:
                return "MainGame";
            case SceneName.MAIN_MENU:
                return "MainMenu";
            case SceneName.OPTIONS:
                return "Options";
            default:
                throw new System.Exception("Scene Name not found!");
        }
    }

    public static void LoadPreviousContextPage() {
        SceneName[] validPreviousPages = { SceneName.MAIN_MENU, SceneName.LEVEL_SELECTOR, SceneName.LEVEL_EDITOR_MENU };
        if (Array.IndexOf(validPreviousPages, GameContext.PreviousPageContext) == -1) {
            Debug.Log("Previous Page Context is invalid. Loading Default Page");
            UnityEngine.SceneManagement.SceneManager.LoadScene(GetSceneNameString(SceneName.MAIN_MENU));
        } else {
            UnityEngine.SceneManagement.SceneManager.LoadScene(GetSceneNameString(GameContext.PreviousPageContext));
        }
    }
}
