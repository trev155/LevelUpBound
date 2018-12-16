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
    public ModalInfo modalWindowVictory;

    void OnTriggerEnter2D(Collider2D other) {
        if (LevelIsCleared(other)) {
            Collider2D player = other;
            AudioManager.Instance.PlaySound(AudioManager.LEVEL_UP);
            LevelManager.RecordLevelCompleted(GameContext.GameMode, GameContext.CurrentLevel);

            if (LevelManager.CameFromCustomLevelEditor()) {
                SceneManager.LoadPreviousContextPage();
                GameContext.PreviousPageContext = SceneName.MAIN_MENU;
                return;
            }

            if (LevelManager.CameFromLevelSelector()) {
                SceneManager.LoadPreviousContextPage();
                GameContext.PreviousPageContext = SceneName.MAIN_MENU;
                PersistentStorage.SaveData();
                return;
            }

            if (LevelManager.CompletedAllLevelsInGameMode()) {
                ModalInfo modal = Instantiate(modalWindowVictory, modalContainer).GetComponent<ModalInfo>();
                modal.InitializeVictoryModal();
                levelManager.StopLevel();
                if (GameContext.GameMode != Mode.TUTORIAL) {
                    SavedGameManager.ClearSavedGameData();
                }
                PersistentStorage.SaveData();
                return;
            }

            MovePlayerToSpawn(player);
            levelManager.LevelCompletedHandler();
            mainGame.UpdateLevelText();
            if (GameContext.GameMode != Mode.TUTORIAL) {
                SavedGameManager.UpdateSavedGameData();
            }
            if (GameContext.ControlScheme == ControlMode.CLICK) {
                StopPlayerMovement(player);
            }

            PersistentStorage.SaveData();
        }
    }

    private bool LevelIsCleared(Collider2D other) {
        return other.tag == "Player";
    }

    private void MovePlayerToSpawn(Collider2D player) {
        player.transform.position = RespawnPoint.position;
    }
    
    private void StopPlayerMovement(Collider2D player) {
        PlayerControl control = player.gameObject.GetComponent<PlayerControl>();
        control.clickMoveActive = false;
    }
}