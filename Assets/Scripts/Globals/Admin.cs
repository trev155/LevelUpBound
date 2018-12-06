/*
 * The idea of the Admin class, is that it has listeners that you can invoke.
 * This will debug.log certain information that you want into the console.
 */
using UnityEngine;
using Newtonsoft.Json;

public class Admin : MonoBehaviour {
    private void Update() {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyUp(KeyCode.Alpha1)) {
            LogAllGameContext();
        } else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyUp(KeyCode.Alpha2)) {
            LogTempCustom();
        }
    }

    private void LogAllGameContext() {
        Debug.Log("GameMode: " + GameContext.GameMode);
        Debug.Log("Current Level: " + GameContext.CurrentLevel);
        Debug.Log("Previous Page Context: " + GameContext.PreviousPageContext);
        Debug.Log("Audio Enabled: " + GameContext.AudioEnabled);
        Debug.Log("Current Music Volume: " + GameContext.CurrentMusicVolume);
        Debug.Log("Current Effects Volume: " + GameContext.CurrentEffectsVolume);
        Debug.Log("Main Menu Game Mode: " + GameContext.MainMenuGameMode);
        Debug.Log("Played From Level Selector: " + GameContext.PlayedFromLevelSelector);
        Debug.Log("Level Selection Page Number: " + GameContext.LevelSelectionPageNum);
        Debug.Log("Current Theme: " + GameContext.CurrentTheme);
        Debug.Log("Control Scheme: " + GameContext.ControlScheme);
        Debug.Log("Modal Active: " + GameContext.ModalActive);
        Debug.Log("Completed Levels: " + JsonConvert.SerializeObject(GameContext.CompletedLevels));
        Debug.Log("Saved Game Level: " + GameContext.SavedGameLevel);
        Debug.Log("Saved Game Mode: " + GameContext.SavedGameMode);
    }

    private void LogTempCustom() {
        Debug.Log("Completed Levels: " + JsonConvert.SerializeObject(GameContext.CompletedLevels));
    }
}


