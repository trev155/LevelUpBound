/*
 * This class handles the generation and display of our explosions.
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionManager : MonoBehaviour {
    private const float EXPLOSION_FADE_FAST_TIME = 0.3f;
    private const float EXPLOSION_FADE_NORMAL_TIME = 0.7f;
    private const float EXPLOSION_FADE_SLOW_TIME = 1.5f;
    private const float EXPLOSION_FADE_VERY_SLOW_TIME = 3.0f;

    List<string> explosionsToFadeOutFast = new List<string> {
        "DT", "FB", "HT", "KA", "MA", "MU", "PB", "ZL"
    };
    List<string> explosionsToFadeOutSlow = new List<string> {
        "DG", "HY", "IT", "TK", "TS"
    };
    List<string> explosionsToFadeOutVerySlow = new List<string> {
        "DF", "LU", "ME", "UL"
    };

    public MainGrid mainGrid;
    private GameObject[][] gameGrid;
    private Player player;

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

    void Awake() {
        gameGrid = mainGrid.GetGameGrid();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void ExplodeRow(int row, string explosionCode) {
        ExplodeTile(row, 0, explosionCode);
        ExplodeTile(row, 1, explosionCode);
        ExplodeTile(row, 2, explosionCode);
        ExplodeTile(row, 3, explosionCode);
        ExplodeTile(row, 4, explosionCode);
    }

    public void ExplodeColumn(int col, string explosionCode) {
        ExplodeTile(0, col, explosionCode);
        ExplodeTile(1, col, explosionCode);
        ExplodeTile(2, col, explosionCode);
        ExplodeTile(3, col, explosionCode);
        ExplodeTile(4, col, explosionCode);
    }

    public void ExplodeDiagonalTopLeft(string explosionCode) {
        ExplodeTile(4, 0, explosionCode);
        ExplodeTile(3, 1, explosionCode);
        ExplodeTile(2, 2, explosionCode);
        ExplodeTile(1, 3, explosionCode);
        ExplodeTile(0, 4, explosionCode);
    }

    public void ExplodeDiagonalBottomLeft(string explosionCode) {
        ExplodeTile(0, 0, explosionCode);
        ExplodeTile(1, 1, explosionCode);
        ExplodeTile(2, 2, explosionCode);
        ExplodeTile(3, 3, explosionCode);
        ExplodeTile(4, 4, explosionCode);
    }

    public void ExplodePlus(string explosionCode) {
        ExplodeColumn(2, explosionCode);
        ExplodeRow(2, explosionCode);
    }

    public void ExplodeFourSquare(int section, string explosionCode) {
        if (section == 0) {
            ExplodeTile(0, 0, explosionCode);
            ExplodeTile(0, 1, explosionCode);
            ExplodeTile(1, 0, explosionCode);
            ExplodeTile(1, 1, explosionCode);
        } else if (section == 1) {
            ExplodeTile(0, 3, explosionCode);
            ExplodeTile(0, 4, explosionCode);
            ExplodeTile(1, 3, explosionCode);
            ExplodeTile(1, 4, explosionCode);
        } else if (section == 2) {
            ExplodeTile(3, 0, explosionCode);
            ExplodeTile(3, 1, explosionCode);
            ExplodeTile(4, 0, explosionCode);
            ExplodeTile(4, 1, explosionCode);
        } else if (section == 3) {
            ExplodeTile(3, 3, explosionCode);
            ExplodeTile(3, 4, explosionCode);
            ExplodeTile(4, 3, explosionCode);
            ExplodeTile(4, 4, explosionCode);
        }
    }

    public void ExplodeTile(int row, int col, string explosionCode) {
        AudioManager.Instance.PlayExplosion(explosionCode);
        Transform explosion = InstantiateUnit(explosionCode, gameGrid[row][col].transform);
        StartCoroutine(FadeOutAndDestroy(explosion.gameObject, explosionCode));
        
        if (PlayerIsTouchingTile(row, col)) {
            player.Death();
        }
    }

    private bool PlayerIsTouchingTile(int tileRow, int tileCol) {
        float playerWidth = player.transform.lossyScale.x;
        float playerHeight = player.transform.lossyScale.y;
        float playerTop = player.transform.position.y + playerHeight / 2;
        float playerBottom = player.transform.position.y - playerHeight / 2;
        float playerRight = player.transform.position.x + playerWidth / 2;
        float playerLeft = player.transform.position.x - playerWidth / 2;

        Transform tileTransform = gameGrid[tileRow][tileCol].transform;
        float tileWidth = tileTransform.lossyScale.x;
        float tileHeight = tileTransform.lossyScale.y;
        float tileTop = tileTransform.position.y + tileHeight / 2;
        float tileBottom = tileTransform.position.y - tileHeight / 2;
        float tileRight = tileTransform.position.x + tileWidth / 2;
        float tileLeft = tileTransform.position.x - tileWidth / 2;

        bool topInTile = playerTop > tileBottom && playerTop < tileTop;
        bool botInTile = playerBottom > tileBottom && playerBottom < tileTop;
        bool rightInTile = playerRight > tileLeft && playerRight < tileRight;
        bool leftInTile = playerLeft > tileLeft && playerLeft < tileRight;

        return (topInTile && rightInTile) || (topInTile && leftInTile) || (botInTile && rightInTile) || (botInTile && leftInTile);
    }

    private IEnumerator FadeOutAndDestroy(GameObject objectToDestroy, string explosionCode) {
        float explosionFadeTime = GetExplosionFadeTime(explosionCode);
        SpriteRenderer objectSpriteRenderer = objectToDestroy.GetComponent<SpriteRenderer>();
        Color objectColor = objectSpriteRenderer.color;
        while (objectColor.a > 0f) {
            objectColor.a -= Time.deltaTime / explosionFadeTime;
            objectSpriteRenderer.color = objectColor;
            if (objectColor.a <= 0f) {
                objectColor.a = 0f;
            }
            yield return null;
        }
        objectSpriteRenderer.color = objectColor;
        Destroy(objectToDestroy);
    }

    private float GetExplosionFadeTime(string explosionCode) {
        float explosionFadeTime;
        if (explosionsToFadeOutFast.Contains(explosionCode)) {
            explosionFadeTime = EXPLOSION_FADE_FAST_TIME;
        } else if (explosionsToFadeOutSlow.Contains(explosionCode)) {
            explosionFadeTime = EXPLOSION_FADE_SLOW_TIME;
        } else if (explosionsToFadeOutVerySlow.Contains(explosionCode)) {
            explosionFadeTime = EXPLOSION_FADE_VERY_SLOW_TIME;
        } else {
            explosionFadeTime = EXPLOSION_FADE_NORMAL_TIME;
        }
        return explosionFadeTime;
    }

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

    private void Vaccum() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPosition = player.transform.position;
        if (PlayerIsInGameGridBounds(playerPosition)) {
            MovePlayerTowardsCenter();
        }
    }

    private bool PlayerIsInGameGridBounds(Vector3 playerPosition) {
        return playerPosition.x > GetLeftBound() && playerPosition.x < GetRightBound() && playerPosition.y > GetBottomBound() && playerPosition.y < GetTopBound();
    }

    private float GetBottomBound() {
        Transform bottomTile = GameObject.Find("0,0").GetComponent<Transform>();
        return bottomTile.position.y - (bottomTile.lossyScale.y / 2);
    } 
    
    private float GetTopBound() {
        Transform topTile = GameObject.Find("4,4").GetComponent<Transform>();
        return topTile.position.y + (topTile.lossyScale.y / 2);
    }

    private float GetLeftBound() {
        Transform leftTile = GameObject.Find("0,0").GetComponent<Transform>();
        return leftTile.position.x - (leftTile.lossyScale.x / 2);
    }

    private float GetRightBound() {
        Transform rightTile = GameObject.Find("4,4").GetComponent<Transform>();
        return rightTile.position.x + (rightTile.lossyScale.x / 2);
    }

    private void MovePlayerTowardsCenter() {
        player.transform.position = Vector2.MoveTowards(player.transform.position, new Vector2(gameGrid[2][2].transform.position.x, gameGrid[2][2].transform.position.y), Time.deltaTime * 12);
    }
}