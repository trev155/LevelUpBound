/*
 * The Goal is where the Player is trying to move towards.
 * When the Player reaches the Goal, go to the next level.
 * If the level was played from the LevelSelector, go back to the LevelSelector scene.
 */
using UnityEngine;

public class Goal : MonoBehaviour {
    public LevelManager levelManager;
    public AudioManager audioManager;
    public UserInterfaceManager UIManager;
    public Transform RespawnPoint;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            if (GameContext.LevelSelection) {
                GameContext.LoadPreviousPage();
                GameContext.PreviousPageContext = "MainMenu";
                return;
            }

            audioManager.LevelComplete();
            other.transform.position = RespawnPoint.position;
            levelManager.AdvanceLevel();
            UIManager.UpdateLevelText();
        }
    }
}

