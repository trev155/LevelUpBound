﻿using UnityEngine;

public class Key : MonoBehaviour {
    // Reference to the coordinates of the wall that this key unlocks (destroys)
    private int wallToDestroyX;
    private int wallToDestroyY;

    // Reference to the Main Grid
    private MainGrid mainGrid;
    private GameObject[][] gameGrid;

    /*
     * Initialization
     */
    private void Awake() {
        mainGrid = GameObject.FindGameObjectWithTag("MainGrid").GetComponent<MainGrid>();
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