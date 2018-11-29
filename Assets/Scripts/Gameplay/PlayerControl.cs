/*
 * Handles the Player Control and movement.
 */
using UnityEngine;
using System.Collections;

public enum ControlMode {
    ARROW,
    CLICK
}

public class PlayerControl : MonoBehaviour {
    private float currentSpriteRotation = 0;

    public float arrowSpeed;
    private readonly float moveDistance = 1.0f;
    private bool enableMoveUp;
    private bool enableMoveDown;
    private bool enableMoveLeft;
    private bool enableMoveRight;

    public float clickSpeed;
    public bool clickMoveActive;
    private Vector3 touchDestination;

    private Transform playerTransform;
    public Transform clickCircleLocation;
    public Transform clickCirclePrefab;

    private void Awake() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        enableMoveUp = false;
        enableMoveDown = false;
        enableMoveLeft = false;
        enableMoveRight = false;

        clickMoveActive = false;

        if (GameContext.ControlScheme == ControlMode.CLICK) {
            DisableCanvasArrows();
        }
    }
   
    private void DisableCanvasArrows() {
        GameObject[] arrows = GameObject.FindGameObjectsWithTag("ControlArrow");
        foreach (GameObject arrow in arrows) {
            arrow.SetActive(false);
        }
    }

    private void Update() {
        if (GameContext.ModalActive) {
            return;
        }

        PreventZRotation();
        transform.Rotate(Vector3.forward * currentSpriteRotation);

        if (GameContext.ControlScheme == ControlMode.ARROW) {
            ArrowMovement();
        }  else if (GameContext.ControlScheme == ControlMode.CLICK) {
            ClickMovement();
        } else {
            Debug.Log("Unknown Control Scheme");
        }
    }

    private void PreventZRotation() {
        if (playerTransform.rotation.z != 0) {
            playerTransform.rotation = Quaternion.Euler(playerTransform.rotation.x, playerTransform.rotation.y, 0);
        }
    }

    private void ArrowMovement() {
        bool up = enableMoveUp || Input.GetAxisRaw("Vertical") > 0;
        bool down = enableMoveDown || Input.GetAxisRaw("Vertical") < 0;
        bool left = enableMoveLeft || Input.GetAxisRaw("Horizontal") < 0;
        bool right = enableMoveRight || Input.GetAxisRaw("Horizontal") > 0;

        if (up) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + moveDistance), Time.deltaTime * arrowSpeed);
        }
        if (down) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y - moveDistance), Time.deltaTime * arrowSpeed);
        }
        if (left) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - moveDistance, transform.position.y), Time.deltaTime * arrowSpeed);
        }
        if (right) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + moveDistance, transform.position.y), Time.deltaTime * arrowSpeed);
        }

        if (up && right) {
            currentSpriteRotation = 315;
        } else if (right && down) {
            currentSpriteRotation = 225;
        } else if (down && left) {
            currentSpriteRotation = 135;
        } else if (left && up) {
            currentSpriteRotation = 45;
        } else if (up) {
            currentSpriteRotation = 360;
        } else if (right) {
            currentSpriteRotation = 270;
        } else if (down) {
            currentSpriteRotation = 180;
        } else if (left) {
            currentSpriteRotation = 90;
        }
    }

    // Button Handlers for arrow movement
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


    private void ClickMovement() {
        if (HasClicked()) {
            SetNewClickDestination();
            RotateSprite();
        }

        if (clickMoveActive) {
            transform.position = Vector3.MoveTowards(transform.position, touchDestination, Time.deltaTime * clickSpeed);
            if (ReachedTouchDestination()) {
                clickMoveActive = false;
            }
        }
    }

    private bool HasClicked() {
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
    }

    private void SetNewClickDestination() {
        Touch touch = Input.GetTouch(0);
        Vector3 touchPoint = Camera.main.ScreenToWorldPoint(touch.position);
        touchPoint.z = 0.0f;

        clickMoveActive = true;
        touchDestination = touchPoint;

        DisplayDestinationCircle(touchPoint);
    }

    private void DisplayDestinationCircle(Vector3 touchPoint) {
        clickCircleLocation.position = touchPoint;
        GameObject circle = Instantiate(clickCirclePrefab, clickCircleLocation).gameObject;
        StartCoroutine(FadeOutAndDestroy(circle));
    }

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

    private void RotateSprite() {
        Vector2 delta = touchDestination - transform.position;
        Quaternion look = Quaternion.LookRotation(delta);
        float vertical = look.eulerAngles.x;
        float horizontal = look.eulerAngles.y;
        if (horizontal < 180) {
            currentSpriteRotation = ((vertical + 90) % 360) * -1;
        } else {
            currentSpriteRotation = vertical + 90;
        }
    }

    private bool ReachedTouchDestination() {
        return transform.position.Equals(touchDestination);
    }
}
