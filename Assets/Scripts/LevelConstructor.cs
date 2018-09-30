using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class LevelConstructor : MonoBehaviour {
    // Script that manages generating the explosions
    public ExplosionManager em;
    // Reference to the game grid
    public MainGrid mainGrid;
    private GameObject[][] gameGrid;
    // Wall prefab
    public Transform wallPrefab;


    /*
     * Initialization.
     */
    void Awake() {
        gameGrid = mainGrid.GetGameGrid();
    }

    /*
     * Load a level coroutine from a given filepath.
     * The filepath points to a file containing level directives. We use this data to construct a Level coroutine.
     */
    public IEnumerator LoadLevelFromFilepath(string levelFilepath) {
        StreamReader inputStream = new StreamReader(levelFilepath);
        List<string> commands = new List<string>();
        while (!inputStream.EndOfStream) {
            string line = inputStream.ReadLine();
            commands.Add(line);
        }
        inputStream.Close();

        IEnumerator level = BuildLevel(commands);
        return level;
    }

    /*
     * The LevelConstructor is responsible for creating a Level. 
     * It returns a Coroutine, which loops an obstacle sequence.
     * The Level is constructed by reading a list of string commands.
     * These commands are all pre-formatted so they can be interpreted properly.
     */
    private IEnumerator BuildLevel(List<string> commands) {
        if (commands.Count == 0) {
            Debug.Log("Command list was empty, stop.");
            yield break;
        }

        while (true) {
            foreach (string line in commands) {
                if (line.StartsWith("R")) {
                    int row = int.Parse(line.Substring(1, 1));
                    em.ExplodeRow(row);
                } else if (line.StartsWith("C")) {
                    int col = int.Parse(line.Substring(1, 1));
                    em.ExplodeColumn(col);
                } else if (line.StartsWith("FS")) {
                    int square = int.Parse(line.Substring(2, 1));
                    em.ExplodeFourSquare(square);
                } else if (line.StartsWith("PLUS")) {
                    em.ExplodePlus();
                } else if (line.StartsWith("DIAG0")) {
                    em.ExplodeDiagonalBottomLeft();
                } else if (line.StartsWith("DIAG1")) {
                    em.ExplodeTileDiagonalTopLeft();
                } else if (line.StartsWith("T")) {
                    int row = int.Parse(line.Substring(1, 1));
                    int col = int.Parse(line.Substring(3, 1));
                    em.ExplodeTile(row, col);
                } else if (line.StartsWith("W")) {
                    float time = int.Parse(line.Substring(1)) / 1000.0f;
                    yield return new WaitForSeconds(time);
                } else {
                    Debug.Log("Unrecognizable command. This shouldn't happen, but is not fatal.");
                }
            }
        }
    }

    /*
     * For some levels, there are external objects that we need to implement.
     * Specifically - walls and keys.
     * 
     * Read this data from an external file.
     */
    public void LoadExternalObjects(string externalsFilepath) {
        StreamReader inputStream = new StreamReader(externalsFilepath);
        List<string> lines = new List<string>();
        while (!inputStream.EndOfStream) {
            string line = inputStream.ReadLine();
            lines.Add(line);
        }
        inputStream.Close();

        ConstructExternalObjects(lines);
    }

    /*
     * Construct external objects.
     */
    public void ConstructExternalObjects(List<string> data) {
        foreach (string line in data) {
            if (line.StartsWith("W")) {
                int wallRow = int.Parse(line.Substring(1, 1));
                int wallCol = int.Parse(line.Substring(3, 1));
                Instantiate(wallPrefab, gameGrid[wallRow][wallCol].transform);
            }
        }
    }

    /*
     * Remove all external objects.
     */
     public void RemoveExternalObjects() {
        GameObject[] gameObjectsToRemove;
        gameObjectsToRemove = GameObject.FindGameObjectsWithTag("External");
        foreach (GameObject obj in gameObjectsToRemove) {
            Destroy(obj);
        }
    }   
}