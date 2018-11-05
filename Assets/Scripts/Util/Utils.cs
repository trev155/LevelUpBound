/* 
 * Utility functions.
 */
using UnityEngine;
using UnityEngine.UI;

public static class Utils {
    /*
     * Set the alpha property of the image to a low value, to make the image more transparent / grayed out/
     */
    public static void GrayoutImage(Image image) {
        Color tempColor = image.color;
        tempColor.a = 0.1f;
        image.color = tempColor;
    }

    /*
     * Restore image's alpha property to 1.
     */
    public static void UndoGrayoutImage(Image image) {
        Color tempColor = image.color;
        tempColor.a = 1.0f;
        image.color = tempColor;
    }
}
