/*
 * Modal that is designed to appear before a level. It should only be used in the MainGame scene,
 * since it relies on the LevelManager.
 * 
 * A page can be one of two things - text only, or text and image.
 * There must be at least 1 page.
 * 
 * All data should be initialized with Initialize() after instantiating a Prefab with this script. 
 */
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ModalPrelevelInfo : Modal {
    // References to game objects
    public Image pageImage;
    public Image backArrow;
    public Image forwardArrow;
    public Image proceedButton;

    // Fields for internal state
    private int currentPage = 1;
    private int numPages;
    private List<string> messages = new List<string>();
    private Dictionary<int, string> images = new Dictionary<int, string>();

    // Scene requires a LevelManager
    private LevelManager levelManager;

    /*
     * Initialization
     */
    private void Awake() {
        GameContext.ModalActive = true;

        GameObject levelManagerGameObject = GameObject.Find("LevelManager");
        if (levelManagerGameObject != null) {
            levelManager = levelManagerGameObject.GetComponent<LevelManager>();
        }
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
        modalMainText.text = messages[currentPage - 1];
        
        // Set image on this page. If an image is not specified, hide the image
        if (images.ContainsKey(currentPage)) {
            pageImage.gameObject.SetActive(true);
            modalMainText.alignment = TextAnchor.UpperCenter;
            modalMainText.fontSize = 35;

            string path = images[currentPage];
            Sprite img = Resources.Load<Sprite>(path);
            pageImage.sprite = img;
        } else {
            pageImage.gameObject.SetActive(false);
            modalMainText.alignment = TextAnchor.MiddleCenter;
            modalMainText.fontSize = 45;
        }

        // Gray out arrows according to the page number
        if (numPages == 1) {
            Util.GrayoutImage(forwardArrow);
            Util.GrayoutImage(backArrow);
        } else if (currentPage == 1 || currentPage == numPages) {
            if (currentPage == 1) {
                Util.GrayoutImage(backArrow);
                Util.UndoGrayoutImage(forwardArrow);
            }
            if (currentPage == numPages) {
                Util.GrayoutImage(forwardArrow);
                Util.UndoGrayoutImage(backArrow);
            }
        } else {
            Util.UndoGrayoutImage(forwardArrow);
            Util.UndoGrayoutImage(backArrow);
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
        currentPage += 1;
        SetContents();
    }

    public void PreviousPage() {
        if (currentPage == 1) {
            return;
        }
        currentPage -= 1;
        SetContents();
    }

    /*
     * Button Handler for the "Proceed" button.
     * Callers should ensure they are from the MainGame scene and that a reference to a LevelManager exists.
     */
    public void CloseModalWindowAndStartLevel() {
        CloseModalWindow();
        levelManager.PlayLevel();
    }
}
