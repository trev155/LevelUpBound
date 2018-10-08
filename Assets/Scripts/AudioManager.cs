using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioSource archonExplosion;
    public AudioSource defilerExplosion;
    public AudioSource devourerExplosion;
    public AudioSource dragoonExplosion;
    public AudioSource dropshipExplosion;
    public AudioSource dtempExplosion;
    public AudioSource firebatExplosion;
    public AudioSource guardianExplosion;
    public AudioSource htempExplosion;
    public AudioSource hydraExplosion;
    public AudioSource lurkerExplosion;
    public AudioSource marineExplosion;
    public AudioSource medicExplosion;
    public AudioSource observerExplosion;
    public AudioSource overlordExplosion;
    public AudioSource probeExplosion;
    public AudioSource queenExplosion;
    public AudioSource reaverExplosion;
    public AudioSource scoutExplosion;
    public AudioSource scvExplosion;
    public AudioSource shuttleExplosion;
    public AudioSource tankExplosion;
    public AudioSource tankSiegeExplosion;
    public AudioSource ultraliskExplosion;
    public AudioSource valkExplosion;
    public AudioSource vesselExplosion;
    public AudioSource wraithExplosion;
    public AudioSource zealotExplosion;
    
    
    private void Awake() {
        archonExplosion = this.transform.GetChild(0).Find("Archon").GetComponent<AudioSource>();
        defilerExplosion = this.transform.GetChild(0).Find("Defiler").GetComponent<AudioSource>();
        devourerExplosion = this.transform.GetChild(0).Find("Devourer").GetComponent<AudioSource>();
        dragoonExplosion = this.transform.GetChild(0).Find("Dragoon").GetComponent<AudioSource>();
        dropshipExplosion = this.transform.GetChild(0).Find("Dropship").GetComponent<AudioSource>();
        dtempExplosion = this.transform.GetChild(0).Find("DarkTemplar").GetComponent<AudioSource>();
        firebatExplosion = this.transform.GetChild(0).Find("Firebat").GetComponent<AudioSource>();
        guardianExplosion = this.transform.GetChild(0).Find("Guardian").GetComponent<AudioSource>();
        htempExplosion = this.transform.GetChild(0).Find("Htemp").GetComponent<AudioSource>();
        hydraExplosion = this.transform.GetChild(0).Find("Hydra").GetComponent<AudioSource>();
        lurkerExplosion = this.transform.GetChild(0).Find("Lurker").GetComponent<AudioSource>();
        marineExplosion = this.transform.GetChild(0).Find("Marine").GetComponent<AudioSource>();
        medicExplosion = this.transform.GetChild(0).Find("Marine").GetComponent<AudioSource>();
        observerExplosion = this.transform.GetChild(0).Find("Observer").GetComponent<AudioSource>();
        overlordExplosion = this.transform.GetChild(0).Find("Overlord").GetComponent<AudioSource>();
        probeExplosion = this.transform.GetChild(0).Find("Probe").GetComponent<AudioSource>();
        queenExplosion = this.transform.GetChild(0).Find("Queen").GetComponent<AudioSource>();
        reaverExplosion = this.transform.GetChild(0).Find("Reaver").GetComponent<AudioSource>();
        scoutExplosion = this.transform.GetChild(0).Find("Scout").GetComponent<AudioSource>();
        scvExplosion = this.transform.GetChild(0).Find("Scv").GetComponent<AudioSource>();
        shuttleExplosion = this.transform.GetChild(0).Find("Shuttle").GetComponent<AudioSource>();
        tankExplosion = this.transform.GetChild(0).Find("Tank").GetComponent<AudioSource>();
        tankSiegeExplosion = this.transform.GetChild(0).Find("TankSiege").GetComponent<AudioSource>();
        ultraliskExplosion = this.transform.GetChild(0).Find("Ultralisk").GetComponent<AudioSource>();
        valkExplosion = this.transform.GetChild(0).Find("Valk").GetComponent<AudioSource>();
        vesselExplosion = this.transform.GetChild(0).Find("Vessel").GetComponent<AudioSource>();
        wraithExplosion = this.transform.GetChild(0).Find("Wraith").GetComponent<AudioSource>();
        zealotExplosion = this.transform.GetChild(0).Find("Zealot").GetComponent<AudioSource>();
    }

    public void PlaySoundEffect(string code) {
        switch (code) {
            case "AR":
                archonExplosion.Play();
                break;
            case "DA":
                archonExplosion.Play();
                break;
            case "DF":
                defilerExplosion.Play();
                break;
            case "DV":
                devourerExplosion.Play();
                break;
            case "DG":
                dragoonExplosion.Play();
                break;
            case "DS":
                dropshipExplosion.Play();
                break;
            case "DT":
                dtempExplosion.Play();
                break;
            case "FB":
                firebatExplosion.Play();
                break;
            case "GD":
                guardianExplosion.Play();
                break;
            case "HT":
                htempExplosion.Play();
                break;
            case "HY":
                hydraExplosion.Play();
                break;
            case "LU":
                lurkerExplosion.Play();
                break;
            case "MA":
                marineExplosion.Play();
                break;
            case "ME":
                medicExplosion.Play();
                break;
            case "OB":
                observerExplosion.Play();
                break;
            case "OBV":
                observerExplosion.Play();
                break;
            case "OV":
                overlordExplosion.Play();
                break;
            case "PB":
                probeExplosion.Play();
                break;
            case "QU":
                queenExplosion.Play();
                break;
            case "RV":
                reaverExplosion.Play();
                break;
            case "SC":
                scoutExplosion.Play();
                break;
            case "SV":
                scvExplosion.Play();
                break;
            case "SH":
                shuttleExplosion.Play();
                break;
            case "TK":
                tankExplosion.Play();
                break;
            case "TS":
                tankSiegeExplosion.Play();
                break;
            case "UL":
                ultraliskExplosion.Play();
                break;
            case "VK":
                valkExplosion.Play();
                break;
            case "VS":
                vesselExplosion.Play();
                break;
            case "WR":
                wraithExplosion.Play();
                break;
            case "ZL":
                zealotExplosion.Play();
                break;
            default:
                Debug.Log("Unrecognized audio code. " + code + " This is not fatal.");
                break;
        }
    }
}
