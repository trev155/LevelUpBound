/*
 * Class that handles Player controls, and anything else related to the Player.
 */
using UnityEngine;

public class Player : MonoBehaviour {
    // Fields
    public float speed;
    public float distance;

    public bool enableMoveUp;
    public bool enableMoveDown;
    public bool enableMoveLeft;
    public bool enableMoveRight;

    // Objects to keep track of
    public Transform respawnPoint;
    private Transform playerTransform;

    /*
     * Initialization
     */
    private void Awake() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enableMoveUp = false;
        enableMoveDown = false;
        enableMoveLeft = false;
        enableMoveRight = false;
    }

    /*
     * Called on every frame.
     */
    private void Update() {
        PreventZRotation();
        Movement();
    }

    /*
     * Movement controls.
     */
    private void Movement() {
        if (enableMoveUp || Input.GetAxisRaw("Vertical") > 0) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + distance), Time.deltaTime * speed);
        }
        if (enableMoveDown || Input.GetAxisRaw("Vertical") < 0) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y - distance), Time.deltaTime * speed);
        }
        if (enableMoveLeft || Input.GetAxisRaw("Horizontal") < 0) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - distance, transform.position.y), Time.deltaTime * speed);
        }
        if (enableMoveRight || Input.GetAxisRaw("Horizontal") > 0) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + distance, transform.position.y), Time.deltaTime * speed);
        }
    }

    // Button event handlers
    public void StartMoveUp() {
        enableMoveUp = true;
    }

    public void StopMoveUp() {
        enableMoveUp = false;
    }

    public void StartMoveDown() {
        enableMoveDown = true;
    }

    public void StopMoveDown() {
        enableMoveDown = false;
    }

    public void StartMoveLeft() {
        enableMoveLeft = true;
    }

    public void StopMoveLeft() {
        enableMoveLeft = false;
    }

    public void StartMoveRight() {
        enableMoveRight = true;
    }

    public void StopMoveRight() {
        enableMoveRight = false;
    }

    /*
     * Prevent Z-axis rotation.
     */
    private void PreventZRotation() {
        if (playerTransform.rotation.z != 0) {
            playerTransform.rotation = Quaternion.Euler(playerTransform.rotation.x, playerTransform.rotation.y, 0);
        }
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
    }
}

    
