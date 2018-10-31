/*
 * A MovingObject moves around certain predefined waypoints. If it makes 
 * contact with the player, the player dies.
 */
using UnityEngine;

public class MovingObject : MonoBehaviour {
    public float speed;

    // Require access to the game grid for waypoint destinations
    private GameObject[][] gameGrid;

    // Waypoints - destinations for the moving object
    Transform[] objectWaypoints;
    private int currentWaypointIndex = 1;

    // whether we should start moving the object
    private bool startMovement = false;

    // Reference to the Player
    public Player player;

    /*
     * Detect if making contact with the Player.
     */
    void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player") {
            player.Death();
        }
    }

    /*
     * Initialize this object's waypoints
     */
    public void SetWaypoints(string[] waypoints) {
        objectWaypoints = new Transform[waypoints.Length];
        for (int i = 0; i < waypoints.Length; i++) {
            int row = int.Parse(waypoints[i].Substring(0, 1));
            int col = int.Parse(waypoints[i].Substring(2, 1));
            objectWaypoints[i] = gameGrid[row][col].transform;
        }      
    }

    /*
     * Initialization
     */
    private void Awake() {
        MainGrid mainGrid = GameObject.Find("MainGrid").GetComponent<MainGrid>();
        gameGrid = mainGrid.GetGameGrid();
    }

    /*
     * Want to call this at the start, but has to be after Awake().
     */
    private void Start() {
        FlipSprite();
    }

    /*
     * Called once per frame.
     */
    private void Update() {
        if (startMovement) {
            Movement();
            Debug.Log("move");
        }
    }

    /*
     * Don't start movement until we have our waypoints set.
     */
    public void StartMovement() {
        startMovement = true;
    }

    /*
     * Move between the waypoints.
     */
    private void Movement() {
        transform.position = Vector2.MoveTowards(transform.position, objectWaypoints[currentWaypointIndex].position, Time.deltaTime * speed);

        // if reached destination, update waypoint
        if (transform.position == objectWaypoints[currentWaypointIndex].position) {
            AdvanceWaypointIndex();
            FlipSprite();
        }
    }

    /*
     * Advance the waypoint index. Go back to 0 if at the end of the array.
     */
    private void AdvanceWaypointIndex() {
        currentWaypointIndex = (currentWaypointIndex + 1) % objectWaypoints.Length;
    }

    /*
     * Flip sprite based on position and movement direction.
     */
    private void FlipSprite() {
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