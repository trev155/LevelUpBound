/*
 * This class handles the generation and display of our explosions.
 * An explosion involes the instantiation of a prefab, which is essentially just an image sprite.
 * Additionally, the explosion turns the Box Collider of the tile on, killing a Player object touching the tile.
 * A sound effect is also played every time an explosion is generated.
 */

using UnityEngine;
using System.Collections;

public class ExplosionManager : MonoBehaviour {
    // constants
    private const float EXPLOSION_COOLDOWN = 0.1f;
    private const float FADE_OUT_TIME = 0.4f;

    // Reference to the game grid
    public MainGrid mainGrid;
    private GameObject[][] gameGrid;

    // Reference to player
    private Player player;

    // Explosion sprite prefabs
    public Transform explosionArchonPrefab;
    public Transform explosionDarkArchonPrefab;
    public Transform explosionDefilerPrefab;
    public Transform explosionDevourerPrefab;
    public Transform explosionDragoonPrefab;
    public Transform explosionDropshipPrefab;
    public Transform explosionDtempPrefab;
    public Transform explosionFirebatPrefab;
    public Transform explosionGuardianPrefab;
    public Transform explosionHtempPrefab;
    public Transform explosionHydraPrefab;
    public Transform explosionInfestedTerranPrefab;
    public Transform explosionKakaruPrefab;
    public Transform explosionLurkerPrefab;
    public Transform explosionMarinePrefab;
    public Transform explosionMedicPrefab;
    public Transform explosionMutaliskPrefab;
    public Transform explosionObserverPrefab;
    public Transform explosionOverlordPrefab;
    public Transform explosionProbePrefab;
    public Transform explosionQueenPrefab;
    public Transform explosionReaverPrefab;
    public Transform explosionScoutPrefab;
    public Transform explosionScvPrefab;
    public Transform explosionShuttlePrefab;
    public Transform explosionTankPrefab;
    public Transform explosionTankSiegePrefab;
    public Transform explosionUltraliskPrefab;
    public Transform explosionValkPrefab;
    public Transform explosionVesselPrefab;
    public Transform explosionWraithPrefab;
    public Transform explosionZealotPrefab;

