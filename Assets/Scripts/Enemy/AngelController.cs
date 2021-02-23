using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelController : EnemyController {
    private List<EnemyController> shieldedEnemies = new List<EnemyController>();

    // Update is called once per frame
    protected override void Update()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        foreach (EnemyController enemy in enemies) {
            if (enemy.trackNum == (trackNum + 1) || enemy.trackNum == (trackNum - 1)) {
                enemy.setShield(true);
                shieldedEnemies.Add(enemy);
            }
            else {
                enemy.setShield(false);
            }
        }
    }

    protected override void die() {
        foreach (EnemyController enemy in shieldedEnemies) {
            enemy.setShield(false);
        }
        
        base.die();
    }
}
