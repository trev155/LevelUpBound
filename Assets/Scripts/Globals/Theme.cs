using UnityEngine;
/*
 * This class is for everything related to themes.
 */
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class Theme {
    /*
     * Adjust the theme. 
     * The main elements of the theme include the background colour, and the game scene tiles.
     * Whenever a scene is loaded, this should be called. Or, if the theme is explicitly set from the Options page.
     */
    public static void SetTheme() {
        string themeName = GameContext.Theme;
        Debug.Log("Setting theme to: " + themeName);

        // Determine what the new color should be
        Color32 newColor;
        if (themeName.Equals("Normal")) {
            newColor = new Color32(0, 190, 230, 255);
        } else if (themeName.Equals("Light")) {
            newColor = new Color32(204, 204, 250, 255);
        } else if (themeName.Equals("Dark")) {
            newColor = new Color32(60, 55, 90, 255);
        } else if (themeName.Equals("Vibrant")) {
            newColor = new Color32(205, 205, 10, 255);
        } else {
            Debug.Log("Theme name not recognized. This is not fatal.");
            return;
        }

        // Set the background color to the new color. For the MainGame scene, we use the Main Camera
        // Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "MainGame") {
            Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            camera.backgroundColor = newColor;
        } else {
            Image backgroundPanelImage = GameObject.Find("BackgroundPanel").GetComponent<Image>();
            backgroundPanelImage.color = newColor;
        }
    }
}
