/*
 * The Goal is where the Player is trying to move towards.
 * When the Player reaches the Goal, go to the next level.
 */
using UnityEngine;

public class Goal : MonoBehaviour {
    public LevelManager levelManager;
    public Transform RespawnPoint;
    public UserInterfaceManager UIManager;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            other.transform.position = RespawnPoint.position;
            levelManager.AdvanceLevel();
            UIManager.UpdateLevelText();
        }
    }
}

