/* 
 * Utility functions.
 */
using UnityEngine;
using UnityEngine.UI;

public static class Util {
    public static void GrayoutImage(Image image) {
        Color tempColor = image.color;
        tempColor.a = 0.1f;
        image.color = tempColor;
    }

    public static void UndoGrayoutImage(Image image) {
        Color tempColor = image.color;
        tempColor.a = 1.0f;
        image.color = tempColor;
    }
}