    /*
     * Initialization
     */
    void Awake() {
        gameGrid = mainGrid.GetGameGrid();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    /*
     * Wrapper functions for the Explode Coroutine. 
     */
    public void ExplodeTile(int row, int col, string explosionCode) {
        Explode(row, col, explosionCode);
    }

    public void ExplodeRow(int row, string explosionCode) {
        Explode(row, 0, explosionCode);
        Explode(row, 1, explosionCode);
        Explode(row, 2, explosionCode);
        Explode(row, 3, explosionCode);
        Explode(row, 4, explosionCode);
    }

    public void ExplodeColumn(int col, string explosionCode) {
        Explode(0, col, explosionCode);
        Explode(1, col, explosionCode);
        Explode(2, col, explosionCode);
        Explode(3, col, explosionCode);
        Explode(4, col, explosionCode);
    }

    public void ExplodeDiagonalTopLeft(string explosionCode) {
        Explode(4, 0, explosionCode);
        Explode(3, 1, explosionCode);
        Explode(2, 2, explosionCode);
        Explode(1, 3, explosionCode);
        Explode(0, 4, explosionCode);
    }

    public void ExplodeDiagonalBottomLeft(string explosionCode) {
        Explode(0, 0, explosionCode);
        Explode(1, 1, explosionCode);
        Explode(2, 2, explosionCode);
        Explode(3, 3, explosionCode);
        Explode(4, 4, explosionCode);
    }

    public void ExplodePlus(string explosionCode) {
        ExplodeColumn(2, explosionCode);
        ExplodeRow(2, explosionCode);
    }

    public void ExplodeFourSquare(int section, string explosionCode) {
        if (section == 0) {
            Explode(0, 0, explosionCode);
            Explode(0, 1, explosionCode);
            Explode(1, 0, explosionCode);
            Explode(1, 1, explosionCode);
        } else if (section == 1) {
            Explode(0, 3, explosionCode);
            Explode(0, 4, explosionCode);
            Explode(1, 3, explosionCode);
            Explode(1, 4, explosionCode);
        } else if (section == 2) {
            Explode(3, 0, explosionCode);
            Explode(3, 1, explosionCode);
            Explode(4, 0, explosionCode);
            Explode(4, 1, explosionCode);
        } else if (section == 3) {
            Explode(3, 3, explosionCode);
            Explode(3, 4, explosionCode);
            Explode(4, 3, explosionCode);
            Explode(4, 4, explosionCode);
        }
    }

    /*
     * Trigger the tile at the specified row and column positions. 
     * What this means, is to generate a sprite at that tile, and "explode" that tile.
     * Exploding the tile, means to destroy any unit on that tile.
     */
    private void Explode(int row, int col, string explosionCode) {
        Transform explosion = InstantiateUnit(explosionCode, gameGrid[row][col].transform);
        AudioManager.Instance.PlayExplosion(explosionCode);
        StartCoroutine(FadeOutAndDestroy(explosion.gameObject));

        // detect if player in this tile
        float playerWidth = player.transform.lossyScale.x;
        float playerHeight = player.transform.lossyScale.y;
        float playerTop = player.transform.position.y + playerHeight / 2;
        float playerBottom = player.transform.position.y - playerHeight / 2;
        float playerRight = player.transform.position.x + playerWidth / 2;
        float playerLeft = player.transform.position.x - playerWidth / 2;
        // Debug.Log("Player:");
        // Debug.Log("Top: " + playerTop + " Bottom: " + playerBottom + " Right: " + playerRight + " Left: " + playerLeft);

        Transform tileTransform = gameGrid[row][col].transform;
        float tileWidth = tileTransform.lossyScale.x;
        float tileHeight = tileTransform.lossyScale.y;
        float tileTop = tileTransform.position.y + tileHeight / 2;
        float tileBottom = tileTransform.position.y - tileHeight / 2;
        float tileRight = tileTransform.position.x + tileWidth / 2;
        float tileLeft = tileTransform.position.x - tileWidth / 2;
        // Debug.Log("Tile:");
        //Debug.Log("Top: " + tileTop + " Bottom: " + tileBottom + " Right: " + tileRight + " Left: " + tileLeft);

        bool topInTile = playerTop > tileBottom && playerTop < tileTop;
        bool botInTile = playerBottom > tileBottom && playerBottom < tileTop;
        bool rightInTile = playerRight > tileLeft && playerRight < tileRight;
        bool leftInTile = playerLeft > tileLeft && playerLeft < tileRight;
        // Debug.Log("Bools:");
        // Debug.Log("Top: " + topInTile + " Bottom: " + botInTile + " Right: " + rightInTile + " Left: " + leftInTile);

        if ((topInTile && rightInTile) || (topInTile && leftInTile) || (botInTile && rightInTile) || (botInTile && leftInTile)) {
            player.Death();
        }
    }

    /*
     * Given a Game object, fade out the object by reducing its alpha property repeatedly.
     * After its alpha hits 0, destroy the game object.
     */
    IEnumerator FadeOutAndDestroy(GameObject explosionGameObject) {
        SpriteRenderer spriteRenderer = explosionGameObject.GetComponent<SpriteRenderer>();

        Color tmpColor = spriteRenderer.color;
        while (tmpColor.a > 0f) {
            tmpColor.a -= Time.deltaTime / FADE_OUT_TIME;
            spriteRenderer.color = tmpColor;
            if (tmpColor.a <= 0f) {
                tmpColor.a = 0f;
            }
            yield return null;
        }
        spriteRenderer.color = tmpColor;
        Destroy(explosionGameObject);
    }

    /*
     * Instantiate a prefab of the unit corresponding to the string code at specified location.
     */
    private Transform InstantiateUnit(string code, Transform location) {
        Transform explosion;
        switch (code) {
            case "AR":
                explosion = Instantiate(explosionArchonPrefab, location);
                break;
            case "DA":
                explosion = Instantiate(explosionDarkArchonPrefab, location);
                break;
            case "DF":
                explosion = Instantiate(explosionDefilerPrefab, location);
                break;
            case "DV":
                explosion = Instantiate(explosionDevourerPrefab, location);
                break;
            case "DG":
                explosion = Instantiate(explosionDragoonPrefab, location);
                break;
            case "DS":
                explosion = Instantiate(explosionDropshipPrefab, location);
                break;
            case "DT":
                explosion = Instantiate(explosionDtempPrefab, location);
                break;
            case "FB":
                explosion = Instantiate(explosionFirebatPrefab, location);
                break;
            case "GD":
                explosion = Instantiate(explosionGuardianPrefab, location);
                break;
            case "HT":
                explosion = Instantiate(explosionHtempPrefab, location);
                break;
            case "HY":
                explosion = Instantiate(explosionHydraPrefab, location);
                break;
            case "IT":
                explosion = Instantiate(explosionInfestedTerranPrefab, location);
                break;
            case "KA":
                explosion = Instantiate(explosionKakaruPrefab, location);
                break;
            case "LU":
                explosion = Instantiate(explosionLurkerPrefab, location);
                break;
            case "MA":
                explosion = Instantiate(explosionMarinePrefab, location);
                break;
            case "ME":
                explosion = Instantiate(explosionMedicPrefab, location);
                break;
            case "MU":
                explosion = Instantiate(explosionMutaliskPrefab, location);
                break;
            case "OB":
                explosion = Instantiate(explosionObserverPrefab, location);
                break;
            case "OBV":
                explosion = Instantiate(explosionObserverPrefab, location);
                Vaccum();
                break;
            case "OV":
                explosion = Instantiate(explosionOverlordPrefab, location);
                break;
            case "PB":
                explosion = Instantiate(explosionProbePrefab, location);
                break;
            case "QU":
                explosion = Instantiate(explosionQueenPrefab, location);
                break;
            case "RV":
                explosion = Instantiate(explosionReaverPrefab, location);
                break;
            case "SC":
                explosion = Instantiate(explosionScoutPrefab, location);
                break;
            case "SV":
                explosion = Instantiate(explosionScvPrefab, location);
                break;
            case "SH":
                explosion = Instantiate(explosionShuttlePrefab, location);
                break;
            case "TK":
                explosion = Instantiate(explosionTankPrefab, location);
                break;
            case "TS":
                explosion = Instantiate(explosionTankSiegePrefab, location);
                break;
            case "UL":
                explosion = Instantiate(explosionUltraliskPrefab, location);
                break;
            case "VK":
                explosion = Instantiate(explosionValkPrefab, location);
                break;
            case "VS":
                explosion = Instantiate(explosionVesselPrefab, location);
                break;
            case "WR":
                explosion = Instantiate(explosionWraithPrefab, location);
                break;
            case "ZL":
                explosion = Instantiate(explosionZealotPrefab, location);
                break;
            default:
                explosion = Instantiate(explosionArchonPrefab, location);
                break;
        }
        return explosion;
    }

    /*
     * Vaccum feature, first pass. 
     * When this is called, find the Player and pull it towards the middle tile.
     */
    private void Vaccum() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPosition = player.transform.position;
        if (playerPosition.x > -2.7 && playerPosition.x < 2.7 && playerPosition.y > -2.7 && playerPosition.y < 2.7) {
            player.transform.position = Vector2.MoveTowards(player.transform.position, new Vector2(gameGrid[2][2].transform.position.x, gameGrid[2][2].transform.position.y), Time.deltaTime * 12);
        }
    }
}