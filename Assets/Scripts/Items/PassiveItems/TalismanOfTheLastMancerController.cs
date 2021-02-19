using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalismanOfTheLastMancerController : ItemController
{
    public override void equip() {
        base.equip();
        GameState.playerStats.attackDamageMultiplier *= 2;
        GameState.playerStats.attackSizeMultiplier *= 2;
    }
}
