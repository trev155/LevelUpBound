using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    private Text currentLevelText;

    private void Awake() {
        currentLevelText = GameObject.Find("CurrentLevelLabel").GetComponent<Text>();
    }

    public void BackButtonPressed() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void UpdateLevelText() {
        currentLevelText.text = "Current Level: " + GameContext.CurrentLevel;
    }
}
