/*
 * Everything to do with the "About" page.
 */
using UnityEngine;
using UnityEngine.UI;

public class About : MonoBehaviour {
    public Image image;
    public Text imageCaption;
    public Text aboutText;

    private void Awake() {
        AspectRatioManager.AdjustScreen();
        ThemeManager.SetTheme();

        image.sprite = Resources.Load<Sprite>("AboutImages/ab1");
        imageCaption.text = "Inspiration for this game can from the popular series of custom maps from SCBW.";
        aboutText.text = "This game was made mostly for fun. For more information, see: https://github.com/trev155/LevelUpBound";
    }

    public void BackButton() {
        AudioManager.Instance.PlayUISound(AudioManager.BUTTON_DING);
        GameContext.LoadPreviousPage();
    }
}
