/* 
 * Various Utility functions that don't quite fit anywhere else.
 */
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Utils {
    /*
     * Set the alpha property of the image to a low value, to make the image more transparent / grayed out.
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

    public static List<E> ShuffleList<E>(List<E> inputList) {
        List<E> randomList = new List<E>();
        System.Random rng = new System.Random();
        int randomIndex = 0;
        while (inputList.Count > 0) {
            randomIndex = rng.Next(0, inputList.Count);
            randomList.Add(inputList[randomIndex]);
            inputList.RemoveAt(randomIndex);
        }
        return randomList;
    }
}
