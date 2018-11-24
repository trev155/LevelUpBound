/*
 * This class handles supporting different resolution sizes.
 * The function AdjustScreen() should be called on every page load.
 * 
 * The way this works, is we detect what the aspect ratio is using Camera.main.aspect.
 * Then, we adjust the size of canvas elements accordingly so everything fits on the page.
 */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class AspectRatioManager {
    /*
     * Adjust the screen elements based on the aspect ratio / resolution of the device that the user has.
     */
    public static void AdjustScreen() {
        float aspectRatio = Camera.main.aspect;
        Debug.Log("Aspect Ratio: " + aspectRatio);
        Scene scene = SceneManager.GetActiveScene();

        // 4:3 portrait
        if (EqualsApprox(aspectRatio, 0.75f, 0.05f)) {
            if (scene.name == "MainMenu") {
                HandleMainMenu(0.75f);
            } else if (scene.name == "MainGame") {
                HandleMainGame(0.75f);
            } else if (scene.name == "LevelSelector") {
                HandleLevelSelector(0.75f);
            } else if (scene.name == "Options") {
                HandleOptions(0.75f);
            } else if (scene.name == "About") {
                HandleAbout(0.75f);
            } else if (scene.name == "Instructions") {
                HandleInstructions(0.75f);
            } else if (scene.name == "LevelEditor") {
                HandleLevelEditor(0.75f);
            }
        }
    }

    /*
     * Set the rect transform size of the canvas element with name gameObject.
     */
    private static void ResizeCanvasElement(string gameObject, float width, float height) {
        RectTransform element = GameObject.Find(gameObject).GetComponent<RectTransform>();
        element.sizeDelta = new Vector2(width, height);
    }

    /*
     * Set the rect transform anchored position of the canvas element with name gameObject.
     */
    private static void SetAnchoredPosition(string gameObject, float x, float y) {
        RectTransform element = GameObject.Find(gameObject).GetComponent<RectTransform>();
        element.anchoredPosition = new Vector2(x, y);
    }

    /*
     * Set the font size of the canvas Text element with name gameObject.
     */
    private static void SetFontSize(string gameObject, int size) {
        Text element = GameObject.Find(gameObject).GetComponent<Text>();
        element.fontSize = size;
    }

    /*
     * Return true if the absolute value between x and y is less than the tolerance.
     * This is needed because resolutions aren't always exact, for example 4:3 may not be exactly 0.75, maybe 0.7498.
     */
    private static bool EqualsApprox(float x, float y, float tolerance) {
        return Mathf.Abs(x - y) < tolerance;
    }

    /*
     * Main Menu resolution adjustments.
     */
    private static void HandleMainMenu(float aspectRatio) {
        if (aspectRatio == 0.75) {
            ResizeCanvasElement("Logo", 600, 300);
            SetAnchoredPosition("Logo", 300, -150);
            ResizeCanvasElement("TitleText", 500, 175);
            SetAnchoredPosition("TitleText", 225, -150);
            ResizeCanvasElement("ScrollLeftButton", 125, 125);
            SetAnchoredPosition("ScrollLeftButton", -400, -400);
            ResizeCanvasElement("ScrollRightButton", 125, 125);
            SetAnchoredPosition("ScrollRightButton", 400, -400);
            SetAnchoredPosition("SelectedModeText", 0, -400);
            ResizeCanvasElement("PlayButton", 400, 150);
            SetAnchoredPosition("PlayButton", 0, -550);
            SetAnchoredPosition("GameModeSelectedDescription", 0, -800);
            SetFontSize("GameModeSelectedDescription", 40);
            SetAnchoredPosition("LevelSelector", -250, -900);
            SetAnchoredPosition("LevelEditor", 250, -900);
            SetAnchoredPosition("TutorialText", 0, -1025);
            SetAnchoredPosition("Instructions", 0, -1150);
            SetAnchoredPosition("Options", -250, -1300);
            SetAnchoredPosition("About", 250, -1300);
        }
    }

    /*
     * Main game scene resolution adjustments.
     */
    private static void HandleMainGame(float aspectRatio) {
        if (aspectRatio == 0.75) {
            Camera.main.orthographicSize = 6.1f;
            ResizeCanvasElement("BackButton", 100, 100);
            SetAnchoredPosition("BackButton", 50, -50);
            ResizeCanvasElement("AudioEnabled", 100, 100);
            SetAnchoredPosition("AudioEnabled", -50, -50);
            ResizeCanvasElement("AudioDisabled", 100, 100);
            SetAnchoredPosition("AudioDisabled", -50, -50);
            ResizeCanvasElement("UpButton", 200, 200);
            SetAnchoredPosition("UpButton", 0, 280);
            ResizeCanvasElement("DownButton", 200, 200);
            SetAnchoredPosition("DownButton", 0, 100);
            ResizeCanvasElement("LeftButton", 200, 200);
            SetAnchoredPosition("LeftButton", -180, 100);
            ResizeCanvasElement("RightButton", 200, 200);
            SetAnchoredPosition("RightButton", 180, 100);
        }
    }

    /*
     * Level Selector resolution adjustments.
     */
    private static void HandleLevelSelector(float aspectRatio) {
        if (aspectRatio == 0.75) {
            ResizeCanvasElement("TitleText", 600, 100);
            SetAnchoredPosition("TitleText", 0, -60);
            SetFontSize("TitleText", 70);
            ResizeCanvasElement("BackButton", 100, 100);
            SetAnchoredPosition("BackButton", 50, -50);
            ResizeCanvasElement("LevelButtonsArea", 950, 1000);
            SetAnchoredPosition("LevelButtonsArea", 0, -625);
            ResizeCanvasElement("ScrollLeftButton", 100, 100);
            SetAnchoredPosition("ScrollLeftButton", -300, 225);
            ResizeCanvasElement("ScrollRightButton", 100, 100);
            SetAnchoredPosition("ScrollRightButton", 300, 225);
            SetAnchoredPosition("GameModeText", 0, 225);
            SetFontSize("GameModeText", 50);
            ResizeCanvasElement("GameModeToggle", 650, 100);
            SetAnchoredPosition("GameModeToggle", 0, 90);
            SetFontSize("ToggleGameModeText", 40);
        }
    }

    /*
     * Options page resolution adjustments.
     */
    private static void HandleOptions(float aspectRatio) {
        if (aspectRatio == 0.75) {
            ResizeCanvasElement("TitleText", 500, 175);
            SetAnchoredPosition("TitleText", 0, -60);
            SetFontSize("TitleText", 70);
            ResizeCanvasElement("BackButton", 100, 100);
            SetAnchoredPosition("BackButton", 50, -50);
            SetAnchoredPosition("AudioControlLabel", 0, -200);
            ResizeCanvasElement("ToggleAudioButton", 300, 125);
            SetAnchoredPosition("ToggleAudioButton", -150, -300);
            ResizeCanvasElement("AudioEnabledImage", 100, 100);
            SetAnchoredPosition("AudioEnabledImage", 150, -300);
            ResizeCanvasElement("AudioDisabledImage", 100, 100);
            SetAnchoredPosition("AudioDisabledImage", 150, -300);
            SetAnchoredPosition("VolumeLabel", 0, -400);
            ResizeCanvasElement("VolumeSlider", 900, 100);
            SetAnchoredPosition("VolumeSlider", 0, -470);
            SetAnchoredPosition("ThemeLabel", 0, -550);
            ResizeCanvasElement("ThemeNormal", 250, 120);
            SetAnchoredPosition("ThemeNormal", -180, -640);
            ResizeCanvasElement("ThemeLight", 250, 120);
            SetAnchoredPosition("ThemeLight", 180, -640);
            ResizeCanvasElement("ThemeDark", 250, 120);
            SetAnchoredPosition("ThemeDark", -180, -760);
            ResizeCanvasElement("ThemeVibrant", 250, 120);
            SetAnchoredPosition("ThemeVibrant", 180, -760);
            SetAnchoredPosition("ControlsLabel", 0, -850);
            ResizeCanvasElement("TouchArrows", 300, 125);
            SetAnchoredPosition("TouchArrows", -180, -940);
            ResizeCanvasElement("TouchClick", 300, 125);
            SetAnchoredPosition("TouchClick", 180, -940);
            ResizeCanvasElement("ControlSchemeArrowImage", 400, 170);
            SetAnchoredPosition("ControlSchemeArrowImage", 0, -1100);
            ResizeCanvasElement("ControlSchemeClickImage", 400, 170);
            SetAnchoredPosition("ControlSchemeClickImage", 0, -1100);
            SetAnchoredPosition("DataLabel", 0, -1220);
            ResizeCanvasElement("ResetData", 300, 125);
            SetAnchoredPosition("ResetData", 0, -1300);
        }
    }
       
    /*
     * About page resolution adjustments.
     */
    private static void HandleAbout(float aspectRatio) {
        if (aspectRatio == 0.75) {
            SetAnchoredPosition("TitleText", 0, -60);
            SetFontSize("TitleText", 70);
            ResizeCanvasElement("BackButton", 100, 100);
            SetAnchoredPosition("BackButton", 50, -50);
            ResizeCanvasElement("Image", 600, 800);
            SetAnchoredPosition("Image", 0, -550);
            SetAnchoredPosition("ImageCaption", 0, -1200);
            SetFontSize("ImageCaption", 35);
            SetAnchoredPosition("AboutText", 0, -1400);
            SetFontSize("AboutText", 35);
        }
    }

    /*
     * Insturctions page resolution adjustments.
     */
    private static void HandleInstructions(float aspectRatio) {
        if (aspectRatio == 0.75) {
            SetAnchoredPosition("TitleText", 0, -60);
            SetFontSize("TitleText", 70);
            ResizeCanvasElement("BackButton", 100, 100);
            SetAnchoredPosition("BackButton", 50, -50);
            ResizeCanvasElement("InstructionsImage", 700, 800);
            SetAnchoredPosition("InstructionsImage", 0, -550);
            ResizeCanvasElement("InstructionsText", 800, 300);
            SetAnchoredPosition("InstructionsText", 0, -1150);
            SetFontSize("InstructionsText", 35);
            ResizeCanvasElement("ScrollLeftButton", 120, 120);
            SetAnchoredPosition("ScrollLeftButton", -300, 150);
            ResizeCanvasElement("ScrollRightButton", 120, 120);
            SetAnchoredPosition("ScrollRightButton", 300, 150);
            SetAnchoredPosition("CurrentPageText", 0, 150);
            SetFontSize("CurrentPageText", 40);
        }
    }

    /* 
     * Level editor page resolution adjustments.
     */
    private static void HandleLevelEditor(float aspectRatio) {
        if (aspectRatio == 0.75) {
            SetAnchoredPosition("TitleText", 0, -60);
            SetFontSize("TitleText", 70);
            ResizeCanvasElement("BackButton", 100, 100);
            SetAnchoredPosition("BackButton", 50, -50);
        }
    }
}
