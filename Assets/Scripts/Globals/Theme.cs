/*
 * This class is for everything related to themes.
 */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class Theme {
    /*
     * Adjust the theme. 
     * The main elements of the theme include the background colour, and the game scene tiles.
     * Whenever a scene is loaded, this should be called. Or, if the theme is explicitly set from the Options page.
     */
    public static void SetTheme() {
        string scene = SceneManager.GetActiveScene().name;
        string theme = GameContext.Theme;
        
        SetBackground(scene, theme);
        SetTextElementColours(theme);
        SetGameElementsColours(scene, theme);
    }
    
    /*
     * Sets the background colour of the scene.
     */
    private static void SetBackground(string scene, string theme) {
        // Determine what the new color should be based on the current theme selection
        Color32 newColor;
        if (theme.Equals("Normal")) {
            newColor = new Color32(0, 190, 230, 255);
        } else if (theme.Equals("Light")) {
            newColor = new Color32(204, 204, 250, 255);
        } else if (theme.Equals("Dark")) {
            newColor = new Color32(58, 66, 60, 255);
        } else if (theme.Equals("Vibrant")) {
            newColor = new Color32(205, 205, 10, 255);
        } else {
            Debug.Log("Theme name not recognized. Cannot set background colour.");
            return;
        }

        // Set the background color to the new color. For the MainGame scene, we use the Main Camera
        if (scene == "MainGame") {
            Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            camera.backgroundColor = newColor;
        } else {
            Image backgroundPanelImage = GameObject.Find("BackgroundPanel").GetComponent<Image>();
            backgroundPanelImage.color = newColor;
        }

        Debug.Log("Setting theme to: " + theme + ", in scene: " + scene);
    }

    /*
     * Sets the colour of all canvas text elements on the current page.
     */
    private static void SetTextElementColours(string theme) {
        // Determine what the new color should be based on the current theme selection
        Color32 textColor;
        if (theme.Equals("Dark")) {
            textColor = new Color32(97, 147, 244, 255);
        } else {
            textColor = new Color32(30, 30, 10, 255);
        }

        // Set text colors
        Text[] textGameObjects = Object.FindObjectsOfType<Text>();
        foreach (Text textObject in textGameObjects) {
            textObject.color = textColor;
        }
    }

    /*
     * Sets the colour of the main game elements.
     */
    private static void SetGameElementsColours(string scene, string theme) {
        if (scene != "MainGame") {
            return;
        }

        // Tile colours
        Color32 evenTileColor;
        Color32 oddTileColor;
        if (theme == "Normal") {
            evenTileColor = new Color32(176, 166, 138, 255);
            oddTileColor = new Color32(99, 99, 21, 255);
        } else if (theme == "Light") {
            evenTileColor = new Color32(41, 133, 169, 255);
            oddTileColor = new Color32(187, 243, 250, 255);
        } else if (theme == "Dark") {
            evenTileColor = new Color32(50, 10, 100, 255);
            oddTileColor = new Color32(0, 80, 15, 255);
        } else if (theme == "Vibrant") {
            evenTileColor = new Color32(179, 248, 169, 255);
            oddTileColor = new Color32(79, 206, 255, 255);
        } else {
            Debug.Log("Theme name not recognized. Cannot same game element colours.");
            return;
        }
        
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("GameTile");
        foreach (GameObject tile in tiles) {
            SpriteRenderer tileSpriteRenderer = tile.GetComponent<SpriteRenderer>();
            string tileName = tile.name;
            int tileRow = int.Parse(tileName.Substring(0, 1));
            int tileCol = int.Parse(tileName.Substring(2, 1));
            if ((tileRow + tileCol) % 2 == 0) { 
                tileSpriteRenderer.color = evenTileColor;
            } else {
                tileSpriteRenderer.color = oddTileColor;
            }
        }

        // Arrow Buttons
        if (GameContext.ControlScheme == ControlMode.ARROW) {
            Color32 arrowColor;
            if (theme == "Dark") {
                arrowColor = new Color32(255, 255, 0, 220);
            } else {
                arrowColor = new Color32(194, 194, 194, 150);
            }

            GameObject[] arrowObjects = GameObject.FindGameObjectsWithTag("ControlArrow");
            foreach (GameObject arrow in arrowObjects) {
                Image arrowSpriteRenderer = arrow.GetComponent<Image>();
                arrowSpriteRenderer.color = arrowColor;
            }
        }
    }
}
