using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : EnemyController {
    private BoxCollider2D attackCollider;

    protected override void Start() {
        attackCollider = enemyProperties.attackPrefab.GetComponent<BoxCollider2D>();
        base.Start();
    }

    protected override void attack() {
        StartCoroutine(attackCoroutine());
    }

    private IEnumerator attackCoroutine()
    {
        for (int i = 0; i < enemyProperties.attackTimes; i++) { 
            BoxCollider2D trackCollider = GameState.tracks[trackNum];
            float yTrackPos = (trackCollider.size.y / 2) + trackCollider.transform.position.y - attackCollider.size.y/4;
            Vector2 spawnPosition = new Vector2(transform.position.x, yTrackPos);
            Instantiate(enemyProperties.attackPrefab, spawnPosition, enemyProperties.attackPrefab.transform.rotation);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
