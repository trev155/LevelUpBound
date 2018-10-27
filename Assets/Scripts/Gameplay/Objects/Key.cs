using UnityEngine;

public class Key : MonoBehaviour {
    // Reference to the coordinates of the wall that this key unlocks (destroys)
    private int wallToDestroyX;
    private int wallToDestroyY;

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
     * When the Player touches the Key, remove the key and the wall that the key refers to.
     */
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            audioManager.KeyPickup();
            Destroy(gameObject);
            
            audioManager.PlayWallDestroyAudio();
            Destroy(gameGrid[wallToDestroyX][wallToDestroyY].transform.Find("LockedWall(Clone)").gameObject);
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