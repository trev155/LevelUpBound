using UnityEngine;

/*
 * An ExplodableTile is a prefab with a box collider.
 * When the Player collides with this object, this indicates a Player death.
 */
public class ExplodableTile : MonoBehaviour {
    public Player player;

    void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player") {
            player.Death();
        }
    }
}