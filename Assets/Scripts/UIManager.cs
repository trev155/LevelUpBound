using UnityEngine;

public class UIManager : MonoBehaviour {
    public void BackButtonPressed() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
