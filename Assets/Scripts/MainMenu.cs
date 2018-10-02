using UnityEngine;

public class MainMenu : MonoBehaviour {
    private void ChooseMode(string mode) {
        GameContext.GameMode = mode;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }

    public void ChooseClassicMode() {
        ChooseMode("classic");
    }

    public void ChooseCustomMode() {
        ChooseMode("custom");
    }
}
