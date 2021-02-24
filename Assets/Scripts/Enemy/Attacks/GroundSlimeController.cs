using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlimeController : EnemyProjectile {
    [SerializeField] private float speed;
    [SerializeField] private float stuckDuration = 0.5f;

    private bool stuckOnPlayer = false;
    // Update is called once per frame
    protected override void Update() {
        transform.position = new Vector2(transform.position.x - speed, transform.position.y);
        if (stuckOnPlayer && GameState.player.isStuck) {
            GameState.player.transform.position = new Vector2(transform.position.x, GameState.player.transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            StartCoroutine(stickPlayerCoroutine());
        }
    }

    IEnumerator stickPlayerCoroutine() {
        GameState.player.isStuck = true;
        stuckOnPlayer = true;
        
        yield return new WaitForSeconds(stuckDuration);

        float momentum = (GameState.player.transform.position.x / GameState.player.startingPosition) * GameState.player.baseMomentum;
        GameState.player.momentum = momentum;
        GameState.player.isStuck = false;
        stuckOnPlayer = false;
    }
}