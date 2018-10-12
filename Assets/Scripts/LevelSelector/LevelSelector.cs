/*
 * The Level Selector, in its current form, starts the game at a specified level, with a specified game mode.
 * In the future, I would like this to be a single level selection. That is, after reaching the goal, we 
 * go back to the level selector.
 * 
 * Right now, the level selector is a "page" with many buttons. Each button represents a level. When pressed,
 * we take the user to the specified level. 
 * 
 * The buttons are named like,
 * "Level_XX"
 * where XX is a number (always at least 2 digits, could be 3).
 */

using UnityEngine;
using UnityEngine.EventSystems;

public class LevelSelector : MonoBehaviour {
    // Constants
    private const int MAX_LEVELS_CLASSIC = 100;
    private const int MAX_LEVELS_CUSTOM = 100;
    private const int LEVELS_PER_PAGE = 20;

    // Store state of current selections
    private string currentGameModeSelection = "classic";
    private int currentPage = 1;
    private int maxPages;


    private void Awake() {
        maxPages = ComputeMaxPages();
    }

    /*
     * This function will play a level by switching scenes to the specified level and game mode.
     */
    private void PlayLevel(string mode, int level) {
        GameContext.GameMode = mode;
        GameContext.CurrentLevel = level;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }

    /*
     * Button Handler
     */
    public void Selection() {
        Debug.Log(EventSystem.current.currentSelectedGameObject);

        // get level name
        string levelName = EventSystem.current.currentSelectedGameObject.name.Split('_')[1];
        int selectedLevel = int.Parse(levelName);

        PlayLevel(currentGameModeSelection, selectedLevel);
        
    }


    public void ScrollPageLeft() {
        Debug.Log("Scroll Page Left");
        ScrollPage("left");
    }

    public void ScrollPageRight() {
        Debug.Log("Scroll Page Right");
        ScrollPage("right");
    }

    /*
     * Called when the "left" or "right" button is pressed.
     */
    private void ScrollPage(string direction) {
        if (direction == "left") {
            currentPage -= 1;
        } else if (direction == "right") {
            currentPage += 1;
        }
    }

    /*
     * Whenever we scroll pages, we are going to rename the button names.
     */
    private void RenameButtons() {

    }

    /*
     * Called when the game mode toggle button is pressed.
     */
     public void ToggleGameMode() {
        if (currentGameModeSelection == "classic") {
            currentGameModeSelection = "custom";
        } else if (currentGameModeSelection == "custom") {
            currentGameModeSelection = "classic";
        }
        
        // TODO change the UI text as well

        // TODO when game mode is changed, go back to page 1
     }

    /*
     * Compute max pages for the current game mode.
     */
     private int ComputeMaxPages() {
        Debug.Log(MAX_LEVELS_CLASSIC / LEVELS_PER_PAGE);
        if (currentGameModeSelection == "classic") {
            return MAX_LEVELS_CLASSIC / LEVELS_PER_PAGE;
        } else if (currentGameModeSelection == "custom") {
            return MAX_LEVELS_CUSTOM / LEVELS_PER_PAGE;
        } else {
            return 0;
        }
    }
}
