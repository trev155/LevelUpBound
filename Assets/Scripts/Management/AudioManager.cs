﻿/*
 * Class that handles all things audio related for the main game.
 */
using UnityEngine;

public class AudioManager : MonoBehaviour {
    // Explosions
    public AudioSource archonExplosion;
    public AudioSource defilerExplosion;
    public AudioSource devourerExplosion;
    public AudioSource dragoonExplosion;
    public AudioSource dropshipExplosion;
    public AudioSource droneExplosion;
    public AudioSource dtempExplosion;
    public AudioSource firebatExplosion;
    public AudioSource guardianExplosion;
    public AudioSource htempExplosion;
    public AudioSource hydraExplosion;
    public AudioSource infestedTerranExplosion;
    public AudioSource kakaruExplosion;
    public AudioSource lurkerExplosion;
    public AudioSource marineExplosion;
    public AudioSource medicExplosion;
    public AudioSource mutaliskExplosion;
    public AudioSource observerExplosion;
    public AudioSource overlordExplosion;
    public AudioSource probeExplosion;
    public AudioSource queenExplosion;
    public AudioSource reaverExplosion;
    public AudioSource scourgeExplosion;
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

    // Other audio
    public AudioSource playerDeath;
    public AudioSource debris;

    /*
     * Initialization.
     */
    private void Awake() {
        InitializeVolumeLevels();
    }

    /*
     * Initialize volume levels.
     * Since this will be used in multiple classes, refactor this out later on.
     */
    private void InitializeVolumeLevels() {    
        GameObject[] allAudioObjects = GameObject.FindGameObjectsWithTag("Audio");
        foreach (GameObject audioObject in allAudioObjects) {
            AudioSource audioSource = audioObject.GetComponent<AudioSource>();
            audioSource.volume = GameContext.CurrentVolume;
        }
    }

    /*
     * Play an Explosion sound effect, given a 2-character code.
     */
    public void PlayExplosionAudio(string code) {
        if (!GameContext.AudioEnabled) {
            return;
        }

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
            case "DR":
                droneExplosion.Play();
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
            case "IT":
                infestedTerranExplosion.Play();
                break;
            case "KA":
                kakaruExplosion.Play();
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
            case "MU":
                mutaliskExplosion.Play();
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
            case "SG":
                scourgeExplosion.Play();
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

    /*
     * Play player death sound.
     */
    public void PlayPlayerDeathAudio() {
        if (!GameContext.AudioEnabled) {
            return;
        }

        playerDeath.Play();
    }

    /*
     * Wall destruction.
     */
    public void PlayWallDestroyAudio() {
        if (!GameContext.AudioEnabled) {
            return;
        }

        debris.Play();
    }
}
