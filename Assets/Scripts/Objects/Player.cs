using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {
    // Fields
    public float speed;
    public float distance;

    // When the player dies, it will go to this position
    public Transform respawnPoint;
	
    /*
     * Called on every frame.
     */
	void Update() {
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
     * Player death handling.
     */
    public void Death() {
        Debug.Log("Player Death");
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerTransform.transform.position = respawnPoint.transform.position;
    }
}
