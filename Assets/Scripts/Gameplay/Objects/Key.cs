/*
 * A Key is an object that when the player touches, a "door" is unlocked / destroyed.
 */
using UnityEngine;

public class Key : MonoBehaviour {
    private int wallToDestroyX;
    private int wallToDestroyY;
    public Transform lockedWallPrefab;

    private MainGrid mainGrid;
    private GameObject[][] gameGrid;

    private void Awake() {
        mainGrid = GameObject.FindGameObjectWithTag("MainGrid").GetComponent<MainGrid>();
        gameGrid = mainGrid.GetGameGrid();
    }

    public void SetWallCoordinates(int x, int y) {
        wallToDestroyX = x;
        wallToDestroyY = y;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (IsTouchingPlayer(other)) {
            Destroy(gameObject);
            AudioManager.Instance.PlaySound(AudioManager.KEY_PICKUP);
            AudioManager.Instance.PlaySound(AudioManager.WALL_UNLOCK);

            Transform lockedWall = GetWallObject();
            Destroy(lockedWall.gameObject);
            if (IsForDoubleWall()) {
                Instantiate(lockedWallPrefab, gameGrid[wallToDestroyX][wallToDestroyY].transform);   
            }
        }
    }

    private bool IsTouchingPlayer(Collider2D other) {
        return other.tag == "Player";
    }

    private Transform GetWallObject() {
        Transform lockedWall;
        if (IsForDoubleWall()) {
            lockedWall = gameGrid[wallToDestroyX][wallToDestroyY].transform.Find("DoubleLockedWall(Clone)");
        } else {
            lockedWall = gameGrid[wallToDestroyX][wallToDestroyY].transform.Find("LockedWall(Clone)");
        }
        return lockedWall;
    }

    private bool IsForDoubleWall() {
        Transform lockedWall = gameGrid[wallToDestroyX][wallToDestroyY].transform.Find("LockedWall(Clone)");
        return lockedWall == null;
    }
}