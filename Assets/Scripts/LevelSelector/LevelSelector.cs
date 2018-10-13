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
using UnityEngine.UI;
using System.Linq;


public class LevelSelector : MonoBehaviour {
    // Constants
    private const int MAX_LEVELS_CLASSIC = 100;
    private const int MAX_LEVELS_CUSTOM = 100;
    private const int LEVELS_PER_PAGE = 20;

    // Store state of current selections
    private string currentGameModeSelection;
    private int maxLevels;
    private int currentPage;
    private float maxPages;

    // prefabs
    public Transform levelButtonPrefab;

    // Other objects
    private Util util = new Util();

    private void Awake() {
        // ...
    }

    private void Start() {
        // default settings
        currentGameModeSelection = "classic";
        currentPage = 1;
        maxPages = ComputeMaxPages();
        maxLevels = GetMaxLevelsForGameMode();

        InstantiateLevelButtons();   
    }

    private void InstantiateLevelButtons() {
        int numberOfLevelButtons;
        if (currentPage == maxPages) {
            numberOfLevelButtons = maxLevels % LEVELS_PER_PAGE;
        } else {
            numberOfLevelButtons = LEVELS_PER_PAGE;
        }

        int startingLevel = ((currentPage - 1) * LEVELS_PER_PAGE) + 1;
        int nextLevel = startingLevel;
        Transform[] levelButtonPositions = GameObject.FindGameObjectsWithTag("LevelButtonPosition").Select(gameObject => gameObject.transform).OrderBy(gameObject => gameObject.name).ToArray();
        for (int buttonPositionIndex = 0; buttonPositionIndex < numberOfLevelButtons; buttonPositionIndex++) {
            Transform levelButtonInstance = Instantiate(levelButtonPrefab, levelButtonPositions[buttonPositionIndex]);
            levelButtonInstance.gameObject.name = "Level_" + util.GetLevelString(nextLevel);
            levelButtonInstance.gameObject.GetComponent<Button>().onClick.AddListener(Selection);
            nextLevel += 1;
        }
    }

    private void RemoveLevelButtons() {
        // remove all level buttons before instantiating new ones
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

        RemoveLevelButtons();
        InstantiateLevelButtons();
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
        maxPages = ComputeMaxPages();
        maxLevels = GetMaxLevelsForGameMode();

        // TODO change the UI text as well



    }

    /*
     * Compute max pages for the current game mode.
     */
     private float ComputeMaxPages() {
        float maxPages;
        if (currentGameModeSelection == "classic") {
            maxPages = Mathf.Ceil(MAX_LEVELS_CLASSIC / (float) LEVELS_PER_PAGE);
        } else if (currentGameModeSelection == "custom") {
            maxPages = Mathf.Ceil(MAX_LEVELS_CUSTOM / (float) LEVELS_PER_PAGE);
        } else {
            maxPages = -1;
        }
        Debug.Log("Max Pages: " + maxPages);
        return maxPages;
    }

    /*
     * Get the number of levels for the currently selected game mode.
     */
    private int GetMaxLevelsForGameMode() {
        int maxLevels;
        if (currentGameModeSelection == "classic") {
            maxLevels = MAX_LEVELS_CLASSIC;
        } else if (currentGameModeSelection == "custom") {
            maxLevels = MAX_LEVELS_CUSTOM;
        } else {
            Debug.Log("Invalid game mode selection, returning -1");
            maxLevels = -1;
        }
        return maxLevels;
    }

    public string getGameMode() {
        return currentGameModeSelection;
    }
}
