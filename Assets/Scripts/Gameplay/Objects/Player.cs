/*
 * The Player is the main character of the game that the user controls.
 */
using UnityEngine;

public class Player : MonoBehaviour {
    public Transform respawnPoint;

    public void Death() {
        MovePlayerToSpawn();
        AudioManager.Instance.PlaySound(AudioManager.PLAYER_DEATH);

        if (GameContext.ControlScheme == ControlMode.CLICK) {
            StopMoving();
        }
    }

    private void MovePlayerToSpawn() {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerTransform.transform.position = respawnPoint.transform.position;
    }

    private void StopMoving() {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerControl control = playerTransform.gameObject.GetComponent<PlayerControl>();
        control.clickMoveActive = false;
    }
}

    
