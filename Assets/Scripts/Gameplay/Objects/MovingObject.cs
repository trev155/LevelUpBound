/*
 * A MovingObject moves around certain predefined waypoints. If it makes 
 * contact with the player, the player dies.
 */
using UnityEngine;

public class MovingObject : MonoBehaviour {
    public float speed;
    private GameObject[][] gameGrid;
    public Transform[] objectWaypoints;
    private int currentWaypointIndex = 1;
    private bool startMovement = false;
    public Player player;

    void OnTriggerStay2D(Collider2D other) {
        if (IsTouchingPlayer(other)) {
            player.Death();
        }
    }

    private bool IsTouchingPlayer(Collider2D other) {
        return other.tag == "Player";
    }

    public void SetWaypoints(string[] waypoints) {
        objectWaypoints = new Transform[waypoints.Length];
        for (int i = 0; i < waypoints.Length; i++) {
            int row = int.Parse(waypoints[i].Substring(0, 1));
            int col = int.Parse(waypoints[i].Substring(2, 1));
            objectWaypoints[i] = gameGrid[row][col].transform;
        }      
    }

    public void StartMovement() {
        startMovement = true;
    }

    private void Awake() {
        MainGrid mainGrid = GameObject.Find("MainGrid").GetComponent<MainGrid>();
        gameGrid = mainGrid.GetGameGrid();
    }

    private void Start() {
        SetSpriteOrientation();
    }

    private void Update() {
        if (startMovement) {
            DoMovement();
        }
    }

    private void DoMovement() {
        MoveTowardsWaypoint();
        if (ReachedWaypoint()) {
            AdvanceWaypointIndex();
            SetSpriteOrientation();
        }
    }

    private void MoveTowardsWaypoint() {
        transform.position = Vector2.MoveTowards(transform.position, objectWaypoints[currentWaypointIndex].position, Time.deltaTime * speed);
    }

    private bool ReachedWaypoint() {
        return transform.position == objectWaypoints[currentWaypointIndex].position;
    }

    private void AdvanceWaypointIndex() {
        currentWaypointIndex = (currentWaypointIndex + 1) % objectWaypoints.Length;
    }

    private void SetSpriteOrientation() {
        // Debug.Log("Current Position: " + transform.position + " Next Position: " + objectWaypoints[currentWaypointIndex].position);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float currentX = transform.position.x;
        float currentY = transform.position.y;
        float nextX = objectWaypoints[currentWaypointIndex].position.x;
        float nextY = objectWaypoints[currentWaypointIndex].position.y;
        
        if (currentX < nextX) {
            transform.Rotate(Vector3.forward * -90);
        }
        if (currentX > nextX) {
            transform.Rotate(Vector3.back * 90);
        }
        if (currentY < nextY) {
            spriteRenderer.flipY = false;
            transform.rotation = Quaternion.identity;
        }
        if (currentY > nextY) {
            spriteRenderer.flipY = true;
            transform.rotation = Quaternion.identity;
        }
    }
}