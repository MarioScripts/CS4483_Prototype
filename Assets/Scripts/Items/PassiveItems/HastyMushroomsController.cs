using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HastyMushroomsController : ItemController
{
    public override void equip() {
        base.equip();
        GameState.playerStats.attackCooldownMultiplier = 0.5f;
    }
}
