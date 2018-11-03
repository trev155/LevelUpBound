/*
 * In order to support multiple resolutions, what we'll do is 
 * adjust the BackgroundPanel on every scene. Each scene should have one
 * of these, holding all canvas elements on the screen.
 * Canvas elements should be anchored to the BackgroundPanel.
 * 
 * Whenever a scene is loaded, we should adjust this BackgroundPanel's dimensions.
 * 
 * Note that by default, I'm developing for 18:9. That is, in my Unity editor, everything
 * is initially 18:9, and I will adjust for other dimensions.
 * This is because my personal device is 18:9, so it's a bit more convenient in that sense.
 */
using UnityEngine;

public static class AspectRatioManager {
    public static void AdjustScreen() {
        float aspectRatio = Camera.main.aspect;
        if (aspectRatio == 0.5f) {
            // 18:9 portrait
            SetBackgroundPanelDimensions(1080, 2160);
        } else if (aspectRatio == 0.5625f) {
            // 16:9 portrait
            SetBackgroundPanelDimensions(1080, 1920);
        } else {
            Debug.Log("This aspect ratio is not supported yet: " + aspectRatio);
        }
    }

    private static void SetBackgroundPanelDimensions(float width, float height) {
        Debug.Log("Setting dimensions to: " + width + ", " + height);
        RectTransform backgroundPanel = GameObject.Find("BackgroundPanel").GetComponent<RectTransform>();
        backgroundPanel.sizeDelta = new Vector2(width, height);
    }
}
