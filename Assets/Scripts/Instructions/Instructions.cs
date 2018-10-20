/*
 * Everything to do with the Instructions page.
 */
using UnityEngine;

public class Instructions : MonoBehaviour {
    public void BackButton() {
        GameContext.LoadPreviousPage();
    }
}
