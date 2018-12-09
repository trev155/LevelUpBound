/*
 * The Intro scene is the very first scene of the game. It leads into the MainMenu.
 */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Intro : MonoBehaviour {
    public Text titleText;
    public Image arrowImage;
    public Text continueText;
    private bool fadeInComplete = false;
    private bool checkForFadeInComplete = true;
    private bool listenForContinueTap = false;

    private void Awake() {
        PersistentStorage.LoadData();   // Load saved data here, as it is the main landing page for the app
    }

    private void Start() {
        PlayIntroSceneSoundtrack();
        StartCoroutine(FadeInCanvasElementsAfterDelay(0.5f));
    }

    private void Update() {
        if (fadeInComplete && checkForFadeInComplete) {
            continueText.gameObject.SetActive(true);
            checkForFadeInComplete = false;
            listenForContinueTap = true;
        }

        if (listenForContinueTap) {
            if (HasTapped() || Input.GetMouseButtonDown(0)) {
                UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetSceneNameString(SceneName.MAIN_MENU));
                if (GameContext.AudioEnabled) {
                    AudioManager.Instance.PlayBackgroundMusic();
                }
            }
        }
    }

    private void PlayIntroSceneSoundtrack() {
        AudioManager.Instance.PlaySound(AudioManager.INTRO_MUSIC);
    }

    private IEnumerator FadeInCanvasElementsAfterDelay(float seconds) {
        yield return new WaitForSeconds(seconds);
        StartCoroutine(FadeInGradually(titleText));
        StartCoroutine(FadeInGradually(arrowImage));
    }

    private IEnumerator FadeInGradually(Text text) {
        float fadeInTime = 2.5f;
        Color objectColor = text.color;
        while (objectColor.a < 1) {
            objectColor.a += Time.deltaTime / fadeInTime;
            text.color = objectColor;
            if (objectColor.a >= 1) {
                objectColor.a = 1;
            } 
            yield return null;
        }

    }

    private IEnumerator FadeInGradually(Image image) {
        float fadeInTime = 2.5f;
        Color objectColor = image.color;
        while (objectColor.a < 1) {
            objectColor.a += Time.deltaTime / fadeInTime;
            image.color = objectColor;
            if (objectColor.a >= 1) {
                objectColor.a = 1;
            }
            yield return null;
        }
        fadeInComplete = true;
    }

    private bool HasTapped() {
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
    }

}
