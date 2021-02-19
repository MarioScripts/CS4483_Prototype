using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintersElixirContoller : ActiveItemController {
    public override void use() {
        if (canUse && numberOfUses > 0) {
            GameState.player.StartCoroutine(drinkCoroutine());
            numberOfUses--;
        }
    }

    private IEnumerator drinkCoroutine() {
        canUse = false;
        GameState.playerStats.momentumRateMultiplier = 0.5f;
        GameState.player.increaseMomentum(100);
        yield return new WaitForSeconds(5);
        canUse = true;
    }
}
