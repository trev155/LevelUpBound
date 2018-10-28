/*
 * Singleton class that handles all audio related functions.
 * Attach this script to some game object.
 * Callers should access instances of this class using the static instance member,
 * eg) AudioSingleton.Instance.PlaySound()
 */
using UnityEngine;

public class AudioSingleton : MonoBehaviour {
    // Static strings to help callers
    public static readonly string WALL_UNLOCK = "Audio/debris";
    public static readonly string BUTTON_DING = "Audio/ding";
    public static readonly string LEVEL_UP = "Audio/levelup";
    public static readonly string KEY_PICKUP = "Audio/itempickup";

    public static readonly string ARCHON_EXPLOSION = "Audio/explosions/archon";
    public static readonly string DEFILER_EXPLOSION = "Audio/explosions/defiler";
    public static readonly string DEVOURER_EXPLOSION = "Audio/explosions/devourer";
    public static readonly string DRAGOON_EXPLOSION = "Audio/explosions/dragoon";
    public static readonly string DRONE_EXPLOSION = "Audio/explosions/drone";
    public static readonly string DROPSHOP_EXPLOSION = "Audio/explosions/dropship";
    public static readonly string DTEMP_EXPLOSION = "Audio/explosions/dtemp";
    public static readonly string FIREBAT_EXPLOSION = "Audio/explosions/firebat";
    public static readonly string GUARDIAN_EXPLOSION = "Audio/explosions/guardian";
    public static readonly string HTEMP_EXPLOSION = "Audio/explosions/htemp";
    public static readonly string HYDRA_EXPLOSION = "Audio/explosions/hydra";
    public static readonly string INFESTEDTERRAN_EXPLOSION = "Audio/explosions/infestedterran";
    public static readonly string KAKARU_EXPLOSION = "Audio/explosions/kakaru";
    public static readonly string LURKER_EXPLOSION = "Audio/explosions/lurker";
    public static readonly string MARINE_EXPLOSION = "Audio/explosions/marine";
    public static readonly string MEDIC_EXPLOSION = "Audio/explosions/medic";
    public static readonly string MUTALISK_EXPLOSION = "Audio/explosions/mutalisk";
    public static readonly string OBSERVER_EXPLOSION = "Audio/explosions/observer";
    public static readonly string OVERLORD_EXPLOSION = "Audio/explosions/overlord";
    public static readonly string PROBE_EXPLOSION = "Audio/explosions/probe";
    public static readonly string QUEEN_EXPLOSION = "Audio/explosions/queen";
    public static readonly string REAVER_EXPLOSION = "Audio/explosions/reaver";
    public static readonly string SCOURGE_EXPLOSION = "Audio/explosions/scourge";
    public static readonly string SCOUT_EXPLOSION = "Audio/explosions/scout";
    public static readonly string SCV_EXPLOSION = "Audio/explosions/scv";
    public static readonly string SHUTTLE_EXPLOSION = "Audio/explosions/shuttle";
    public static readonly string TANK_EXPLOSION = "Audio/explosions/tank";
    public static readonly string TANKSIEGE_EXPLOSION = "Audio/explosions/tanksiege";
    public static readonly string ULTRALISK_EXPLOSION = "Audio/explosions/ultralisk";
    public static readonly string VALK_EXPLOSION = "Audio/explosions/valk";
    public static readonly string VESSEL_EXPLOSION = "Audio/explosions/vessel";
    public static readonly string WRAITH_EXPLOSION = "Audio/explosions/wraith";
    public static readonly string ZEALOT_EXPLOSION = "Audio/explosions/zealot";

    public static readonly string PLAYER_DEATH = "Audio/explosions/zergling";

    // private instance variable, public static getter
    private static AudioSingleton instance = null;
    public static AudioSingleton Instance {
        get { return instance; }
    }

    // other members
    private AudioSource audioSource;
    private AudioSource audioSourceExplosion;
    

    // initialization
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Sound methods
    /*
     * Play a sound by loading in a resource as an audio clip from the specified path.
     * Plays at full volume, regular pitch.
     */
    public void PlaySound(string path) {
        if (!GameContext.AudioEnabled) {
            return;
        }

        audioSource = GetComponents<AudioSource>()[0];
        audioSource.clip = Resources.Load<AudioClip>(path);
        // TODO set volume to full, pitch to regular (1)
        audioSource.Play();
        // audioSource.PlayOneShot(audioSource.clip);
    }

    /*
     * An explosion is different from a regular sound, because we will need to alter its volume / pitch depending
     * on which one it is.
     */
    public void PlayExplosion(string explosionCode) {
        if (!GameContext.AudioEnabled) {
            return;
        }

        string path = "";
        switch (explosionCode) {
            case "AR":
                path = ARCHON_EXPLOSION;
                break;
            case "DA":
                path = ARCHON_EXPLOSION;
                break;
            case "DF":
                path = DEFILER_EXPLOSION;
                break;
            case "DV":
                path = DEVOURER_EXPLOSION;
                break;
            case "DG":
                path = DRAGOON_EXPLOSION;
                break;
            case "DS":
                path = DROPSHOP_EXPLOSION;
                break;
            case "DR":
                path = DRONE_EXPLOSION;
                break;
            case "DT":
                path = DTEMP_EXPLOSION;
                break;
            case "FB":
                path = FIREBAT_EXPLOSION;
                break;
            case "GD":
                path = GUARDIAN_EXPLOSION;
                break;
            case "HT":
                path = HTEMP_EXPLOSION;
                break;
            case "HY":
                path = HYDRA_EXPLOSION;
                break;
            case "IT":
                path = INFESTEDTERRAN_EXPLOSION;
                break;
            case "KA":
                path = KAKARU_EXPLOSION;
                break;
            case "LU":
                path = LURKER_EXPLOSION;
                break;
            case "MA":
                path = MARINE_EXPLOSION;
                break;
            case "ME":
                path = MEDIC_EXPLOSION;
                break;
            case "MU":
                path = MUTALISK_EXPLOSION;
                break;
            case "OB":
                path = OBSERVER_EXPLOSION;
                break;
            case "OBV":
                path = OBSERVER_EXPLOSION;
                break;
            case "OV":
                path = OVERLORD_EXPLOSION;
                break;
            case "PB":
                path = PROBE_EXPLOSION;
                break;
            case "QU":
                path = QUEEN_EXPLOSION;
                break;
            case "RV":
                path = REAVER_EXPLOSION;
                break;
            case "SG":
                path = SCOURGE_EXPLOSION;
                break;
            case "SC":
                path = SCOUT_EXPLOSION;
                break;
            case "SV":
                path = SCV_EXPLOSION;
                break;
            case "SH":
                path = SHUTTLE_EXPLOSION;
                break;
            case "TK":
                path = TANK_EXPLOSION;
                break;
            case "TS":
                path = TANKSIEGE_EXPLOSION;
                break;
            case "UL":
                path = ULTRALISK_EXPLOSION;
                break;
            case "VK":
                path = VALK_EXPLOSION;
                break;
            case "VS":
                path = VESSEL_EXPLOSION;
                break;
            case "WR":
                path = WRAITH_EXPLOSION;
                break;
            case "ZL":
                path = ZEALOT_EXPLOSION;
                break;
            case "ZE":
                path = PLAYER_DEATH;
                break;
            default:
                Debug.Log("Unrecognized audio code. " + explosionCode + " This is not fatal.");
                break;
        }

        audioSourceExplosion = GetComponents<AudioSource>()[1];
        audioSourceExplosion.clip = Resources.Load<AudioClip>(path);
        // TODO alter volume / pitch
        audioSourceExplosion.Play();
    }
}
