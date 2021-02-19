using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour {
    [SerializeField] public string name;
    [SerializeField] public string description;
    [SerializeField] public int id;
    [SerializeField] public ItemType type;
    
    public enum ItemType {
        Passive,
        Active,
        Attack
    };
    
    private Canvas descriptionCanvas;
    public int trackNum;
    
    // Start is called before the first frame update
    protected void Start() {
        descriptionCanvas = GetComponentInChildren<Canvas>();
    }

    // Update is called once per frame
    protected void Update() {
        if (descriptionCanvas != null) {
            // Show/hide item info popup depending on whether the player is on the same track or not
            if (GameState.player.currentTrack == trackNum) {
                descriptionCanvas.gameObject.SetActive(true);
            } else {
                descriptionCanvas.gameObject.SetActive(false);
            }
        }
    }

    protected void Awake() {
        DontDestroyOnLoad(this);
    }

    public virtual void equip() {
        GameState.addItem(this);
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Track")) {
            trackNum = other.GetComponent<TrackController>().trackNum - 1;
        }
    }

    public override bool Equals(object other) {
        if (other is ItemController) {
            return ((ItemController) other).id == id;
        }
        
        return base.Equals(other);
    }
}