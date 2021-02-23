using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerController : EnemyController {
    protected override void attack() {
        SpiderController spider = FindObjectOfType<SpiderController>();
        if (spider == null) {
            base.attack();
        }
    }
}