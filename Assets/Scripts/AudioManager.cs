using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioSource overlordExplosion;
    public AudioSource observerExplosion;

    private void Awake() {
        // Order matters in the Hierarchy.
        // Children (Depth 1): 0 = Explosion
        // Children (Depth 2): 0 = overlord, 1 = observer
        overlordExplosion = this.transform.GetChild(0).GetChild(0).GetComponent<AudioSource>();
        observerExplosion = this.transform.GetChild(0).GetChild(1).GetComponent<AudioSource>();
    }
}
