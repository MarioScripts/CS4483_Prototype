using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelController : EnemyController {
    private List<EnemyController> shieldedEnemies = new List<EnemyController>();
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        foreach (EnemyController enemy in enemies) {
            if (enemy.trackNum == trackNum + 1 || enemy.trackNum == trackNum - 1) {
                enemy.setShield(true);
                shieldedEnemies.Add(enemy);
            }
        }
    }

    protected override void die() {
        Debug.Log(shieldedEnemies);
        foreach (EnemyController enemy in shieldedEnemies) {
            enemy.setShield(false);
        }
        
        base.die();
    }
}
