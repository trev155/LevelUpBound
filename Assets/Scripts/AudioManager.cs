using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioSource archonExplosion;
    public AudioSource observerExplosion;
    public AudioSource reaverExplosion;
    public AudioSource scoutExplosion;
    public AudioSource darktempExplosion;
    public AudioSource zealotExplosion;
    public AudioSource overlordExplosion;

    private void Awake() {
        // Order matters in the Hierarchy.
        // Children (Depth 1): 0 = Explosion
        // Children (Depth 2): 0 = archon (AR), 1 = observer (OB), 2 = reaver (RV), 3 = scout (SC), 4 = dark templar (DT),
        // 5 = zealot (ZL), 6 = overlord (OV)
        archonExplosion = this.transform.GetChild(0).GetChild(0).GetComponent<AudioSource>();
        observerExplosion = this.transform.GetChild(0).GetChild(1).GetComponent<AudioSource>();
        reaverExplosion = this.transform.GetChild(0).GetChild(2).GetComponent<AudioSource>();
        scoutExplosion = this.transform.GetChild(0).GetChild(3).GetComponent<AudioSource>();
        darktempExplosion = this.transform.GetChild(0).GetChild(4).GetComponent<AudioSource>();
        zealotExplosion = this.transform.GetChild(0).GetChild(5).GetComponent<AudioSource>();
        overlordExplosion = this.transform.GetChild(0).GetChild(6).GetComponent<AudioSource>();
    }

    public void PlaySoundEffect(string code) {
        switch (code) {
            case "AR":
                archonExplosion.Play();
                break;
            case "OB":
                observerExplosion.Play();
                break;
            case "RV":
                reaverExplosion.Play();
                break;
            case "SC":
                scoutExplosion.Play();
                break;
            case "DT":
                darktempExplosion.Play();
                break;
            case "ZL":
                zealotExplosion.Play();
                break;
            case "OV":
                overlordExplosion.Play();
                break;
            default:
                Debug.Log("Unrecognized audio code. This is not fatal.");
                break;
        }
    }
}
