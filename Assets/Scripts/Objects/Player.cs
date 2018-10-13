using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {
    // Fields
    public float speed;
    public float distance;

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
     * Called on every frame.
     */
    void Update() {
        PreventZRotation();
        Movement();
    }

    /*
     * Listen for cross platform inputs.
     */
    private void Movement() {
        if (CrossPlatformInputManager.GetButton("UpButton") || Input.GetAxisRaw("Vertical") > 0) {
            Debug.Log("Up Button");
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + distance), Time.deltaTime * speed);
        }
        if (CrossPlatformInputManager.GetButton("DownButton") || Input.GetAxisRaw("Vertical") < 0) {
            Debug.Log("Down Button");
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y - distance), Time.deltaTime * speed);
        }
        if (CrossPlatformInputManager.GetButton("RightButton") || Input.GetAxisRaw("Horizontal") > 0) {
            Debug.Log("Right Button");
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + distance, transform.position.y), Time.deltaTime * speed);
        }
        if (CrossPlatformInputManager.GetButton("LeftButton") || Input.GetAxisRaw("Horizontal") < 0) {
            Debug.Log("Left Button");
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - distance, transform.position.y), Time.deltaTime * speed);
        }
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
        playerTransform.transform.position = respawnPoint.transform.position;
    }
}
