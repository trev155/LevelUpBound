/*
 * The goal is the beacon at the top of the screen that the player is trying to reach. 
 * When the player touches the goal, the level is completed.
 */
using UnityEngine;

public class Goal : MonoBehaviour {
    public LevelManager levelManager;
    public MainGame mainGame;
    public Transform RespawnPoint;

    public Transform modalContainer;
    public ModalVictory modalWindowVictory;

    void OnTriggerEnter2D(Collider2D other) {
        if (LevelCleared(other)) {
            Collider2D player = other;
            AudioManager.Instance.PlaySound(AudioManager.LEVEL_UP);
            GameContext.SaveLevelCompleted(GameContext.GameMode, GameContext.CurrentLevel);
            Memory.SaveData();

            if (ShouldDisplayCelebrationModal()) {
                ModalVictory modal = Instantiate(modalWindowVictory, modalContainer).GetComponent<ModalVictory>();
                modal.SetModalTextVictory();
                levelManager.StopLevel();
                return;
            }
            
            if (CameFromLevelSelector()) {
                GameContext.LoadPreviousPage();
                GameContext.PreviousPageContext = "MainMenu";
                return;
            }

            MovePlayerToSpawn(player);
            levelManager.AdvanceLevel();
            mainGame.UpdateLevelText();

            if (GameContext.ControlScheme == ControlMode.CLICK) {
                StopPlayerMovement(player);
            }
        }
    }

    private bool LevelCleared(Collider2D other) {
        return other.tag == "Player";
    }

    private bool ShouldDisplayCelebrationModal() {
        return IsLastLevel() && !CameFromLevelSelector();
    }

    private bool IsLastLevel() {
        return GameContext.CurrentLevel == GameMode.GetNumberOfLevels(GameContext.GameMode);
    }

    private bool CameFromLevelSelector() {
        return GameContext.LevelSelection;
    }

    private void MovePlayerToSpawn(Collider2D player) {
        player.transform.position = RespawnPoint.position;
    }
    
    private void StopPlayerMovement(Collider2D player) {
        PlayerControl control = player.gameObject.GetComponent<PlayerControl>();
        control.clickMoveActive = false;
    }
}

