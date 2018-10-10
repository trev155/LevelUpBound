using UnityEngine;

public class ExplodableTile : MonoBehaviour {
    public Transform RespawnPoint;

    void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player") {
            other.transform.position = RespawnPoint.position;
        }
    }
}