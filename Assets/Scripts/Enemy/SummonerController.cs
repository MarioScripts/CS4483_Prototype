using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerController : EnemyController {
    // Start is called before the first frame update
    void Start() {
        base.Start();
    }

    // Update is called once per frame
    void Update() {
        base.Update();
    }

    protected override void attack() {
        SpiderController spider = FindObjectOfType<SpiderController>();
        if (spider == null) {
            base.attack();
        }
    }
}