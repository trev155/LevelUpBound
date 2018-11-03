/*
 * This class handles the Player Controls.
 */
using UnityEngine;
using System.Collections;

public enum ControlMode {
    ARROW,
    CLICK
}

public class PlayerControl : MonoBehaviour {
    // fields for arrow controls
    public float arrowSpeed;
    private readonly float distance = 1.0f;
    private bool enableMoveUp;
    private bool enableMoveDown;
    private bool enableMoveLeft;
    private bool enableMoveRight;

    // fields for click controls
    public float clickSpeed;
    public bool clickMoveActive;
    private Vector3 touchDestination;

    // Objects
    private Transform playerTransform;

    // Click circle
    public Transform clickCircleLocation;
    public Transform clickCirclePrefab;

    /* 
     * Initialization
     */
    private void Awake() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        enableMoveUp = false;
        enableMoveDown = false;
        enableMoveLeft = false;
        enableMoveRight = false;

        clickMoveActive = false;

        // If control mode is click, disable the arrow buttons
        if (GameContext.ControlScheme == ControlMode.CLICK) {
            GameObject[] arrows = GameObject.FindGameObjectsWithTag("ControlArrow");
            foreach (GameObject arrow in arrows) {
                arrow.SetActive(false);
            }
        }
    }

    /*
    * Called on every frame.
    */
    private void Update() {
        if (GameContext.ModalActive) {
            return;
        }

        PreventZRotation();

        if (GameContext.ControlScheme == ControlMode.ARROW) {
            ArrowMovement();
        }  else if (GameContext.ControlScheme == ControlMode.CLICK) {
            ClickMovement();
        } else {
            Debug.Log("Unknown Control Scheme");
        }
    }

    /*
     * Player movement
     */
    private void ArrowMovement() {
        if (enableMoveUp || Input.GetAxisRaw("Vertical") > 0) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + distance), Time.deltaTime * arrowSpeed);
        }
        if (enableMoveDown || Input.GetAxisRaw("Vertical") < 0) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y - distance), Time.deltaTime * arrowSpeed);
        }
        if (enableMoveLeft || Input.GetAxisRaw("Horizontal") < 0) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - distance, transform.position.y), Time.deltaTime * arrowSpeed);
        }
        if (enableMoveRight || Input.GetAxisRaw("Horizontal") > 0) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + distance, transform.position.y), Time.deltaTime * arrowSpeed);
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
     * Movement for the Click control scheme.
     */
    private void ClickMovement() {
        // Listen for clicks
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            // Set new destination
            clickMoveActive = true;
            Touch touch = Input.GetTouch(0);
            Vector3 touchPoint = Camera.main.ScreenToWorldPoint(touch.position);
            touchPoint.z = 0.0f;
            touchDestination = touchPoint;

            // Visual Indication
            clickCircleLocation.position = touchPoint;
            GameObject circle = Instantiate(clickCirclePrefab, clickCircleLocation).gameObject;
            StartCoroutine(FadeOutAndDestroy(circle));
            
            Debug.Log("Touch - Set New Destination at: " + touchDestination);
        }

        if (clickMoveActive) {
            transform.position = Vector3.MoveTowards(transform.position, touchDestination, Time.deltaTime * clickSpeed);
            if (transform.position.Equals(touchDestination)) {
                Debug.Log("Reached destination at " + touchDestination);
                clickMoveActive = false;
            }
        }
    }

    /*
     * Given a Game object, fade out the object by reducing its alpha property repeatedly.
     * After its alpha hits 0, destroy the game object.
     */
    IEnumerator FadeOutAndDestroy(GameObject explosionGameObject) {
        float fadeOutTime = 1.0f;
        SpriteRenderer spriteRenderer = explosionGameObject.GetComponent<SpriteRenderer>();

        Color tmpColor = spriteRenderer.color;
        while (tmpColor.a > 0f) {
            tmpColor.a -= Time.deltaTime / fadeOutTime;
            spriteRenderer.color = tmpColor;
            if (tmpColor.a <= 0f) {
                tmpColor.a = 0f;
            }
            yield return null;
        }
        spriteRenderer.color = tmpColor;
        Destroy(explosionGameObject);
    }
}
