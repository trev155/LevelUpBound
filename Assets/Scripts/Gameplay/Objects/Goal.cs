/*
 * When the Player reaches the Goal, handle appropriately.
 */
using UnityEngine;

public class Goal : MonoBehaviour {
    public LevelManager levelManager;
    public MainGame mainGame;
    public Transform RespawnPoint;

    // Modals
    public Transform modalContainer;
    public ModalVictory modalWindowVictory;

    void OnTriggerEnter2D(Collider2D other) {
        // Level cleared
        if (other.tag == "Player") {
            AudioManager.Instance.PlaySound(AudioManager.LEVEL_UP);

            // Check if last level. If so, display the celebration modal.
            if (GameContext.CurrentLevel == GameMode.GetNumberOfLevels(GameContext.GameMode)) {
                ModalVictory modal = Instantiate(modalWindowVictory, modalContainer).GetComponent<ModalVictory>();
                modal.SetModalTextVictory();
                levelManager.StopLevel();
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

