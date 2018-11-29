/*
 * Everything to do with the Instructions page.
 */
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Instructions : MonoBehaviour {
    public Image instructionsImage;
    public Image leftArrowButton;
    public Image rightArrowButton;
    public Text instructionsText;
    public Text pageText;

    private int currentPage = 1;
    private readonly int numPages = 6;
    private Dictionary<int, string> instructionImageLocations = new Dictionary<int, string>();
    private Dictionary<int, string> instructionTextValues = new Dictionary<int, string>();

    private void Awake() {
        AspectRatioManager.AdjustScreenElements();
        ThemeManager.SetTheme();

        InitializeInstructionData();
        SetPageContents();
        BlurArrows();
    }

    private void InitializeInstructionData() {
        instructionImageLocations.Add(1, "InstructionImages/Ins1-1");
        instructionTextValues.Add(1, "Welcome! Level Up Bound is a top-down arcade style game where the objective is to move your player to the goal.");
        instructionImageLocations.Add(2, "InstructionImages/Ins1-2");
        instructionTextValues.Add(2, "To play, select a game mode from the main menu. Or, you can play levels one at a time from the level selector.");
        instructionImageLocations.Add(3, "InstructionImages/Ins1-3");
        instructionTextValues.Add(3, "There are two ways to move the player, which you can choose from in the Options page: Touch Arrows, or Point-Click.");
        instructionImageLocations.Add(4, "InstructionImages/Ins1-4");
        instructionTextValues.Add(4, "Each level contains patterns of explosions. It is up to you, through precise timing and control, to pass through these obstacles.");
        instructionImageLocations.Add(5, "InstructionImages/Ins1-5");
        instructionTextValues.Add(5, "Some levels contain external elements, such as walls, key doors, and more.");
        instructionImageLocations.Add(6, "InstructionImages/Ins1-6");
        instructionTextValues.Add(6, "Have fun bounding!");
    }

    private void SetPageContents() {
        instructionsImage.sprite = Resources.Load<Sprite>(instructionImageLocations[currentPage]);
        instructionsText.text = instructionTextValues[currentPage];
        pageText.text = currentPage + " of " + numPages;
    }

    private void BlurArrows() {
        Utils.UndoGrayoutImage(leftArrowButton);
        Utils.UndoGrayoutImage(rightArrowButton);
        if (currentPage == 1) {
            Utils.GrayoutImage(leftArrowButton);
        }
        if (currentPage == numPages) {
            Utils.GrayoutImage(rightArrowButton);
        }
    }

    // Button Handlers
    public void ScrollPageLeft() {
        if (currentPage == 1) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        currentPage -= 1;
        SetPageContents();
        BlurArrows();
    }

    public void ScrollPageRight() {
        if (currentPage == numPages) {
            return;
        }
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        currentPage += 1;
        SetPageContents();
        BlurArrows();
    }

    public void BackButton() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.LoadPreviousContextPage();
    }
}

