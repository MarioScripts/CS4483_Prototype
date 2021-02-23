using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    [SerializeField] private Slider momentumSlider;
    [SerializeField] private Image attackImage;
    [SerializeField] private Image activeImage;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Image[] passiveItemImages;
    [SerializeField] private Image levelFade;

    private SpriteRenderer attackSpriteRenderer;
    void Start() {
        attackSpriteRenderer = GameState.attackItem.GetComponent<SpriteRenderer>();
        attackImage.sprite = attackSpriteRenderer.sprite;
        attackImage.transform.rotation = attackSpriteRenderer.transform.rotation;
        attackImage.transform.localScale = attackSpriteRenderer.transform.localScale;

        if (GameState.activeItem != null) {
            SpriteRenderer activeSpriteRenderer = GameState.activeItem.GetComponent<SpriteRenderer>();
            activeImage.sprite = activeSpriteRenderer.sprite;
            activeImage.transform.rotation = activeSpriteRenderer.transform.rotation;
        }

        if (GameState.items != null) {
            for (int i = 0; i < GameState.passiveItems.Count; i++) {
                ItemController item = GameState.passiveItems[i];
                Sprite itemImage = item.GetComponent<SpriteRenderer>().sprite;
                passiveItemImages[i].sprite = itemImage;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        timeText.text = ((int) GameState.time).ToString().PadLeft(3, '0');
        momentumSlider.value = GameState.player.momentum;
        if (GameState.activeItem != null && !GameState.activeItem.canUse) {
            activeImage.color = new Color(activeImage.color.r, activeImage.color.g, activeImage.color.b, 0.25f);
        }
        else {
            activeImage.color = new Color(activeImage.color.r, activeImage.color.g, activeImage.color.b, 1f);
        }

        if (GameState.levelEnd) {
            if (levelFade.color.a < 1) {
                levelFade.color = new Color(levelFade.color.r, levelFade.color.g, levelFade.color.b,
                    levelFade.color.a + 0.002f);
            } else {
                SceneManager.LoadScene("LevelTwo");
            }
        }
    }
}