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
    public void ExplodeTile(int row, int col, string explosionCode) {
        StartCoroutine(Explode(row, col, explosionCode));
    }

    public void ExplodeRow(int row, string explosionCode) {
        StartCoroutine(Explode(row, 0, explosionCode));
        StartCoroutine(Explode(row, 1, explosionCode));
        StartCoroutine(Explode(row, 2, explosionCode));
        StartCoroutine(Explode(row, 3, explosionCode));
        StartCoroutine(Explode(row, 4, explosionCode));
    }

    public void ExplodeColumn(int col, string explosionCode) {
        StartCoroutine(Explode(0, col, explosionCode));
        StartCoroutine(Explode(1, col, explosionCode));
        StartCoroutine(Explode(2, col, explosionCode));
        StartCoroutine(Explode(3, col, explosionCode));
        StartCoroutine(Explode(4, col, explosionCode));
    }

    public void ExplodeDiagonalBottomLeft(string explosionCode) {
        StartCoroutine(Explode(4, 0, explosionCode));
        StartCoroutine(Explode(3, 1, explosionCode));
        StartCoroutine(Explode(2, 2, explosionCode));
        StartCoroutine(Explode(1, 3, explosionCode));
        StartCoroutine(Explode(0, 4, explosionCode));
    }

    public void ExplodeTileDiagonalTopLeft(string explosionCode) {
        StartCoroutine(Explode(0, 0, explosionCode));
        StartCoroutine(Explode(1, 1, explosionCode));
        StartCoroutine(Explode(2, 2, explosionCode));
        StartCoroutine(Explode(3, 3, explosionCode));
        StartCoroutine(Explode(4, 4, explosionCode));
    }

    public void ExplodePlus(string explosionCode) {
        ExplodeColumn(2, explosionCode);
        ExplodeRow(2, explosionCode);
    }

    public void ExplodeFourSquare(int section, string explosionCode) {
        if (section == 0) {
            StartCoroutine(Explode(0, 0, explosionCode));
            StartCoroutine(Explode(0, 1, explosionCode));
            StartCoroutine(Explode(1, 0, explosionCode));
            StartCoroutine(Explode(1, 1, explosionCode));
        } else if (section == 1) {
            StartCoroutine(Explode(0, 3, explosionCode));
            StartCoroutine(Explode(0, 4, explosionCode));
            StartCoroutine(Explode(1, 3, explosionCode));
            StartCoroutine(Explode(1, 4, explosionCode));
        } else if (section == 2) {
            StartCoroutine(Explode(3, 0, explosionCode));
            StartCoroutine(Explode(3, 1, explosionCode));
            StartCoroutine(Explode(4, 0, explosionCode));
            StartCoroutine(Explode(4, 1, explosionCode));
        } else if (section == 3) {
            StartCoroutine(Explode(3, 3, explosionCode));
            StartCoroutine(Explode(3, 4, explosionCode));
            StartCoroutine(Explode(4, 3, explosionCode));
            StartCoroutine(Explode(4, 4, explosionCode));
        }
    }

    /*
     * Trigger the tile at the specified row and column positions. 
     * What this means, is to generate a sprite at that tile, and "explode" that tile.
     * Exploding the tile, means to destroy any unit on that tile.
     */
    private IEnumerator Explode(int row, int col, string explosionCode) {
        audioManager.PlaySoundEffect(explosionCode);
        gameGrid[row][col].GetComponent<BoxCollider2D>().enabled = true;
        gameGrid[row][col].GetComponent<SpriteRenderer>().sortingOrder = 100;
        yield return new WaitForSeconds(explosionCooldown);
        gameGrid[row][col].GetComponent<BoxCollider2D>().enabled = false;
        gameGrid[row][col].GetComponent<SpriteRenderer>().sortingOrder = 0;
    }
}