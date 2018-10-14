/*
 * The level selector is a "page" with many buttons. 
 * Each button represents a level. 
 * When pressed, we take the user to the specified level. 
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

    // Reference objects
    public Text gameModeText;

    /*
     * Initialization of propreties
     */
    private void Start() {
        // default settings
        currentGameModeSelection = GameContext.LevelSelectionMode;
        SetGameModeText();
        currentPage = GameContext.LevelSelectionPage;
        BlurArrow();
        maxPages = ComputeMaxPages();
        maxLevels = GetMaxLevelsForGameMode();

        CreateLevelButtons();   
    }

    /*
     * Instantiate level buttons. There are predefined transforms with tag "LevelButtonPosition". 
     */
    private void CreateLevelButtons() {
        Debug.Log("Creating Level Buttons");
        int numberOfLevelButtons;
        if (currentPage == maxPages) {
            numberOfLevelButtons = maxLevels % LEVELS_PER_PAGE;
            if (numberOfLevelButtons == 0) {
                numberOfLevelButtons = LEVELS_PER_PAGE;
            }
        } else {
            numberOfLevelButtons = LEVELS_PER_PAGE;
        }

        int startingLevel = ((currentPage - 1) * LEVELS_PER_PAGE) + 1;
        int nextLevel = startingLevel;
        Transform[] levelButtonPositions = GameObject.FindGameObjectsWithTag("LevelButtonPosition").Select(gameObject => gameObject.transform).OrderBy(gameObject => gameObject.name).ToArray();
        for (int buttonPositionIndex = 0; buttonPositionIndex < numberOfLevelButtons; buttonPositionIndex++) {
            Transform levelButtonInstance = Instantiate(levelButtonPrefab, levelButtonPositions[buttonPositionIndex]);
            levelButtonInstance.gameObject.name = "Level_" + Util.GetLevelString(nextLevel);
            levelButtonInstance.gameObject.GetComponent<Button>().onClick.AddListener(LevelButtonOnClick);
            Text levelButtonText = levelButtonInstance.GetChild(0).GetComponent<Text>();
            levelButtonText.text = nextLevel.ToString();
            nextLevel += 1;
        }
    }

    /*
     * Remove all level buttons from the scene. Level buttons have a tag of "LevelButton".
     * This should be called whenever we scroll to a new page, where we want to get rid of the current level buttons
     * before displaying new ones.
     */
    private void RemoveLevelButtons() {
        Debug.Log("Removing all Level Buttons");
        GameObject[] allLevelButtons = GameObject.FindGameObjectsWithTag("LevelButton");
        foreach (GameObject levelButton in allLevelButtons) {
            Destroy(levelButton);
        }
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
     * If we are on the leftmost or the rightmost page, disable the button.
     */
    private void ScrollPage(string direction) {
        if (direction == "left") {
            if (currentPage != 1) {
                currentPage -= 1;
            }
        } else if (direction == "right") {
            if (currentPage != maxPages) {
                currentPage += 1;
            }
        } else {
            Debug.Log("Unknown scroll direction");
            return;
        }
        Debug.Log("Current Page: " + currentPage);
        if (currentPage == 1 || currentPage == maxPages) {
            BlurArrow();
        } else {
            UnblurArrows();
        }
        RemoveLevelButtons();
        CreateLevelButtons();
    }


    /*
     * This function will play a level by switching scenes to the specified level and game mode.
     */
    private void PlayLevel(string mode, int level) {
        GameContext.GameMode = mode;
        GameContext.CurrentLevel = level;
        GameContext.PreviousPageContext = "LevelSelector";
        GameContext.LevelSelection = true;
        GameContext.LevelSelectionPage = currentPage;
        GameContext.LevelSelectionMode = currentGameModeSelection;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }

    /*
     * Button Handler for when any Level Button on the Canvas is clicked.
     */
    public void LevelButtonOnClick() {
        string levelName = EventSystem.current.currentSelectedGameObject.name.Split('_')[1];
        int selectedLevel = int.Parse(levelName);
        
        PlayLevel(currentGameModeSelection, selectedLevel);
    }

    /*
     * Button Handler for when the game mode toggle is clicked.
     */
     public void ToggleGameMode() {
        if (currentGameModeSelection == "classic") {
            currentGameModeSelection = "custom";
        } else if (currentGameModeSelection == "custom") {
            currentGameModeSelection = "classic";
        }
        SetGameModeText();
        maxPages = ComputeMaxPages();
        maxLevels = GetMaxLevelsForGameMode();
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

    /*
     * Set game mode text according to the current state.
     */ 
    private void SetGameModeText() {
        gameModeText.text = currentGameModeSelection;
    }

    /*
     * Blur out a scroll arrow if we are at the leftmost or rightmost page.
     * Blurring means to reduce the alpha property of the image.
     */ 
    private void BlurArrow() {
        if (currentPage == 1) {
            Image image = GameObject.Find("ScrollLeftButton").GetComponent<Image>();
            Color tempColor = image.color;
            tempColor.a = 0.1f;
            image.color = tempColor;
        }
        if (currentPage == maxPages) {
            Image image = GameObject.Find("ScrollRightButton").GetComponent<Image>();
            Color tempColor = image.color;
            tempColor.a = 0.1f;
            image.color = tempColor;
        }
    }

    /*
     * Unblur both scroll arrows. Set alpha property of image back to 1.
     */
    private void UnblurArrows() {
        Image imageLeft = GameObject.Find("ScrollLeftButton").GetComponent<Image>();
        Color tempColorLeft = imageLeft.color;
        tempColorLeft.a = 1.0f;
        imageLeft.color = tempColorLeft;
        
        Image imageRight = GameObject.Find("ScrollRightButton").GetComponent<Image>();
        Color tempColorRight = imageRight.color;
        tempColorRight.a = 1.0f;
        imageRight.color = tempColorRight;   
    }

    /*
    * Back button handler
    */
    public void BackButton() {
        GameContext.LoadPreviousPage();
    }
}
