/*
 * The LevelConstructor is responsible for reading files and constructing a level coroutine based on the file contents.
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelConstructor : MonoBehaviour {
    public ExplosionManager em;
    public MainGrid mainGrid;
    private GameObject[][] gameGrid;
    
    public Transform wallPrefab;
    public Transform lockedWallPrefab;
    public Transform doubleLockedWallPrefab;
    public Transform smallWallBottomLeftPrefab;
    public Transform smallWallBottomRightPrefab;
    public Transform smallWallTopLeftPrefab;
    public Transform smallWallTopRightPrefab;
    public Transform movingObjectPrefab;
    public Transform keyPrefab;

    void Awake() {
        gameGrid = mainGrid.GetGameGrid();
    }

    public IEnumerator LoadLevelFromFilepath(string levelFilepath) {
        List<string> commands = new List<string>();
        TextAsset levelTextAsset = Resources.Load<TextAsset>(levelFilepath);
        string[] levelLines = levelTextAsset.text.Split('\n');
        foreach (string line in levelLines) {
            commands.Add(line.Trim());
        }

        IEnumerator level = BuildLevel(commands);
        return level;
    }

    /*
     * Creates a looping Coroutine, which represents a level.
     */
    private IEnumerator BuildLevel(List<string> commands) {
        if (commands.Count == 0) {
            Debug.Log("Command list was empty, stop.");
            yield break;
        }

        while (true) {
            foreach (string line in commands) {
                string[] lineSplitBySpace = line.Split(' ');
                string explosionCode = "";
                if (lineSplitBySpace.Length > 1) {
                    explosionCode = lineSplitBySpace[1];
                }

                if (line.StartsWith("R")) {
                    int row = int.Parse(line.Substring(1, 1));
                    em.ExplodeRow(row, explosionCode);
                } else if (line.StartsWith("C")) {
                    int col = int.Parse(line.Substring(1, 1));
                    em.ExplodeColumn(col, explosionCode);
                } else if (line.StartsWith("FS")) {
                    int square = int.Parse(line.Substring(2, 1));
                    em.ExplodeFourSquare(square, explosionCode);
                } else if (line.StartsWith("PLUS")) {
                    em.ExplodePlus(explosionCode);
                } else if (line.StartsWith("DIAG0")) {
                    em.ExplodeDiagonalBottomLeft(explosionCode);
                } else if (line.StartsWith("DIAG1")) {
                    em.ExplodeDiagonalTopLeft(explosionCode);
                } else if (line.StartsWith("T")) {
                    int row = int.Parse(line.Substring(1, 1));
                    int col = int.Parse(line.Substring(3, 1));
                    em.ExplodeTile(row, col, explosionCode);
                } else if (line.StartsWith("W")) {
                    float time = int.Parse(line.Substring(1)) / 1000.0f;
                    yield return new WaitForSeconds(time);
                } else {
                    Debug.Log("Unrecognizable command. This shouldn't happen, but is not fatal.");
                }
            }
        }
    }

    public void LoadExternalObjects(string externalsFilepath) {
        List<string> lines = new List<string>();
        TextAsset externalsTextAsset = Resources.Load<TextAsset>(externalsFilepath);
        string[] externalsList = externalsTextAsset.text.Split('\n');
        foreach (string line in externalsList) {
            lines.Add(line.Trim());
        }

        ConstructExternalObjects(lines);
    }

    public void ConstructExternalObjects(List<string> data) {
        foreach (string line in data) {
            int wallRow; 
            int wallCol;
            if (line.StartsWith("W")) {
                wallRow = int.Parse(line.Substring(1, 1));
                wallCol = int.Parse(line.Substring(3, 1));
                Instantiate(wallPrefab, gameGrid[wallRow][wallCol].transform);
            } else if (line.StartsWith("LW")) {
                wallRow = int.Parse(line.Substring(2, 1));
                wallCol = int.Parse(line.Substring(4, 1));
                Instantiate(lockedWallPrefab, gameGrid[wallRow][wallCol].transform);
            } else if (line.StartsWith("DLW")) {
                wallRow = int.Parse(line.Substring(3, 1));
                wallCol = int.Parse(line.Substring(5, 1));
                Instantiate(doubleLockedWallPrefab, gameGrid[wallRow][wallCol].transform);
            } else if (line.StartsWith("SWBL")) {
                wallRow = int.Parse(line.Substring(4, 1));
                wallCol = int.Parse(line.Substring(6, 1));
                Instantiate(smallWallBottomLeftPrefab, gameGrid[wallRow][wallCol].transform);
            } else if (line.StartsWith("SWBR")) {
                wallRow = int.Parse(line.Substring(4, 1));
                wallCol = int.Parse(line.Substring(6, 1));
                Instantiate(smallWallBottomRightPrefab, gameGrid[wallRow][wallCol].transform);
            } else if (line.StartsWith("SWTL")) {
                wallRow = int.Parse(line.Substring(4, 1));
                wallCol = int.Parse(line.Substring(6, 1));
                Instantiate(smallWallTopLeftPrefab, gameGrid[wallRow][wallCol].transform);
            } else if (line.StartsWith("SWTR")) {
                wallRow = int.Parse(line.Substring(4, 1));
                wallCol = int.Parse(line.Substring(6, 1));
                Instantiate(smallWallTopRightPrefab, gameGrid[wallRow][wallCol].transform);
            } else if (line.StartsWith("MV")) {
                string[] tokens = line.Split(' ');
                Transform obj = Instantiate(movingObjectPrefab, gameGrid[int.Parse(tokens[1].Substring(0, 1))][int.Parse(tokens[1].Substring(2, 1))].transform);
                MovingObject m = obj.gameObject.GetComponent<MovingObject>();
                string[] waypoints = new string[] { tokens[1], tokens[2], tokens[3], tokens[4] };
                m.SetWaypoints(waypoints);
                m.StartMovement();
            } else if (line.StartsWith("K")) {
                int row = int.Parse(line.Substring(1, 1));
                int col = int.Parse(line.Substring(3, 1));
                wallRow = int.Parse(line.Substring(5, 1));
                wallCol = int.Parse(line.Substring(7, 1));
                Transform obj = Instantiate(keyPrefab, gameGrid[row][col].transform);
                Key key = obj.gameObject.GetComponent<Key>();
                key.SetWallCoordinates(wallRow, wallCol);
            }
        }
    }

     public void RemoveExternalObjectsFromScene() {
        GameObject[] gameObjectsToRemove = GameObject.FindGameObjectsWithTag("External");
        foreach (GameObject obj in gameObjectsToRemove) {
            Destroy(obj);
        }
    }   
}