/*
 * The LevelSelector page allows the user to select a specific level they want to play.
 */
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class LevelSelector : MonoBehaviour {
    private const int LEVELS_PER_PAGE = 20;

    private int currentPage;
    private float maxPages;

    public Transform levelButtonPrefab;
    public Transform levelCompleteButtonPrefab;

    public Text gameModeText;

    private void Awake() {
        AspectRatioManager.AdjustScreenElements();
        ThemeManager.SetTheme();
    }

    // this needs to run after earlier Awake() calls
    private void Start() {
        if (!GameContext.PlayedFromLevelSelector) {
            GameContext.GameMode = Mode.EASY;
        }
        currentPage = GameContext.LevelSelectionPageNum;
        maxPages = ComputeMaxPagesForCurrentGameMode();
        BlurArrows();
        SetGameModeText();
        CreateLevelButtons();
    }

    private void CreateLevelButtons() {
        int numberOfLevelButtons;
        if (currentPage == maxPages) {
            numberOfLevelButtons = GameMode.GetNumberOfLevels(GameContext.GameMode) % LEVELS_PER_PAGE;
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
            Transform levelButtonInstance;
            if (GameContext.CompletedLevels[GameContext.GameMode].Contains(nextLevel)) {
                levelButtonInstance = Instantiate(levelCompleteButtonPrefab, levelButtonPositions[buttonPositionIndex]);
            } else {
                levelButtonInstance = Instantiate(levelButtonPrefab, levelButtonPositions[buttonPositionIndex]);
            }
            
            levelButtonInstance.gameObject.name = "Level_" + LevelStringConstructor.GetLevelString(nextLevel);
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
        GameObject[] allLevelButtons = GameObject.FindGameObjectsWithTag("LevelButton");
        foreach (GameObject levelButton in allLevelButtons) {
            Destroy(levelButton);
        }
    }

    public void ScrollPageLeft() {
        ScrollPage("left");
    }

    public void ScrollPageRight() {
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
            } else {
                return;
            }
        } else if (direction == "right") {
            if (currentPage != maxPages) {
                currentPage += 1;
            } else {
                return;
            }
        } else {
            Debug.Log("Unknown scroll direction");
            return;
        }
        
        if (currentPage == 1 || currentPage == maxPages) {
            BlurArrows();
        } else {
            UnblurArrows();
        }
        RemoveLevelButtons();
        CreateLevelButtons();
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
    }

    private void PlayLevel(Mode mode, int level) {
        GameContext.GameMode = mode;
        GameContext.CurrentLevel = level;
        GameContext.PreviousPageContext = SceneName.LEVEL_SELECTOR;
        GameContext.PlayedFromLevelSelector = true;
        GameContext.LevelSelectionPageNum = currentPage;

        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetSceneNameString(SceneName.MAIN_GAME));
    }

    /*
     * Button Handler for when any Level Button on the Canvas is clicked.
     */
    public void LevelButtonOnClick() {
        string levelName = EventSystem.current.currentSelectedGameObject.name.Split('_')[1];
        int selectedLevel = int.Parse(levelName);
        
        PlayLevel(GameContext.GameMode, selectedLevel);
    }

    public void ToggleGameMode() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);

        if (GameContext.GameMode == Mode.EASY) {
            GameContext.GameMode = Mode.CLASSIC;
        } else if (GameContext.GameMode == Mode.CLASSIC) {
            GameContext.GameMode = Mode.ADVANCED;
        } else if (GameContext.GameMode == Mode.ADVANCED) {
            GameContext.GameMode = Mode.CHALLENGE;
        } else if (GameContext.GameMode == Mode.CHALLENGE) {
            GameContext.GameMode = Mode.EASY;
        }

        // Refresh
        SetGameModeText();
        maxPages = ComputeMaxPagesForCurrentGameMode();
        currentPage = 1;

        RemoveLevelButtons();
        CreateLevelButtons();
        UnblurArrows();
        BlurArrows();
    }

     private float ComputeMaxPagesForCurrentGameMode() {
        int numLevels = GameMode.GetNumberOfLevels(GameContext.GameMode);
        float maxPages = Mathf.Ceil(numLevels / (float)LEVELS_PER_PAGE);
        return maxPages;
    }

    private void SetGameModeText() {
        gameModeText.text = GameMode.GetName(GameContext.GameMode);
    }

    private void BlurArrows() {
        if (currentPage == 1) {
            Image leftButton = GameObject.Find("ScrollLeftButton").GetComponent<Image>();
            Utils.GrayoutImage(leftButton);
        }
        if (currentPage == maxPages) {
            Image rightButton = GameObject.Find("ScrollRightButton").GetComponent<Image>();
            Utils.GrayoutImage(rightButton);
        }
    }

    private void UnblurArrows() {
        Image leftButton = GameObject.Find("ScrollLeftButton").GetComponent<Image>();
        Utils.UndoGrayoutImage(leftButton);

        Image rightButton = GameObject.Find("ScrollRightButton").GetComponent<Image>();
        Utils.UndoGrayoutImage(rightButton);
    }

    /*
    * Back button handler
    */
    public void BackButton() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        SceneManager.LoadPreviousContextPage();
    }
}
