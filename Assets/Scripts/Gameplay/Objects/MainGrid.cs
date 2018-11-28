/*
 * The MainGrid is the 5x5 grid in the main game scene.
 */
using UnityEngine;

public class MainGrid : MonoBehaviour {
    private const int DIMENSION = 5;
    private GameObject[][] gameGrid;
    
    void Awake() {
        InitializeGameGrid();
        PopulateGameGrid();
    }

    private void InitializeGameGrid() {
        gameGrid = new GameObject[DIMENSION][];
        for (int row = 0; row < DIMENSION; row++) {
            gameGrid[row] = new GameObject[DIMENSION];
        }
    }

    private void PopulateGameGrid() {
        int childIndex = 0;
        for (int row = 0; row < DIMENSION; row++) {
            for (int col = 0; col < DIMENSION; col++) {
                gameGrid[row][col] = this.transform.GetChild(childIndex).gameObject;
                childIndex++;
            }
        }
    }

    public GameObject[][] GetGameGrid() {
        return gameGrid;
    }
}
