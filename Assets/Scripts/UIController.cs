using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    [SerializeField] private Slider momentumSlider;
    [SerializeField] private Image attackImage;
    [SerializeField] private Image activeImage;
    [SerializeField] private TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start() {
        SpriteRenderer attackSpriteRenderer = GameState.attackItem.GetComponent<SpriteRenderer>();
        attackImage.sprite = attackSpriteRenderer.sprite;
        attackImage.transform.rotation = attackSpriteRenderer.transform.rotation;
        attackImage.transform.localScale = attackSpriteRenderer.transform.localScale;

        Debug.Log(GameState.activeItem);
        if (GameState.activeItem != null) {
            SpriteRenderer activeSpriteRenderer = GameState.activeItem.GetComponent<SpriteRenderer>();
            activeImage.sprite = activeSpriteRenderer.sprite;
            activeImage.transform.rotation = activeSpriteRenderer.transform.rotation;
        }
    }

    // Update is called once per frame
    void Update() {
        timeText.text = ((int) GameState.time).ToString().PadLeft(3, '0');
        momentumSlider.value = GameState.player.momentum;
    }
}