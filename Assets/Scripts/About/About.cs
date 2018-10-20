/*
 * Everything to do with the "About" page.
 */
using UnityEngine;

public class About : MonoBehaviour {
    public void BackButton() {
        GameContext.LoadPreviousPage();
    }
}
