/*
 * When the Player reaches the Goal, handle appropriately.
 */
using UnityEngine;

public class Goal : MonoBehaviour {
    public LevelManager levelManager;
    public AudioManager audioManager;
    public MainGame mainGame;
    public Transform RespawnPoint;
    public GameObject modalWindow;

    void OnTriggerEnter2D(Collider2D other) {
        // Level cleared
        if (other.tag == "Player") {
            audioManager.PlayLevelCompleteSound();

            // Check if last level. If so, display the celebration modal.
            if (GameContext.CurrentLevel == GameMode.GetNumberOfLevels(GameContext.GameMode)) {
                modalWindow.SetActive(true);
                Modal modal = modalWindow.GetComponent<Modal>();
                modal.SetModalTextVictory();
                levelManager.StopLevel();
                // TODO also stop all other actions
                return;
            }
            
            // Check if this level was played from the level selection page. If so, go back to there.
            if (GameContext.LevelSelection) {
                GameContext.LoadPreviousPage();
                GameContext.PreviousPageContext = "MainMenu";
                return;
            }

            // Advance level
            other.transform.position = RespawnPoint.position;
            levelManager.AdvanceLevel();
            mainGame.UpdateLevelText();

            if (GameContext.ControlScheme == ControlMode.CLICK) {
                // Stop movement
                PlayerControl control = other.gameObject.GetComponent<PlayerControl>();
                control.clickMoveActive = false;
            }
        }
    }
}

