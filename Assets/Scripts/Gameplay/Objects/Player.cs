/*
 * Class that handles Player controls, and anything else related to the Player.
 */
using UnityEngine;

public class Player : MonoBehaviour {
    // Objects to keep track of
    public Transform respawnPoint;

    /*
     * Player death handling.
     */
    public void Death() {
        Debug.Log("Player Death");

        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerTransform.transform.position = respawnPoint.transform.position;
        AudioManager.Instance.PlaySound(AudioManager.PLAYER_DEATH);

        if (GameContext.ControlScheme == ControlMode.CLICK) {
            // Stop moving
            PlayerControl control = playerTransform.gameObject.GetComponent<PlayerControl>();
            control.clickMoveActive = false;
        }
    }    
}

    
