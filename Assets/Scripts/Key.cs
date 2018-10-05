using UnityEngine;

public class Key : MonoBehaviour {
    private int wallToDestroyX;
    private int wallToDestroyY;
    private GameObject[][] gameGrid;

    /*
     * Initialization
     */
    private void Awake() {
        MainGrid mainGrid = GameObject.Find("MainGrid").GetComponent<MainGrid>();
        gameGrid = mainGrid.GetGameGrid();
    }

    /*
     * When the Player touches the Key, remove the key and the wall that the key refers to.
     */
    void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player") {
            Destroy(gameObject);
            Destroy(gameGrid[wallToDestroyX][wallToDestroyY].transform.Find("Wall(Clone)").gameObject);
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