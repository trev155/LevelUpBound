/*
 * A Key is an object that when the player touches, a "door" is unlocked.
 */
using UnityEngine;

public class Key : MonoBehaviour {
    // Reference to the coordinates of the wall that this key unlocks (destroys)
    private int wallToDestroyX;
    private int wallToDestroyY;

    // For double walls, we replace with single wall
    public Transform lockedWallPrefab;

    // Reference to the Main Grid
    private MainGrid mainGrid;
    private GameObject[][] gameGrid;

    // Reference to audio management
    private AudioManager audioManager;

    /*
     * Initialization
     */
    private void Awake() {
        mainGrid = GameObject.FindGameObjectWithTag("MainGrid").GetComponent<MainGrid>();
        gameGrid = mainGrid.GetGameGrid();
        
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    /*
     * When the Player touches the Key, unlock the door/lock that the key refers to.
     */
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            audioManager.KeyPickup();
            Destroy(gameObject);
            
            audioManager.PlayWallUnlockAudio();
            bool isDouble = false;
            Transform lockedWall = gameGrid[wallToDestroyX][wallToDestroyY].transform.Find("LockedWall(Clone)");
            if (lockedWall == null) {
                lockedWall = gameGrid[wallToDestroyX][wallToDestroyY].transform.Find("DoubleLockedWall(Clone)");
                isDouble = true;
            }
            
            if (!isDouble) {
                Destroy(lockedWall.gameObject);
            } else {
                Destroy(lockedWall.gameObject);
                Instantiate(lockedWallPrefab, gameGrid[wallToDestroyX][wallToDestroyY].transform);
            }
        }
    }

    /*
     * Sets the wall coordinates that the key refers to.
     */
    public void InitData(int x, int y) {
        wallToDestroyX = x;
        wallToDestroyY = y;
    }
}