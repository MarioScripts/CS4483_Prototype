using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcavatorsHandyToolsController : ActiveItemController {
    [SerializeField] private BombItemController bomb;
    [SerializeField] private float speed = 5;

    public override void use() {
        if (canUse && availableUses > 0) {
            availableUses--;
            GameState.player.StartCoroutine(throwBombCoroutine());
        }
    }

    private IEnumerator throwBombCoroutine() {
        canUse = false;
        GameObject spawn = Instantiate(bomb.gameObject, GameState.player.transform.position, this.gameObject.transform.rotation);
        spawn.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        yield return new WaitForSeconds(5);
        canUse = availableUses > 0;
    }
}