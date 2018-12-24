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
    public static void AdjustScreenElements() {
        float aspectRatio = Camera.main.aspect;
        Debug.Log("Aspect Ratio: " + aspectRatio);
        Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

        // 4:3 portrait (0.70 to 0.80)
        if (EqualsApprox(aspectRatio, 0.75f, 0.05f)) {
            if (scene.name == SceneManager.GetSceneNameString(SceneName.MAIN_MENU)) {
                AdjustMainMenuSceneElements(0.75f);
            } else if (scene.name == SceneManager.GetSceneNameString(SceneName.MAIN_GAME)) {
                AdjustMainGameSceneElements(0.75f);
            } else if (scene.name == SceneManager.GetSceneNameString(SceneName.LEVEL_SELECTOR)) {
                AdjustLevelSelectorSceneElements(0.75f);
            } else if (scene.name == SceneManager.GetSceneNameString(SceneName.OPTIONS)) {
                AdjustOptionsSceneElements(0.75f);
            } else if (scene.name == SceneManager.GetSceneNameString(SceneName.ABOUT)) {
                AdjustAboutSceneElements(0.75f);
            } else if (scene.name == SceneManager.GetSceneNameString(SceneName.INSTRUCTIONS)) {
                AdjustInstructionsSceneElements(0.75f);
            } else if (scene.name == SceneManager.GetSceneNameString(SceneName.LEVEL_EDITOR_MENU)) {
                AdjustLevelEditorSceneElements(0.75f);
            } else if (scene.name == SceneManager.GetSceneNameString(SceneName.INTRO)) {
                AdjustIntroSceneElements(0.75f);
            } else if (scene.name == SceneManager.GetSceneNameString(SceneName.LEVEL_EDITOR_CREATOR)) {
                AdjustLevelCreatorSceneElements(0.75f);
            }
        }

        // 720x1080 (0.55 to 0.65)
        if (EqualsApprox(aspectRatio, 0.60f, 0.05f)) {
            if (scene.name == SceneManager.GetSceneNameString(SceneName.MAIN_MENU)) {
                AdjustMainMenuSceneElements(0.60f);
            } else if (scene.name == SceneManager.GetSceneNameString(SceneName.MAIN_GAME)) {
                AdjustMainGameSceneElements(0.60f);
            } else if (scene.name == SceneManager.GetSceneNameString(SceneName.LEVEL_SELECTOR)) {
                AdjustLevelSelectorSceneElements(0.60f);
            } else if (scene.name == SceneManager.GetSceneNameString(SceneName.OPTIONS)) {
                AdjustOptionsSceneElements(0.60f);
            } else if (scene.name == SceneManager.GetSceneNameString(SceneName.ABOUT)) {
                AdjustAboutSceneElements(0.60f);
            } else if (scene.name == SceneManager.GetSceneNameString(SceneName.INSTRUCTIONS)) {
                AdjustInstructionsSceneElements(0.60f);
            } else if (scene.name == SceneManager.GetSceneNameString(SceneName.LEVEL_EDITOR_MENU)) {
                AdjustLevelEditorSceneElements(0.60f);
            } else if (scene.name == SceneManager.GetSceneNameString(SceneName.INTRO)) {
                AdjustIntroSceneElements(0.60f);
            } else if (scene.name == SceneManager.GetSceneNameString(SceneName.LEVEL_EDITOR_CREATOR)) {
                AdjustLevelCreatorSceneElements(0.60f);
            }
        }
    }

    private static void ResizeCanvasElement(string gameObject, float width, float height) {
        RectTransform element = GameObject.Find(gameObject).GetComponent<RectTransform>();
        element.sizeDelta = new Vector2(width, height);
    }

    private static void SetAnchoredPosition(string gameObject, float x, float y) {
        RectTransform element = GameObject.Find(gameObject).GetComponent<RectTransform>();
        element.anchoredPosition = new Vector2(x, y);
    }

    private static void SetFontSize(string gameObject, int size) {
        Text element = GameObject.Find(gameObject).GetComponent<Text>();
        element.fontSize = size;
    }

    private static bool EqualsApprox(float x, float y, float tolerance) {
        return Mathf.Abs(x - y) < tolerance;    // resolutions aren't always exact, for example 4:3 may not be exactly 0.75, maybe 0.7498.
    }
 
    private static void AdjustMainMenuSceneElements(float aspectRatio) {
        if (EqualsApprox(aspectRatio, 0.75f, 0.01f)) {
            ResizeCanvasElement("TitleText", 1000, 200);
            SetAnchoredPosition("TitleText", 0, -150);
            ResizeCanvasElement("ScrollLeftButton", 125, 125);
            SetAnchoredPosition("ScrollLeftButton", -400, -400);
            ResizeCanvasElement("ScrollRightButton", 125, 125);
            SetAnchoredPosition("ScrollRightButton", 400, -400);
            SetAnchoredPosition("SelectedModeText", 0, -400);
            ResizeCanvasElement("PlayButton", 400, 150);
            SetAnchoredPosition("PlayButton", 0, -550);
            SetAnchoredPosition("LoadButton", 400, -550);
            SetAnchoredPosition("GameModeSelectedDescription", 0, -800);
            SetFontSize("GameModeSelectedDescription", 40);
            SetAnchoredPosition("LevelSelector", -250, -900);
            SetAnchoredPosition("LevelEditor", 250, -900);
            SetAnchoredPosition("Options", 0, -1050);
            SetAnchoredPosition("TutorialText", 0, -1175);
            SetAnchoredPosition("Instructions", -250, -1300);
            SetAnchoredPosition("About", 250, -1300);
        }

        if (EqualsApprox(aspectRatio, 0.60f, 0.01f)) {
            ResizeCanvasElement("TitleText", 1000, 200);
            SetAnchoredPosition("TitleText", 0, -150);
            ResizeCanvasElement("ScrollLeftButton", 125, 125);
            SetAnchoredPosition("ScrollLeftButton", -400, -400);
            ResizeCanvasElement("ScrollRightButton", 125, 125);
            SetAnchoredPosition("ScrollRightButton", 400, -400);
            SetAnchoredPosition("SelectedModeText", 0, -400);
            ResizeCanvasElement("PlayButton", 400, 150);
            SetAnchoredPosition("PlayButton", 0, -550);
            SetAnchoredPosition("LoadButton", 400, -550);
            SetAnchoredPosition("GameModeSelectedDescription", 0, -800);
            SetFontSize("GameModeSelectedDescription", 40);
            SetAnchoredPosition("LevelSelector", -250, -1000);
            SetAnchoredPosition("LevelEditor", 250, -1000);
            SetAnchoredPosition("Options", 0, -1200);
            SetAnchoredPosition("TutorialText", 0, -1350);
            SetAnchoredPosition("Instructions", -250, -1500);
            SetAnchoredPosition("About", 250, -1500);
        }
    }

    private static void AdjustMainGameSceneElements(float aspectRatio) {
        if (EqualsApprox(aspectRatio, 0.75f, 0.01f)) {
            Camera.main.orthographicSize = 6.1f;
            ResizeCanvasElement("BackButton", 100, 100);
            SetAnchoredPosition("BackButton", 50, -50);
            ResizeCanvasElement("AudioEnabled", 100, 100);
            SetAnchoredPosition("AudioEnabled", -50, -50);
            ResizeCanvasElement("AudioDisabled", 100, 100);
            SetAnchoredPosition("AudioDisabled", -50, -50);
            if (GameContext.ControlScheme == ControlMode.ARROW) {
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

        if (EqualsApprox(aspectRatio, 0.60f, 0.01f)) {
            // none
        }
    }

    private static void AdjustLevelSelectorSceneElements(float aspectRatio) {
        if (EqualsApprox(aspectRatio, 0.75f, 0.01f)) {
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

        if (EqualsApprox(aspectRatio, 0.60f, 0.01f)) {
            ResizeCanvasElement("LevelButtonsArea", 950, 1000);
            SetAnchoredPosition("LevelButtonsArea", 0, -750);

            ResizeCanvasElement("ScrollLeftButton", 100, 100);
            SetAnchoredPosition("ScrollLeftButton", -300, 250);
            ResizeCanvasElement("ScrollRightButton", 100, 100);
            SetAnchoredPosition("ScrollRightButton", 300, 250);
            SetAnchoredPosition("GameModeText", 0, 250);
            SetFontSize("GameModeText", 50);
            ResizeCanvasElement("GameModeToggle", 650, 100);
            SetAnchoredPosition("GameModeToggle", 0, 100);
            SetFontSize("ToggleGameModeText", 50);
        }
    }

    private static void AdjustOptionsSceneElements(float aspectRatio) {
        if (EqualsApprox(aspectRatio, 0.75f, 0.01f)) {
            ResizeCanvasElement("TitleText", 500, 175);
            SetAnchoredPosition("TitleText", 0, -60);
            SetFontSize("TitleText", 70);
            ResizeCanvasElement("BackButton", 100, 100);
            SetAnchoredPosition("BackButton", 50, -50);
            SetAnchoredPosition("AudioControlLabel", 0, -200);
            SetFontSize("AudioControlLabel", 45);
            ResizeCanvasElement("ToggleAudioButton", 300, 125);
            SetAnchoredPosition("ToggleAudioButton", -150, -300);
            ResizeCanvasElement("AudioEnabledImage", 100, 100);
            SetAnchoredPosition("AudioEnabledImage", 150, -300);
            ResizeCanvasElement("AudioDisabledImage", 100, 100);
            SetAnchoredPosition("AudioDisabledImage", 150, -300);
            SetAnchoredPosition("BackgroundMusicVolumeLabel", 0, -400);
            SetFontSize("BackgroundMusicVolumeLabel", 45);
            ResizeCanvasElement("BackgroundMusicVolumeSlider", 900, 100);
            SetAnchoredPosition("BackgroundMusicVolumeSlider", 0, -470);
            SetAnchoredPosition("SoundEffectsVolumeLabel", 0, -530);
            SetFontSize("SoundEffectsVolumeLabel", 45);
            ResizeCanvasElement("SoundEffectsVolumeSlider", 900, 100);
            SetAnchoredPosition("SoundEffectsVolumeSlider", 0, -600);
            SetAnchoredPosition("ThemeLabel", 0, -650);
            SetFontSize("ThemeLabel", 45);
            ResizeCanvasElement("ThemeNormal", 250, 100);
            SetAnchoredPosition("ThemeNormal", -180, -720);
            ResizeCanvasElement("ThemeLight", 250, 100);
            SetAnchoredPosition("ThemeLight", 180, -720);
            ResizeCanvasElement("ThemeDark", 250, 100);
            SetAnchoredPosition("ThemeDark", -180, -820);
            ResizeCanvasElement("ThemeVibrant", 250, 100);
            SetAnchoredPosition("ThemeVibrant", 180, -820);
            SetAnchoredPosition("ControlsLabel", 0, -900);
            SetFontSize("ControlsLabel", 45);
            ResizeCanvasElement("TouchArrows", 300, 125);
            SetAnchoredPosition("TouchArrows", -180, -980);
            ResizeCanvasElement("TouchClick", 300, 125);
            SetAnchoredPosition("TouchClick", 180, -980);
            ResizeCanvasElement("ControlSchemeArrowImage", 400, 170);
            SetAnchoredPosition("ControlSchemeArrowImage", 0, -1150);
            ResizeCanvasElement("ControlSchemeClickImage", 400, 170);
            SetAnchoredPosition("ControlSchemeClickImage", 0, -1150);
            SetAnchoredPosition("DataLabel", 0, -1250);
            SetFontSize("DataLabel", 45);
            ResizeCanvasElement("ResetData", 300, 125);
            SetAnchoredPosition("ResetData", -200, -1330);
            ResizeCanvasElement("ResetOptions", 300, 125);
            SetAnchoredPosition("ResetOptions", 200, -1330);
        }

        if (EqualsApprox(aspectRatio, 0.60f, 0.01f)) {
            ResizeCanvasElement("TitleText", 500, 175);
            SetAnchoredPosition("TitleText", 0, -60);
            SetFontSize("TitleText", 70);
            ResizeCanvasElement("BackButton", 100, 100);
            SetAnchoredPosition("BackButton", 50, -50);
            SetAnchoredPosition("AudioControlLabel", 0, -200);
            SetFontSize("AudioControlLabel", 45);
            ResizeCanvasElement("ToggleAudioButton", 300, 125);
            SetAnchoredPosition("ToggleAudioButton", -150, -300);
            ResizeCanvasElement("AudioEnabledImage", 100, 100);
            SetAnchoredPosition("AudioEnabledImage", 150, -300);
            ResizeCanvasElement("AudioDisabledImage", 100, 100);
            SetAnchoredPosition("AudioDisabledImage", 150, -300);
            SetAnchoredPosition("BackgroundMusicVolumeLabel", 0, -400);
            SetFontSize("BackgroundMusicVolumeLabel", 45);
            ResizeCanvasElement("BackgroundMusicVolumeSlider", 900, 100);
            SetAnchoredPosition("BackgroundMusicVolumeSlider", 0, -470);
            SetAnchoredPosition("SoundEffectsVolumeLabel", 0, -530);
            SetFontSize("SoundEffectsVolumeLabel", 45);
            ResizeCanvasElement("SoundEffectsVolumeSlider", 900, 100);
            SetAnchoredPosition("SoundEffectsVolumeSlider", 0, -600);
            SetAnchoredPosition("ThemeLabel", 0, -650);
            SetFontSize("ThemeLabel", 45);
            ResizeCanvasElement("ThemeNormal", 250, 100);
            SetAnchoredPosition("ThemeNormal", -180, -720);
            ResizeCanvasElement("ThemeLight", 250, 100);
            SetAnchoredPosition("ThemeLight", 180, -720);
            ResizeCanvasElement("ThemeDark", 250, 100);
            SetAnchoredPosition("ThemeDark", -180, -820);
            ResizeCanvasElement("ThemeVibrant", 250, 100);
            SetAnchoredPosition("ThemeVibrant", 180, -820);
            SetAnchoredPosition("ControlsLabel", 0, -920);
            SetFontSize("ControlsLabel", 45);
            ResizeCanvasElement("TouchArrows", 300, 125);
            SetAnchoredPosition("TouchArrows", -180, -1020);
            ResizeCanvasElement("TouchClick", 300, 125);
            SetAnchoredPosition("TouchClick", 180, -1020);
            ResizeCanvasElement("ControlSchemeArrowImage", 400, 170);
            SetAnchoredPosition("ControlSchemeArrowImage", 0, -1180);
            ResizeCanvasElement("ControlSchemeClickImage", 400, 170);
            SetAnchoredPosition("ControlSchemeClickImage", 0, -1180);
            SetAnchoredPosition("DataLabel", 0, -1310);
            SetFontSize("DataLabel", 45);
            ResizeCanvasElement("ResetData", 300, 125);
            SetAnchoredPosition("ResetData", -200, -1400);
            ResizeCanvasElement("ResetOptions", 300, 125);
            SetAnchoredPosition("ResetOptions", 200, -1400);
        }
    }
       
    private static void AdjustAboutSceneElements(float aspectRatio) {
        if (EqualsApprox(aspectRatio, 0.75f, 0.01f)) {
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

        if (EqualsApprox(aspectRatio, 0.60f, 0.01f)) {
            ResizeCanvasElement("Image", 600, 700);
            SetAnchoredPosition("Image", 0, -500);
            SetAnchoredPosition("ImageCaption", 0, -1200);
            SetFontSize("ImageCaption", 35);
            SetAnchoredPosition("AboutText", 0, -1500);
            SetFontSize("AboutText", 35);
        }
    }

    private static void AdjustInstructionsSceneElements(float aspectRatio) {
        if (EqualsApprox(aspectRatio, 0.75f, 0.01f)) {
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

        if (EqualsApprox(aspectRatio, 0.60f, 0.01f)) {
            // none
        }
    }

    private static void AdjustLevelEditorSceneElements(float aspectRatio) {
        if (EqualsApprox(aspectRatio, 0.75f, 0.01f)) {
            SetAnchoredPosition("TitleText", 0, -60);
            SetFontSize("TitleText", 70);
            ResizeCanvasElement("BackButton", 100, 100);
            SetAnchoredPosition("BackButton", 50, -50);
        }

        if (EqualsApprox(aspectRatio, 0.60f, 0.01f)) {
            // none
        }
    }

    private static void AdjustIntroSceneElements(float aspectRatio) {
        if (EqualsApprox(aspectRatio, 0.75f, 0.01f)) {
            ResizeCanvasElement("CloudOutline", 900, 700);
            SetAnchoredPosition("CloudOutline", 0, -380);
            SetFontSize("TitleText", 120);
            ResizeCanvasElement("TitleText", 600, 500);
            ResizeCanvasElement("Arrow", 400, 400);
        }

        if (EqualsApprox(aspectRatio, 0.60f, 0.01f)) {
            // none
        }
    }

    private static void AdjustLevelCreatorSceneElements(float aspectRatio) {
        if (EqualsApprox(aspectRatio, 0.75f, 0.01f)) {
            ResizeCanvasElement("GridContainer", 720, 720);
            SetAnchoredPosition("GridContainer", 0, -400);
            SetAnchoredPosition("0,0", 80, 80);
            SetAnchoredPosition("0,1", 220, 80);
            SetAnchoredPosition("0,2", 360, 80);
            SetAnchoredPosition("0,3", 500, 80);
            SetAnchoredPosition("0,4", 640, 80);
            SetAnchoredPosition("1,0", 80, 220);
            SetAnchoredPosition("1,1", 220, 220);
            SetAnchoredPosition("1,2", 360, 220);
            SetAnchoredPosition("1,3", 500, 220);
            SetAnchoredPosition("1,4", 640, 220);
            SetAnchoredPosition("2,0", 80, 360);
            SetAnchoredPosition("2,1", 220, 360);
            SetAnchoredPosition("2,2", 360, 360);
            SetAnchoredPosition("2,3", 500, 360);
            SetAnchoredPosition("2,4", 640, 360);
            SetAnchoredPosition("3,0", 80, 500);
            SetAnchoredPosition("3,1", 220, 500);
            SetAnchoredPosition("3,2", 360, 500);
            SetAnchoredPosition("3,3", 500, 500);
            SetAnchoredPosition("3,4", 640, 500);
            SetAnchoredPosition("4,0", 80, 640);
            SetAnchoredPosition("4,1", 220, 640);
            SetAnchoredPosition("4,2", 360, 640);
            SetAnchoredPosition("4,3", 500, 640);
            SetAnchoredPosition("4,4", 640, 640);

            SetAnchoredPosition("LevelDefinitionPanel", -300, -1050);
            SetAnchoredPosition("ControlPanel", 220, -1050);

            SetAnchoredPosition("FooterPanel", 220, -1340);
        }

        if (EqualsApprox(aspectRatio, 0.60f, 0.01f)) {
            // none
        }
    }
}
