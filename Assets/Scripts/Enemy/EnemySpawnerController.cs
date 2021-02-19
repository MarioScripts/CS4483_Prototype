using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawnerController : MonoBehaviour {
    [SerializeField] private EnemySpawn[] enemySpawns;
    private int numOfEnemies;
    private int enemyIndex = 0;
    private GameObject spawnedEnemy;

    // Start is called before the first frame update
    void Start() {
        numOfEnemies = enemySpawns.Length - 1;
        spawnEnemy();
    }

    // Update is called once per frame
    void Update() {
        if (spawnedEnemy == null) {
            spawnEnemy();
        }
    }

    public void spawnEnemy() {
        if (enemyIndex <= numOfEnemies) {
            spawnedEnemy = Instantiate(enemySpawns[enemyIndex].enemy.gameObject,
                new Vector2(transform.position.x,
                    transform.position.y +
                    enemySpawns[enemyIndex].enemy.GetComponent<SpriteRenderer>().bounds.extents.y), transform.rotation);
            spawnedEnemy.GetComponent<EnemyController>().enemyProperties = enemySpawns[enemyIndex].enemyProperties;
            enemyIndex++;
        }
        else {
            Destroy(transform.gameObject);
        }
    }
}