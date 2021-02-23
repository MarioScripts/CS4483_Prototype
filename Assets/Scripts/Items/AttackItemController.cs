using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackItemController : ItemController {
   
    [SerializeField] private float speed = 20f;
    [SerializeField] private int damage = 20;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) {
            EnemyController enemy = other.GetComponent<EnemyController>();
            enemy.dealDamage(damage * GameState.playerStats.attackDamageMultiplier);

            Destroy(transform.gameObject);
        } else if (other.CompareTag("EnemyProjectile")) {
            if (other.GetComponent<EnemyProjectile>().destroyable) {
                Destroy(other.gameObject);
            } else {
                Destroy(transform.gameObject);
            }
        }
    }
}