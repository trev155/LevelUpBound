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

    // Explosion sprite prefabs
    public Transform explosionArchonPrefab;
    public Transform explosionObserverPrefab;
    public Transform explosionReaverPrefab;
    public Transform explosionScoutPrefab;
    public Transform explosionDtPrefab;
    public Transform explosionZealotPrefab;
    public Transform explosionOverlordPrefab;


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
        Transform explosion = InstantiateUnit(explosionCode, gameGrid[row][col].transform);
        yield return new WaitForSeconds(explosionCooldown);
        gameGrid[row][col].GetComponent<BoxCollider2D>().enabled = false;
        Destroy(explosion.gameObject);
    }


    private Transform InstantiateUnit(string code, Transform location) {
        Transform explosion;
        switch (code) {
            case "AR":
                explosion = Instantiate(explosionArchonPrefab, location);
                break;
            case "OB":
                explosion = Instantiate(explosionObserverPrefab, location);
                break;
            case "OBV":
                explosion = Instantiate(explosionObserverPrefab, location);
                Vaccum();
                break;
            case "RV":
                explosion = Instantiate(explosionReaverPrefab, location);
                break;
            case "SC":
                explosion = Instantiate(explosionScoutPrefab, location);
                break;
            case "DT":
                explosion = Instantiate(explosionDtPrefab, location);
                break;
            case "ZL":
                explosion = Instantiate(explosionZealotPrefab, location);
                break;
            case "OV":
                explosion = Instantiate(explosionOverlordPrefab, location);
                break;
            default:
                explosion = Instantiate(explosionArchonPrefab, location);
                break;
        }
        return explosion;
    }

    private void Vaccum() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPosition = player.transform.position;
        if (playerPosition.x > -2.7 && playerPosition.x < 2.7 && playerPosition.y > -2.7 && playerPosition.y < 2.7) {
            player.transform.position = Vector2.MoveTowards(player.transform.position, new Vector2(gameGrid[2][2].transform.position.x, gameGrid[2][2].transform.position.y), Time.deltaTime * 12);
        }
    }
}