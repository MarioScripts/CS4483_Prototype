using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour {
    [SerializeField] private ItemController defaultAttackItem;
    private PlayerController player;
    // Start is called before the first frame update
    void Start() {
        initializeTracksForLevel();
        player = FindObjectOfType<PlayerController>();
        // GameState.addItem(defaultAttackItem);

        GameState.levelComplete = false;
        GameState.inEndPosition = false;
        GameState.stopCamera = false;
        GameState.levelEnd = false;
    }

    // Update is called once per frame
    void Update() {
        if (!GameState.levelComplete) {
            GameState.time += Time.deltaTime;

            EnemySpawnerController[] enemyspawners = FindObjectsOfType<EnemySpawnerController>();
            if (enemyspawners == null || enemyspawners.Length == 0) {
                GameState.levelComplete = true;
                player.attemptEndPosition();
            }
        }
    }

    private void initializeTracksForLevel() {
        GameObject[] trackObjects = GameObject.FindGameObjectsWithTag("Track");
        GameState.tracks = new List<BoxCollider2D>();
        foreach (GameObject track in trackObjects) {
            GameState.tracks.Add(track.GetComponent<BoxCollider2D>());
        }
    }
}
