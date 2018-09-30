using UnityEngine;

public class MainGrid : MonoBehaviour {
    // The data structure that will hold all the grid locations in the level
    private GameObject[][] gameGrid;
    
    // Size of the square grid
    private const int DIMENSION = 5;


    /*
     * Initialize the gameGrid array.
     * The gameGrid is an array of arrays. 
     * gameGrid[row][col] refers to a specific tile on the game grid.
     */
    void Awake() {
        // Initialize the gameGrid array
        gameGrid = new GameObject[DIMENSION][];
        for (int i = 0; i < DIMENSION; i++) {
            gameGrid[i] = new GameObject[DIMENSION];
        }

        // Populate the gameGrid array
        int childIndex = 0;
        for (int i = 0; i < DIMENSION; i++) {
            for (int j = 0; j < DIMENSION; j++) {
                gameGrid[i][j] = this.transform.GetChild(childIndex).gameObject;
                childIndex++;
            }
        }
    }

    /*
     * Public getter for the gameGrid.
     */
    public GameObject[][] GetGameGrid() {
        return gameGrid;
    }
}
