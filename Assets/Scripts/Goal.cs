using UnityEngine;

public class Goal : MonoBehaviour {
    public LevelManager levelManager;
    public Transform RespawnPoint;
    private UIManager UImanager;

    private void Awake() {
        UImanager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            other.transform.position = RespawnPoint.position;
            levelManager.AdvanceLevel();
            UImanager.UpdateLevelText();
        }
    }
}

