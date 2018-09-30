using UnityEngine;
using System.Collections;

public class ExplosionManager : MonoBehaviour {
    // constants
    private const float explosionCooldown = 0.1f;

    // Reference to the game grid
    public MainGrid mainGrid;
    private GameObject[][] gameGrid;

    // Reference to audio manager
    public AudioManager audioManager;


    /*
     * Initialization
     */
    void Awake() {
        gameGrid = mainGrid.GetGameGrid();
    }

    /*
     * Wrapper functions for the Explode Coroutine. 
     */
    public void ExplodeTile(int row, int col) {
        StartCoroutine(Explode(row, col));
    }

    public void ExplodeRow(int row) {
        StartCoroutine(Explode(row, 0));
        StartCoroutine(Explode(row, 1));
        StartCoroutine(Explode(row, 2));
        StartCoroutine(Explode(row, 3));
        StartCoroutine(Explode(row, 4));
    }

    public void ExplodeColumn(int col) {
        StartCoroutine(Explode(0, col));
        StartCoroutine(Explode(1, col));
        StartCoroutine(Explode(2, col));
        StartCoroutine(Explode(3, col));
        StartCoroutine(Explode(4, col));
    }

    public void ExplodeDiagonalBottomLeft() {
        StartCoroutine(Explode(4, 0));
        StartCoroutine(Explode(3, 1));
        StartCoroutine(Explode(2, 2));
        StartCoroutine(Explode(1, 3));
        StartCoroutine(Explode(0, 4));
    }

    public void ExplodeTileDiagonalTopLeft() {
        StartCoroutine(Explode(0, 0));
        StartCoroutine(Explode(1, 1));
        StartCoroutine(Explode(2, 2));
        StartCoroutine(Explode(3, 3));
        StartCoroutine(Explode(4, 4));
    }

    public void ExplodePlus() {
        ExplodeColumn(2);
        ExplodeRow(2);
    }

    public void ExplodeFourSquare(int section) {
        if (section == 0) {
            StartCoroutine(Explode(0, 0));
            StartCoroutine(Explode(0, 1));
            StartCoroutine(Explode(1, 0));
            StartCoroutine(Explode(1, 1));
        } else if (section == 1) {
            StartCoroutine(Explode(0, 3));
            StartCoroutine(Explode(0, 4));
            StartCoroutine(Explode(1, 3));
            StartCoroutine(Explode(1, 4));
        } else if (section == 2) {
            StartCoroutine(Explode(3, 0));
            StartCoroutine(Explode(3, 1));
            StartCoroutine(Explode(4, 0));
            StartCoroutine(Explode(4, 1));
        } else if (section == 3) {
            StartCoroutine(Explode(3, 3));
            StartCoroutine(Explode(3, 4));
            StartCoroutine(Explode(4, 3));
            StartCoroutine(Explode(4, 4));
        }
    }

    /*
     * Trigger the tile at the specified row and column positions. 
     * What this means, is to generate a sprite at that tile, and "explode" that tile.
     * Exploding the tile, means to destroy any unit on that tile.
     */
    private IEnumerator Explode(int row, int col) {
        audioManager.overlordExplosion.Play();
        gameGrid[row][col].GetComponent<BoxCollider2D>().enabled = true;
        gameGrid[row][col].GetComponent<SpriteRenderer>().sortingOrder = 100;
        yield return new WaitForSeconds(explosionCooldown);
        gameGrid[row][col].GetComponent<BoxCollider2D>().enabled = false;
        gameGrid[row][col].GetComponent<SpriteRenderer>().sortingOrder = 0;
    }
}