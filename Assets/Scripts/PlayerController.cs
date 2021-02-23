using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public int currentTrack;
    
    private float baseMomentum = 100;
    private Animator animator;
    private bool beginLevelEnd = false;

    [SerializeField] public float momentum = 100;
    [SerializeField] float startingPosition = -3f;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        currentTrack = 0;

        BoxCollider2D trackCollider = GameState.tracks[currentTrack];
        float yTrackPos = (trackCollider.size.y / 2) + trackCollider.transform.position.y;
        transform.position = new Vector2(transform.position.x, yTrackPos);

        StartCoroutine(loseMomentum());
        InvokeRepeating("loseMomentum", 1, GameState.playerStats.momentumRate * GameState.playerStats.momentumRateMultiplier);
    }

    // Update is called once per frame
    void Update() {
        if (GameState.levelComplete && momentum <= baseMomentum + GameState.playerStats.momentumDecayRate &&
            momentum >= baseMomentum - GameState.playerStats.momentumDecayRate) {
            GameState.inEndPosition = true;
        }

        if (!GameState.inEndPosition) {
            transform.position = new Vector2(startingPosition * ((float) momentum / (float) baseMomentum),
                transform.position.y);
        }
        else if (GameState.stopCamera && !GameState.levelEnd) {
            GameState.playerStats.momentumRateMultiplier = 1f;
            animator.SetTrigger("StopCamera");
        }

        if (beginLevelEnd && !GameState.levelEnd) {
            Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
            animator.ResetTrigger("StopCamera");
            animator.SetTrigger("LevelEnd");
            rb.velocity = new Vector2(rb.velocity.x + 3, rb.velocity.y);
            GameState.levelEnd = true;
        }

        handleControls();
    }

    public void increaseMomentum(int increase) {
        GameState.playerStats.momentumPool += increase;
    }

    public void attemptEndPosition() {
        GameState.playerStats.momentumRateMultiplier = 0.1f;

        if (momentum > 100) {
            GameState.playerStats.momentumPool = momentum - 100;
        }
        else {
            GameState.playerStats.momentumPool = 0;
        }
    }

    private IEnumerator loseMomentum() {
        while (true) {
            if (GameState.inEndPosition) yield break;
    
            if (GameState.playerStats.momentumPool > GameState.playerStats.momentumIncreaseRate) {
                momentum -= GameState.playerStats.momentumIncreaseRate;
                GameState.playerStats.momentumPool -= GameState.playerStats.momentumIncreaseRate;
            }
            else if (GameState.playerStats.momentumPool > 0) {
                momentum -= GameState.playerStats.momentumPool;
                GameState.playerStats.momentumPool = 0;
            }
            else {
                momentum += GameState.playerStats.momentumDecayRate;
            }
            
            yield return new WaitForSeconds(GameState.playerStats.momentumRate * GameState.playerStats.momentumRateMultiplier);
        }
    }

    private void handleControls() {
        if (Input.GetKeyDown(KeyCode.W) && currentTrack < GameState.tracks.Length - 1) {
            currentTrack++;
            BoxCollider2D trackCollider = GameState.tracks[currentTrack];
            float yTrackPos = (trackCollider.size.y / 2) + trackCollider.transform.position.y;
            transform.position = new Vector2(transform.position.x, yTrackPos);
        }
        else if (Input.GetKeyDown(KeyCode.S) && currentTrack > 0) {
            currentTrack--;
            BoxCollider2D trackCollider = GameState.tracks[currentTrack];
            float yTrackPos = (trackCollider.size.y / 2) + trackCollider.transform.position.y;
            transform.position = new Vector2(transform.position.x, yTrackPos);
        } 
        else if (Input.GetKeyDown(KeyCode.F) && GameState.stopCamera) {
            // Look for item on track
            ItemController[] items = FindObjectsOfType<ItemController>();
            bool foundItem = false;
            foreach (ItemController item in items) {
                // Debug.Log();
                if (item.trackNum == currentTrack) {
                    foundItem = true;
                    item.equip();
                    
                    item.gameObject.SetActive(false);
                }
                else {
                    Destroy(item.gameObject);
                }
            }
            
            beginLevelEnd = foundItem;
        } 
        else if (Input.GetKeyDown(KeyCode.E) && GameState.activeItem != null) {
            GameState.activeItem.use();
        }
    }
}