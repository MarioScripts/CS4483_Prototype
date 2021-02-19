using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = System.Random;

public class BoonSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject boon;
    [SerializeField] private float speed;
    
    private BoxCollider2D boonCollider;

    [SerializeField] private int odds = 3;
    [SerializeField] private int badLuckOdds = 2;
    
    // Start is called before the first frame update
    void Start() {
        boonCollider = boon.GetComponent<BoxCollider2D>();
        spawnBoon(UnityEngine.Random.Range(0, GameState.tracks.Count - 1));
        InvokeRepeating("attemptBoonSpawn", 1, 3);
    }

    private void Update() {
        if (GameState.levelComplete) {
            CancelInvoke();
        }
    }

    private void attemptBoonSpawn()
    {
        if (!GameState.levelComplete) {
            int willSpawn = UnityEngine.Random.Range(1, GameState.player.momentum > 150 ? badLuckOdds : odds);
            int randTrack = UnityEngine.Random.Range(0, GameState.tracks.Count);
            if (willSpawn == 1) {
                spawnBoon(randTrack);
            }
        }
    }

    private void spawnBoon(int trackIndex)
    {
        BoxCollider2D trackCollider = GameState.tracks[trackIndex];
        float yTrackPos = (trackCollider.size.y / 2) + trackCollider.transform.position.y - boonCollider.size.y/4;
        Vector2 spawnPosition = new Vector2(transform.position.x, yTrackPos);
        
        Instantiate(boon, spawnPosition, transform.rotation);
    }
}
