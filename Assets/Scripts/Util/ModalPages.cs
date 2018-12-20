/*
 * This modal is similar to the ModalPrelevelInfo, except that it is not tied to the MainGame scene.
 * It does not require a level manager.
 * 
 * A page can be one of two things - text only, or text and image.
 * There must be at least 1 page.
 * 
 * All data should be initialized with Initialize() after instantiating a Prefab with this script. 
 */
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ModalPages : Modal {
    public Image pageImage;
    public Image backArrow;
    public Image forwardArrow;
    public Image proceedButton;

    private int currentPage = 1;
    private int numPages;
    private List<string> messages = new List<string>();
    private Dictionary<int, string> images = new Dictionary<int, string>();

    private void Awake() {
        GameContext.ModalActive = true;
    }

    /*
     * Initialize data for this modal window. This must be called after instantiation!
     */
    public void Initialize(List<string> msgs, Dictionary<int, string> imageLevelToPaths) {
        foreach (string msg in msgs) {
            messages.Add(msg);
        }
        numPages = messages.Count;

        foreach (KeyValuePair<int, string> entry in imageLevelToPaths) {
            images.Add(entry.Key, entry.Value);
        }
        
        SetContents();
    }

    /*
     * Set contents of the current page of the modal window.
     */
    public void SetContents() {
        // Set text on the current page
        SetMainText(messages[currentPage - 1]);
        
        // Set image on this page. If an image is not specified, hide the image
        if (images.ContainsKey(currentPage)) {
            pageImage.gameObject.SetActive(true);
            SetMainTextAlignment(TextAnchor.UpperCenter);
            SetMainTextFontSize(40);

            string path = images[currentPage];
            Sprite img = Resources.Load<Sprite>(path);
            pageImage.sprite = img;
        } else {
            pageImage.gameObject.SetActive(false);
            SetMainTextAlignment(TextAnchor.MiddleCenter);
            SetMainTextFontSize(50);
        }

        // Gray out arrows according to the page number
        if (numPages == 1) {
            Utils.GrayoutImage(forwardArrow);
            Utils.GrayoutImage(backArrow);
        } else if (currentPage == 1 || currentPage == numPages) {
            if (currentPage == 1) {
                Utils.GrayoutImage(backArrow);
                Utils.UndoGrayoutImage(forwardArrow);
            }
            if (currentPage == numPages) {
                Utils.GrayoutImage(forwardArrow);
                Utils.UndoGrayoutImage(backArrow);
            }
        } else {
            Utils.UndoGrayoutImage(forwardArrow);
            Utils.UndoGrayoutImage(backArrow);
        }

        // Show proceed button if we're on the last page
        if (currentPage == numPages) {
            proceedButton.gameObject.SetActive(true);
        } else {
            proceedButton.gameObject.SetActive(false);
        }
    }

    // Arrow button handlers for moving pages
    public void AdvancePage() {
        if (currentPage == numPages) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        currentPage += 1;
        SetContents();
    }

    public void PreviousPage() {
        if (currentPage == 1) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        currentPage -= 1;
        SetContents();
    }
}
