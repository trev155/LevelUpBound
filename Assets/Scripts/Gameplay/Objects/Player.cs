/*
 * Class that handles Player controls, and anything else related to the Player.
 */
using UnityEngine;

public class Player : MonoBehaviour {
    // Objects to keep track of
    public Transform respawnPoint;
    private Transform playerTransform;
    
    /*
     * Initialization
     */
    private void Awake() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    /*
     * Player death handling.
     */
    public void Death() {
        Debug.Log("Player Death");

        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        playerTransform.transform.position = respawnPoint.transform.position;
        audioManager.PlayPlayerDeathAudio();

        if (GameContext.ControlScheme == ControlMode.CLICK) {
            // Stop moving
            PlayerControl control = playerTransform.gameObject.GetComponent<PlayerControl>();
            control.clickMoveActive = false;
        }
    }    
}

    
